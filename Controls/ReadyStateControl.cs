using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ReadyStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioRecognition _recognitionService;

        public ReadyStateControl(MainForm mainForm, IAudioRecognition recognitionService)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _recognitionService = recognitionService;
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Library);
        }

        private void FABtnSettings_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Settings);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Library);
        }

        private async void BtnStartRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Recording);

            await Task.Delay(500);

            var results = await _recognitionService.RecognizeFromMicrophoneAsync();

            _mainForm.SetRecognitionResults(results);
            _mainForm.SetState(AppState.Result);
        }
    }
}
