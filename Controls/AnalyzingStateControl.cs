using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class AnalyzingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioRecorder _recorderService;

        public AnalyzingStateControl(MainForm mainForm, IAudioRecorder recorderService)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _recorderService = recorderService;

            _recorderService.RecordingProgress += OnRecordingProgress;
        }

        private void OnRecordingProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(OnRecordingProgress), progress);
                return;
            }

            ProgressBarAnalyzing.Value = progress;
            LblProgressPercent.Text = $"{progress}%";
        }

        private void BtnStopRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Ready);
        }
    }
}
