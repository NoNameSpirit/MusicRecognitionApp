using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class LibraryStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly ICardService _cardService;

        public LibraryStateControl(
            MainForm mainForm,
            ICardService cardService)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _cardService = cardService;
            
            _cardService.Initialize(BtnSongs, BtnAuthors, FLPanelOfCards);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                _cardService.ShowSongs();
            }
        }

        private void BtnSongs_Click(object sender, EventArgs e)
        {
            FLPanelOfCards.Controls.Clear();

            _cardService.ShowSongs();
        }

        private void BtnAuthors_Click(object sender, EventArgs e)
        {
            FLPanelOfCards.Controls.Clear();

            _cardService.ShowAuthors();
        }

        private void FABtnReadyStateControl_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Ready);
        }
    }
}
