using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class AnalyzingStateControl : UserControl, IStateWithData
    {
        public List<SearchResult>? RecognitionResults { get; set; }

        private readonly IStateManagerService _stateManagerService;
        
        private IAnalyzingSessionService _sessionService;
        private string? _recordedAudioFile;
        public AnalyzingStateControl(
            IStateManagerService stateManagerService,
            IAnalyzingSessionService sessionService)
        {
            InitializeComponent();

            _stateManagerService = stateManagerService;
            _sessionService = sessionService;
        }

        public void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), progress);
                return;
            }

            ProgressBarAnalyzing.Value = progress;
            LblProgressPercent.Text = $"{progress}%";
        }

        public void SetStateData(object? stateData)
        {
            _recordedAudioFile = stateData as string;
        }

        private async Task StartAnalyzingAsync()
        {
            try
            {
                _sessionService.AnalyzingSession += UpdateProgress;

                RecognitionResults = await _sessionService.StartAnalyzingAsync(_recordedAudioFile);
            }
            finally 
            {
                _sessionService.AnalyzingSession -= UpdateProgress;
            }

            await _stateManagerService.SetStateAsync(AppState.Result, RecognitionResults);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                ProgressBarAnalyzing.Value = 0;
                LblProgressPercent.Text = "0%";
                RecognitionResults = null;

                _ = StartAnalyzingAsync();
            }
        }
    }
}
