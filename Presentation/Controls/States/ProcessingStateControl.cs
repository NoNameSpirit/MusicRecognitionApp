using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Controls
{
    public partial class ProcessingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private CancellationTokenSource _cancellationTokenSource;
        private IAudioRecognition? _recognition;

        public ProcessingStateControl(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        public void SetRecognition(IAudioRecognition recognition)
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

        private void BtnStopRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Ready);
        }
    }
}
