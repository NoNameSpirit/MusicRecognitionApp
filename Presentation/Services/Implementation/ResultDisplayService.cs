using MaterialSkin.Controls;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class ResultDisplayService : IResultDisplayService
    {
        private readonly IMessageBox _messageBoxService;
        private readonly ICardService _cardService;
        private readonly IRecognitionSongService _recognitionSongService;
        public ResultDisplayService(
            IMessageBox messageBoxService,
            ICardService cardService,
            IRecognitionSongService recognitionSongService)
        {
            _messageBoxService = messageBoxService;
            _cardService = cardService;
            _recognitionSongService = recognitionSongService;
        }

        public void ClearResults(Panel panelResults)
        {
            foreach (var control in panelResults.Controls.OfType<MaterialCard>().ToArray())
            {
                control.Dispose();
            }
            panelResults.Controls.Clear();
        }

        public async Task DisplayResults(Panel panelResults, PictureBox picRecordingGif, List<SearchResultModel>? results)
        {
            ClearResults(panelResults);

            if (results == null || results.Count == 0)
            {
                ShowNoResults(panelResults, picRecordingGif);
                return;
            }

            SearchResultModel bestResult = results.FirstOrDefault()!;
            if (bestResult.Matches > 0)
            {
                try
                {
                    await _recognitionSongService
                        .SaveRecognizedSongsAsync(bestResult.Song.Id, bestResult.Matches);
                }
                catch (Exception ex)
                {
                    _messageBoxService
                        .ShowError($"Error saving a recognized track: {ex.Message}");
                }
            }

            ShowResult(bestResult, panelResults, picRecordingGif);
        }

        private void ShowNoResults(Panel panelResults, PictureBox picRecordingGif)
        {
            var card = _cardService.CreateNoResultsCard();
            picRecordingGif.Image = Properties.Resources.rimuruNoResult;
            panelResults.Controls.Add(card);
        }

        private void ShowResult(SearchResultModel result, Panel panelResults, PictureBox picRecordingGif)
        {
            var card = _cardService.CreateResultCard(result);
            picRecordingGif.Image = Properties.Resources.rimuruHasResults;
            panelResults.Controls.Add(card);
        }
    }
}
