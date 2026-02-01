using Microsoft.AspNetCore.Components;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;
using System.Linq.Expressions;

namespace MusicRecognitionApp.Blazor.Components.Pages.Library
{
    public partial class StatisticTab : ComponentBase
    {
        [Inject] private IRecognitionSongService RecognitionSongService { get; set; } = null!;
        
        protected List<ArtistStatisticModel>? _artistStats;
        protected string SearchQuery { get; set; } = "";

        protected IEnumerable<ArtistStatisticModel> FilteredStats
        {
            get
            {
                if (_artistStats == null) return Enumerable.Empty<ArtistStatisticModel>();
                if (string.IsNullOrWhiteSpace(SearchQuery))
                    return _artistStats;

                var q = SearchQuery.Trim().ToLowerInvariant();
                return _artistStats.Where(x =>
                    x.Artist.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _artistStats = await RecognitionSongService.GetRecognizedArtistsAsync();
        }
    }
}