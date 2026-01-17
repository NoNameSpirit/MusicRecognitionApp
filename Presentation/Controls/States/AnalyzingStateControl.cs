using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class AnalyzingStateControl : UserControl, IStateWithData
    {
        public List<SearchResultModel>? RecognitionResults { get; set; }

        private readonly IStateManagerService _stateManagerService;
        private readonly IServiceProvider _serviceProvider;
        
        private IAnalyzingSessionService _sessionService;
        private string? _recordedAudioFile;
        public AnalyzingStateControl(
            IStateManagerService stateManagerService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _stateManagerService = stateManagerService;
            _serviceProvider = serviceProvider;
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
            string? recordedAudioFile;
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

                _sessionService = _serviceProvider.GetRequiredService<IAnalyzingSessionService>();
                _ = StartAnalyzingAsync();
            }
        }
    }
}
