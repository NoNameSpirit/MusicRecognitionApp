using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;

namespace MusicRecognitionApp.Controls
{
    public partial class RecordingStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioRecorder _recorderService;

        public RecordingStateControl(
            MainForm mainForm, 
            IAudioRecorder recorderService)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _recorderService = recorderService;

            _recorderService.RecordingProgress += OnRecordingProgress;
        }

        private void BtnStopRecording_Click(object sender, EventArgs e)
        {
            _mainForm.StopRecording();
            _mainForm.SetStateAsync(AppState.Ready);
        }

        private void OnRecordingProgress(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(OnRecordingProgress), progress);
                return;
            }

            ProgressBarRecording.Value = progress;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                ProgressBarRecording.Value = 0;
            }
        }
    }
}
