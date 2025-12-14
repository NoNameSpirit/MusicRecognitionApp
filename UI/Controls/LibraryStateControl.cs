using MaterialSkin.Controls;
using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services;
using MusicRecognitionApp.Services.Interfaces;
using System;

namespace MusicRecognitionApp.Controls
{
    public partial class LibraryStateControl : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MainForm _mainForm;
        private readonly ICardService _cardService;

        public LibraryStateControl(
            MainForm mainForm, 
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _mainForm = mainForm;
            _cardService = _serviceProvider.GetRequiredService<ICardService>();
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
            _cardService.ShowSongs();
        }

        private void BtnAuthors_Click(object sender, EventArgs e)
        {
            _cardService.ShowAuthors();
        }

        private void FABtnReadyStateControl_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Ready);
        }
    }
}
