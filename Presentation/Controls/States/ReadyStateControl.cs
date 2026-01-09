using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using NAudio.Wave;
using System.Windows.Forms;

namespace MusicRecognitionApp.Controls
{
    public partial class ReadyStateControl : UserControl
    {
        private readonly IAudioRecognition _recognitionService;
        private readonly MainForm _mainForm;
        private readonly IAnimationService _animationService;
        private readonly IMessageBox _messageBox;
        private readonly ISongAddingService _songAddingService;

        private bool _isProcessing = false;
        
        public ReadyStateControl(
            IAudioRecognition recognitionService,
            MainForm mainForm,
            IAnimationService animationService,
            IMessageBox messageBox,
            ISongAddingService songAddingService)
        {
            InitializeComponent();

            _recognitionService = recognitionService;
            _mainForm = mainForm;
            _animationService = animationService;
            _messageBox = messageBox;
            _songAddingService = songAddingService;

            _animationService.AddHoverAnimation(PicRecordingGif);
        }

        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
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

        private async void FABtnAddingTracks_Click(object sender, EventArgs e)
        {
            if (_isProcessing)
            {
                return;
            }

            _isProcessing = true;
            try
            {
                _mainForm.SetStateAsync(AppState.Processing);
                
                ImportResult result = await _songAddingService.ImportTracksFromFolderAsync();

                _mainForm.SetStateAsync(AppState.Ready);

                if (result.Success)
                {
                    _messageBox.ShowInfo(result.Message);
                }
                else if(!string.IsNullOrEmpty(result.Message))
                {
                    _messageBox.ShowError(result.Message);
                }
            }
            finally
            {
                _isProcessing = false;
            }
        }
    }
}
