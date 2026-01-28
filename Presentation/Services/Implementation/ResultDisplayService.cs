using MaterialSkin.Controls;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class ResultDisplayService : IResultDisplayService
    {
        private readonly ICardService _cardService;
        private readonly IRecognitionSongService _recognitionSongService;
        public ResultDisplayService(
            ICardService cardService,
            IRecognitionSongService recognitionSongService)
        {
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

        public async Task DisplayResults(Panel panelResults, PictureBox picRecordingGif, List<SearchResult>? results)
        {
            ClearResults(panelResults);

            if (results == null || results.Count == 0)
            {
                ShowNoResults(panelResults, picRecordingGif);
                return;
            }

            SearchResult bestResult = results.FirstOrDefault()!;
            if (bestResult.Matches > 0)
            {
                await _recognitionSongService.SaveRecognizedSongsAsync(bestResult.Song.Id, bestResult.Matches);
            }

            ShowResult(bestResult, panelResults, picRecordingGif);
        }

        private void ShowNoResults(Panel panelResults, PictureBox picRecordingGif)
        {
            var card = _cardService.CreateNoResultsCard();
            picRecordingGif.Image = Properties.Resources.rimuruNoResult;
            panelResults.Controls.Add(card);
        }

        private void ShowResult(SearchResult result, Panel panelResults, PictureBox picRecordingGif)
        {
            var card = _cardService.CreateResultCard(result);
            picRecordingGif.Image = Properties.Resources.rimuruHasResults;
            panelResults.Controls.Add(card);
        }
    }
}
