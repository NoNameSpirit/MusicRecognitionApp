using MaterialSkin.Controls;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Forms
{
    public partial class MainForm : BaseForm
    {
        private AppState _currentState;
        private Dictionary<AppState, UserControl> _states = new();
        private string _recordedAudioFile; 
        private CancellationTokenSource _recordingCancellationTokenSource;
        
        public List<(int songId, string title, string artist, int matches, double confidence)> RecognitionResults { get; set; }

        private readonly IMessageBox _messageBoxService;
        private readonly IStateRegistry _stateRegistryService;
        private readonly IAudioRecognition _recognitionService;
        
        public MainForm(
            IMessageBox messageBoxService, 
            IStateRegistry stateRegistryService, 
            IAudioRecognition recognitionService)
        {
            InitializeComponent();

            _messageBoxService = messageBoxService;
            _stateRegistryService = stateRegistryService;
            _recognitionService = recognitionService;

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

        public async void SetStateAsync(AppState newState)
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
            _recordedAudioFile = await _recognitionService.RecordAudioAsync(15, _recordingCancellationTokenSource.Token);

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
            var results = await _recognitionService.RecognizeFromMicrophoneAsync(_recordedAudioFile);
            RecognitionResults = results;

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
    }
}
