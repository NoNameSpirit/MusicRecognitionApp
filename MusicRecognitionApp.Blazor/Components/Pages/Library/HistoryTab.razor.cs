using Microsoft.AspNetCore.Components;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Blazor.Components.Pages.Library
{
    public partial class HistoryTab : ComponentBase
    {
        [Inject] private IRecognitionSongService RecognitionSongService { get; set; } = null!;

        protected List<RecognizedSongModel> _recognizedSongs;
        protected string SearchQuery { get; set; } = "";

        protected IEnumerable<RecognizedSongModel> FilteredSongs
        {
            get 
            {
                if (_recognizedSongs == null)
                    return Enumerable.Empty<RecognizedSongModel>();
                if (string.IsNullOrEmpty(SearchQuery))
                    return _recognizedSongs;

                var q = SearchQuery.Trim().ToLowerInvariant();
                return _recognizedSongs.Where(x =>
                    x.Song.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                    x.Song.Artist.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _recognizedSongs = await RecognitionSongService.GetRecognizedSongsAsync();
        }
    }
}
