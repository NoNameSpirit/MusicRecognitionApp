using Microsoft.AspNetCore.Components;
using MusicRecognitionApp.Blazor.Components.Pages.Table.Model;
using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Blazor.Components.Pages.Library
{
    public partial class HistoryTab : CancellableComponentBase
    {
        [Inject] private ITableDetailProvider<RecognizedSongEntity> Provider { get; set; } = null!;

        private IEnumerable<RecognizedSongEntity>? _items;
        private string? _searchQuery = null;

        protected override void OnInitialized()
        {
            LoadData();
        }

        private void OnSearch(string text)
        {
            _searchQuery = text;
            LoadData();
        }

        private void LoadData()
        {
            var query = Provider.GetQueryableAll();
            query = Provider.SearchByName(query, _searchQuery);
            _items = query.ToList();
        }
    }
}