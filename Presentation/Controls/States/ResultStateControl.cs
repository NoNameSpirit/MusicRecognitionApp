using MaterialSkin.Controls;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Controls
{
    public partial class ResultStateControl : UserControl, IStateWithData
    {
        private List<SearchResultModel> _results;
        
        private readonly IStateManagerService _stateManagerService;
        private readonly IMessageBox _messageBoxService;
        private readonly ICardService _cardService;
        private readonly IRecognitionSongService _recognitionSongService;

        public ResultStateControl(
            IStateManagerService stateManagerService, 
            IMessageBox messageBoxService,
            ICardService cardService,
            IRecognitionSongService recognitionSongService)
        {
            InitializeComponent();

            _stateManagerService = stateManagerService;
            _messageBoxService = messageBoxService;
            _cardService = cardService;
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

            if (_results == null || _results.Count == 0)
            {
                ShowNoResults();
                PicRecordingGif.Image = Properties.Resources.rimuruNoResult;
                return;
            }

            SearchResultModel bestResult = _results.FirstOrDefault()!;
            if (bestResult.Matches > 0)
            {
                _ = SaveRecognizedSong(bestResult);
            }

            ShowResult(bestResult);
        }

        private void ShowNoResults()
        {
            var card = _cardService.CreateNoResultsCard(); 
            PicRecordingGif.Image = Properties.Resources.rimuruNoResult;
            PanelResults.Controls.Add(card);
        }

        private void ShowResult(SearchResultModel result)
        {
            var card = _cardService.CreateResultCard(result); 
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
            _stateManagerService.SetStateAsync(AppState.Library);
        }

        private void BtnLibrary_Click(object sender, EventArgs e)
        {
            _stateManagerService.SetStateAsync(AppState.Library);
        }

        private void BtnBackToReady_Click(object sender, EventArgs e)
        {
            _stateManagerService.SetStateAsync(AppState.Ready);
        }

        public void SetStateData(object? stateData)
        {
            _results = stateData as List<SearchResultModel>;
            DisplayResults();
        }
    }
}
