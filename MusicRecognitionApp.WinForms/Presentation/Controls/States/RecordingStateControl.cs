using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class RecordingStateControl : UserControl
    {
        private readonly IStateManagerService _stateManagerService;
        private readonly IMessageBoxService _messageBoxService;
        private IRecordingSessionService _sessionService;
        
        public RecordingStateControl(
            IStateManagerService stateManagerService,
            IRecordingSessionService sessionService,
            IMessageBoxService messageBoxService)
        {
            InitializeComponent();

            _stateManagerService = stateManagerService;
            _sessionService = sessionService;
            _messageBoxService = messageBoxService;
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
            try
            {
                _sessionService.RecordingSession += OnRecordingProgress;

                string? recordedAudioFile = await _sessionService.StartRecordingAsync();

                if (string.IsNullOrEmpty(recordedAudioFile))
                {
                    await _stateManagerService.SetStateAsync(AppState.Ready);
                }
                else
                {
                    await _stateManagerService.SetStateAsync(AppState.Analyzing, recordedAudioFile);
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError($"Recording failed: {ex.Message}");

                await _stateManagerService.SetStateAsync(AppState.Ready);
            }
            finally
            {
                _sessionService.RecordingSession -= OnRecordingProgress;
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
