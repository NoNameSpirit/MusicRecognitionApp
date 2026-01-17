using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ReadyStateControl : UserControl
    {
        private readonly IStateManagerService _stateManagerService;
        private readonly IAnimationService _animationService;
        private readonly IMessageBox _messageBox;
        private readonly ISongAddingService _songAddingService;

        private bool _isProcessing = false;

        public ReadyStateControl(
            IStateManagerService stateManagerService,
            IAnimationService animationService,
            IMessageBox messageBox,
            ISongAddingService songAddingService)
        {
            _stateManagerService = stateManagerService;
            _animationService = animationService;
            _messageBox = messageBox;
            _songAddingService = songAddingService;

            InitializeComponent();
        }

        private async void FABtnLibrary_Click(object sender, EventArgs e)
        {
            await _stateManagerService.SetStateAsync(AppState.Library);
        }

        private async void BtnLibrary_Click(object sender, EventArgs e)
        {
            await _stateManagerService.SetStateAsync(AppState.Library);
        }

        private async void BtnStartRecognition_Click(object sender, EventArgs e)
        {
            await _stateManagerService.SetStateAsync(AppState.Recording);
        }

        private async void PicRecordingGif_Click(object sender, EventArgs e)
        {
            await _stateManagerService.SetStateAsync(AppState.Recording);
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
                await _stateManagerService.SetStateAsync(AppState.Processing);

                ImportResult result = await _songAddingService.ImportTracksFromFolderAsync();

                if (result.Success)
                {
                    _messageBox.ShowInfo(result.Message);
                }
                else if (!string.IsNullOrEmpty(result.Message))
                {
                    _messageBox.ShowError(result.Message);
                }

                await _stateManagerService.SetStateAsync(AppState.Ready);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void ReadyStateControl_Load(object sender, EventArgs e)
        {
            _animationService.AddHoverAnimation(PicRecordingGif);
        }
    }
}
