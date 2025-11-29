using MaterialSkin.Controls;
using MusicRecognitionApp.Forms;
using MusicRecognitionApp.Model.Enums;
using MusicRecognitionApp.Services.Interfaces;
using MusicRecognitionApp.Services.UI;
using MusicRecognitionApp.Services.UI.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ResultStateControl : UserControl
    {
        private readonly MainForm _mainForm;
        private readonly IAudioDatabase _databaseService;
        private readonly IMessageBox _messageBoxService;
        private readonly IResultCardBuilder _resultCardBuilder;

        public ResultStateControl(
            MainForm mainForm, 
            IAudioDatabase databaseService,
            IMessageBox messageBoxService,
            IResultCardBuilder resultCardBuilder)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _databaseService = databaseService;
            _messageBoxService = messageBoxService;
            _resultCardBuilder = resultCardBuilder;
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
            if (bestResult.matches > 0)
            {
                SaveRecognizedSong(bestResult);
            }

            ShowResult(bestResult);
        }

        private void ShowNoResults()
        {
            var card = _resultCardBuilder.CreateNoResultsCard(); 
            PicRecordingGif.Image = Properties.Resources.rimuruNoResult;
            PanelResults.Controls.Add(card);
        }

        private void ShowResult((int songId, string title, string artist, int matches, double confidence) result)
        {
            var card = _resultCardBuilder.CreateResultCard(result); 
            PicRecordingGif.Image = Properties.Resources.rimuruHasResults;
            PanelResults.Controls.Add(card);
        }

        private void SaveRecognizedSong((int songId, string title, string artist, int matches, double confidence) result)
        {
            try
            {
                _databaseService
                    .SaveRecognizedSongs(result.songId, result.title, result.artist, result.matches);
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
