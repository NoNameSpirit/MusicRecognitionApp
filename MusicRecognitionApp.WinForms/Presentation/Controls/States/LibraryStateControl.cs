using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class LibraryStateControl : UserControl
    {
        private readonly ICardService _cardService;
        private readonly IStateManagerService _stateManagerService;

        public LibraryStateControl(
            IStateManagerService stateManagerService,
            ICardService cardService)
        {
            _stateManagerService = stateManagerService;
            _cardService = cardService;

            InitializeComponent();
        }

        protected async override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                await _cardService.ShowSongsAsync();
            }
        }

        private async void BtnSongs_Click(object sender, EventArgs e)
        {
            FLPanelOfCards.Controls.Clear();

            await _cardService.ShowSongsAsync();
        }

        private async void BtnAuthors_Click(object sender, EventArgs e)
        {
            FLPanelOfCards.Controls.Clear();

            await _cardService.ShowAuthorsAsync();
        }

        private async void FABtnReadyStateControl_Click(object sender, EventArgs e)
        {
            await _stateManagerService.SetStateAsync(AppState.Ready);
        }

        private void LibraryStateControl_Load(object sender, EventArgs e)
        {
            _cardService.Initialize(BtnSongs, BtnAuthors, FLPanelOfCards);
        }
    }
}
