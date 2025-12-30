using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ProcessingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioRecognition _recognitionService;
        private CancellationTokenSource _cancellationTokenSource;

        public ProcessingStateControl(
            MainForm mainForm,
            IAudioRecognition recognitionService)
        {
            InitializeComponent();
            _mainForm = mainForm;

            _recognitionService = recognitionService;
            _recognitionService.ImportProgress += UpdateProgress;
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

        private void BtnStopRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Ready);
        }
    }
}
