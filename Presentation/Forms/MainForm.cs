using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using System.Text;

namespace MusicRecognitionApp.Forms
{
    public partial class MainForm : BaseForm
    {
        private AppState _currentState ;
        private Dictionary<AppState, UserControl> _states = new();
        private string _recordedAudioFile; 
        private CancellationTokenSource _recordingCancellationTokenSource;
        
        public List<SearchResultModel> RecognitionResults { get; set; }
        public IAudioRecorder AudioRecorder { get; set; }

        private readonly IMessageBox _messageBoxService;
        private readonly IStateRegistry _stateRegistryService;
        private readonly IAudioRecognition _recognitionService;
        private readonly IServiceProvider _serviceProvider;
        public MainForm(
            IMessageBox messageBoxService, 
            IStateRegistry stateRegistryService,
            IAudioRecognition recognitionService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _messageBoxService = messageBoxService;
            _stateRegistryService = stateRegistryService;
            _recognitionService = recognitionService;
            _serviceProvider = serviceProvider;

            SetStateAsync(AppState.Ready);
        }

        private void InitializeState()
        {
            if (!_states.ContainsKey(_currentState))
            {
                var control = _stateRegistryService.CreateStateControl(this, _currentState);

                control.Dock = DockStyle.Fill;
                control.Visible = false;

                _states[_currentState] = control;
                this.Controls.Add(control);
            }
        }

        public async Task SetStateAsync(AppState newState)
        {
            if (_states.ContainsKey(_currentState))
                _states[_currentState].Visible = false;

            _currentState = newState;
            InitializeState();

            _states[_currentState].Visible = true;
            _states[_currentState].BringToFront();

            if (_currentState == AppState.Recording)
            {
                await StartRecordingProcess();
            }
            else if (_currentState == AppState.Analyzing)
            {
                await StartAnalysisProcess();
                RecognitionResults = null;
            }
        }

        private async Task StartRecordingProcess()
        {
            _recordingCancellationTokenSource = new CancellationTokenSource();

            var recorder = _serviceProvider.GetRequiredService<IAudioRecorder>();

            if (_states[AppState.Recording] is RecordingStateControl recordingStateControl)
            {
                recordingStateControl.SetRecorder(recorder);
            }

            _recordedAudioFile = await recorder.RecordAudioFromMicrophoneAsync(15, _recordingCancellationTokenSource.Token);

            if (_recordingCancellationTokenSource.Token.IsCancellationRequested 
                || string.IsNullOrEmpty(_recordedAudioFile))
            {
                SetStateAsync(AppState.Ready);
            }
            else
            {
                SetStateAsync(AppState.Analyzing);
            }
        }

        private async Task StartAnalysisProcess()
        {
            if (_states[AppState.Analyzing] is AnalyzingStateControl analyzingStateControl)
            {
                analyzingStateControl.SetRecognition(_recognitionService);
            }

            RecognitionResults = await _recognitionService.RecognizeFromMicrophoneAsync(_recordedAudioFile);

            if (!string.IsNullOrEmpty(_recordedAudioFile) && File.Exists(_recordedAudioFile))
            {
                 File.Delete(_recordedAudioFile);
                 _recordedAudioFile = null;
            }

            SetStateAsync(AppState.Result);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = _messageBoxService
                .ShowWarning("Вы действительно хотите закрыть приложение?");

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        public void StopRecording()
        {
            _recordingCancellationTokenSource?.Cancel(); 
        }

        public AppState GetCurrentControl()
        {
            return _currentState;
        }
    }
}
