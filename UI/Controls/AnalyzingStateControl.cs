using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class AnalyzingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioRecognition _recognitionService;

        public AnalyzingStateControl(
            MainForm mainForm,
            IAudioRecognition recognitionService)
        {
            InitializeComponent();
            _mainForm = mainForm;

            _recognitionService = recognitionService;
            _recognitionService.AnalysisProgress += UpdateProgress;
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
