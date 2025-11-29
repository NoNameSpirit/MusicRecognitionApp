using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Services.UI;
using MusicRecognitionApp.Services.UI.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace MusicRecognitionApp.Controls
{
    public partial class ReadyStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioRecognition _recognitionService;
        private readonly IAnimationService _animationService;
        
        public ReadyStateControl(
            MainForm mainForm, 
            IAudioRecognition recognitionService,
            IAnimationService animationService)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _recognitionService = recognitionService;
            _animationService = animationService;

            _animationService.AddHoverAnimation(PicRecordingGif);
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
        }

        private void FABtnSettings_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Settings);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
        }

        private void BtnStartRecognition_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Recording);
        }

        private void PicRecordingGif_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Recording);
        }
    }
}
