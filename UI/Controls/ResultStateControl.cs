using MaterialSkin.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Services.Data.Interfaces;
using MusicRecognitionApp.Services.History;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Services.UI;
using MusicRecognitionApp.Services.UI.Interfaces;
using System.DirectoryServices;

namespace MusicRecognitionApp.Controls
{
    public partial class ResultStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IMessageBox _messageBoxService;
        private readonly IResultCardBuilder _resultCardBuilder;
        private readonly IRecognitionSongService _recognitionSongService;

        public ResultStateControl(
            MainForm mainForm, 
            IMessageBox messageBoxService,
            IResultCardBuilder resultCardBuilder,
            IRecognitionSongService recognitionSongService)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _messageBoxService = messageBoxService;
            _resultCardBuilder = resultCardBuilder;
            _recognitionSongService = recognitionSongService;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                DisplayResults();
            }
            else
            {
                ClearResults();
            }
        }

        private void ClearResults()
        {
            foreach (var control in PanelResults.Controls.OfType<MaterialCard>().ToArray())
            {
                control.Dispose();
            }
            PanelResults.Controls.Clear();
        }

        private void DisplayResults()
        {
            ClearResults();

            var results = _mainForm.RecognitionResults;

            if (results == null || results.Count == 0)
            {
                ShowNoResults();
                PicRecordingGif.Image = Properties.Resources.rimuruNoResult;
                return;
            }

            var bestResult = results.FirstOrDefault();
            if (bestResult.Matches > 0)
            {
                //Don't wait
                _ = SaveRecognizedSong(bestResult);
            }

            ShowResult(bestResult);
        }

        private void ShowNoResults()
        {
            var card = _resultCardBuilder.CreateNoResultsCard(); 
            PicRecordingGif.Image = Properties.Resources.rimuruNoResult;
            PanelResults.Controls.Add(card);
        }

        private void ShowResult(SearchResultModel result)
        {
            var card = _resultCardBuilder.CreateResultCard(result); 
            PicRecordingGif.Image = Properties.Resources.rimuruHasResults;
            PanelResults.Controls.Add(card);
        }

        private async Task SaveRecognizedSong(SearchResultModel result)
        {
            try
            {
                var temp = result.Song.Id;
                var kek = result.Song.Id;

                await _recognitionSongService
                    .SaveRecognizedSongsAsync(result.Song.Id, result.Matches);
            }
            catch (Exception ex)
            {
                _messageBoxService
                    .ShowError($"Ошибка при сохранении распознанного трека: {ex.Message}");
            }
        }


        private void FABtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Library);
        }

        private void BtnBackToReady_Click(object sender, EventArgs e)
        {
            _mainForm.SetStateAsync(AppState.Ready);
        }
    }
}
