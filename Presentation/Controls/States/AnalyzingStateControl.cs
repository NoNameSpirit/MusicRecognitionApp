using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Controls
{
    public partial class AnalyzingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private IAudioRecognition _recognition;

        public AnalyzingStateControl(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        public void SetRecognition(IAudioRecognition recognition)
        {
            if(_recognition != null)
            {
                _recognition.AnalysisProgress -= UpdateProgress;
            }
            _recognition = recognition;
            _recognition.AnalysisProgress += UpdateProgress;
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
