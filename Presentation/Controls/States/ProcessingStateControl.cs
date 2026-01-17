using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ProcessingStateControl : UserControl
    {
        private readonly IStateManagerService _stateManagerService;
        private CancellationTokenSource _cancellationTokenSource;
        private IAudioRecognitionService? _recognition;

        public ProcessingStateControl(IStateManagerService stateManagerService)
        {
            InitializeComponent();
            _stateManagerService = stateManagerService;
        }

        public void SetRecognition(IAudioRecognitionService recognition)
        {
            if (_recognition != null)
            {
                _recognition.ImportProgress -= UpdateProgress;
            }
            _recognition = recognition;
            _recognition.ImportProgress += UpdateProgress;
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
