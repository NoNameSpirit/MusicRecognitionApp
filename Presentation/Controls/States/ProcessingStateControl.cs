using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ProcessingStateControl : UserControl
    {
        private readonly IStateManagerService _stateManagerService;
        private IProcessingAudio? _processingAudio;

        public ProcessingStateControl(IStateManagerService stateManagerService)
        {
            InitializeComponent();
            _stateManagerService = stateManagerService;
        }

        public void SetRecognition(IProcessingAudio processingAudio)
        {
            if (_processingAudio != null)
            {
                _processingAudio.ImportProgress -= UpdateProgress;
            }
            _processingAudio = processingAudio;
            _processingAudio.ImportProgress += UpdateProgress;
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
    }
}
