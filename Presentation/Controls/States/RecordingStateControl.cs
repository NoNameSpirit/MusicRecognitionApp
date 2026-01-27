using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class RecordingStateControl : UserControl
    {
        private readonly IStateManagerService _stateManagerService;
        
        private IRecordingSessionService _sessionService;

        public RecordingStateControl(
            IStateManagerService stateManagerService,
            IRecordingSessionService sessionService)
        {
            InitializeComponent();

            _stateManagerService = stateManagerService;
            _sessionService = sessionService;
        }

        private void BtnStopRecording_Click(object sender, EventArgs e)
        {
            _sessionService.StopRecording();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        
            if (Visible)
            {
                ProgressBarRecording.Value = 0;
                
                _ = StartRecordingAsync();
            }
        }

        private async Task StartRecordingAsync()
        {
            string? recordedAudioFile;
            try 
            {
                _sessionService.RecordingSession += OnRecordingProgress;
                
                recordedAudioFile = await _sessionService.StartRecordingAsync();
            }
            finally 
            {
                _sessionService.RecordingSession -= OnRecordingProgress;
            }

            if (string.IsNullOrEmpty(recordedAudioFile))
            {
                await _stateManagerService.SetStateAsync(AppState.Ready);
            }
            else
            {
                await _stateManagerService.SetStateAsync(AppState.Analyzing, recordedAudioFile);
            }
        }

        private void OnRecordingProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(OnRecordingProgress), progress);
                return;
            }

            ProgressBarRecording.Value = progress;
        }
    }
}
