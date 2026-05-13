using Microsoft.AspNetCore.Components;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Blazor.Components.Pages.Library
{
    public partial class StatisticTab : CancellableComponentBase
    {
        [Inject] private IRecognitionSongService RecognitionSongService { get; set; } = null!;

        protected List<ArtistStatisticModel>? _artistStats;
        protected string SearchQuery { get; set; } = "";

        protected async override Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task OnSearchAsync(string text)
        {
            SearchQuery = text;
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            _artistStats = await RecognitionSongService.GetRecognizedArtistsAsync(SearchQuery, Ct);
        }
    }
}