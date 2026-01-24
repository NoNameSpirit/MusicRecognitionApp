using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ProcessingStateControl : UserControl
    {
        private readonly IStateManagerService _stateManagerService;
        private readonly IProcessingAudio _processingAudio;

        public ProcessingStateControl(
            IStateManagerService stateManagerService,
            IProcessingAudio processingAudio)
        {
            _stateManagerService = stateManagerService;
            _processingAudio = processingAudio;

            InitializeComponent();
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

        private async void BtnStopRecognition_Click(object sender, EventArgs e)
        {
            await _stateManagerService.SetStateAsync(AppState.Ready);
        }

        private void ProcessingStateControl_Load(object sender, EventArgs e)
        {
            _processingAudio.ImportProgress += UpdateProgress;
        }
    }
}
