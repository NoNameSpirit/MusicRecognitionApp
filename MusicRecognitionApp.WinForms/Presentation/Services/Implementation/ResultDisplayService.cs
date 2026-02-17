using MaterialSkin.Controls;
using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Presentation.Services.Interfaces;
using MusicRecognitionApp.WinForms.Properties;

namespace MusicRecognitionApp.Presentation.Services.Implementation
{
    public class ResultDisplayService : IResultDisplayService
    {
        private readonly ICardService _cardService;
        private readonly IServiceScopeFactory _scopeFactory;

        public ResultDisplayService(
            ICardService cardService,
            IServiceScopeFactory scopeFactory)
        {
            _cardService = cardService;
            _scopeFactory = scopeFactory;
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
            using (var scope = _scopeFactory.CreateScope())
            {
                var _recognitionSongService = scope.ServiceProvider.GetRequiredService<IRecognitionSongService>();

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
        }

        private void ShowNoResults(Panel panelResults, PictureBox picRecordingGif)
        {
            var card = _cardService.CreateNoResultsCard();
            picRecordingGif.Image = Resources.RimuruNoResult;
            panelResults.Controls.Add(card);
        }

        private void ShowResult(SearchResult result, Panel panelResults, PictureBox picRecordingGif)
        {
            var card = _cardService.CreateResultCard(result);
            picRecordingGif.Image = Resources.RimuruHasResults;
            panelResults.Controls.Add(card);
        }
    }
}
