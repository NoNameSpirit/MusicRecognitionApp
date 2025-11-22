using MaterialSkin.Controls;
using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class LibraryStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly ICardService _cardService;

        public LibraryStateControl(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _cardService = new CardService(BtnSongs, BtnAuthors, FLPanelOfCards); // UI components, didn't use DI 

            _cardService.ShowSongs();
        }

        private void BtnSongs_Click(object sender, EventArgs e)
        {
            _cardService.ShowSongs();
        }

        private void BtnAuthors_Click(object sender, EventArgs e)
        {
            _cardService.ShowAuthors();
        }

        private void FABtnReadyStateControl_Click(object sender, EventArgs e)
        {
            _mainForm.SetState(AppState.Ready);
        }
    }
}
