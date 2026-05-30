using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Blazor.Components.Pages.Table.Model;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Blazor.Components.Pages.Table.PageTableProvider
{
    public class TrackDetailProvider : ITableDetailProvider<RecognizedSongEntity>
    {
        private readonly MusicRecognitionContext _context;

        public TrackDetailProvider(MusicRecognitionContext context)
        {
            _context = context;
        }

        public IReadOnlyList<ListTableColumn> Columns => new List<ListTableColumn>()
        {
            new ListTableColumn("Title", 40, e => ((RecognizedSongEntity)e).Song.Title),
            new ListTableColumn("Artist", 30, e => ((RecognizedSongEntity)e).Song.Artist),
            new ListTableColumn("Title", 20, e => ((RecognizedSongEntity)e).RecognitionDate.ToString("g")),
            new ListTableColumn("Title", 10, e => ((RecognizedSongEntity)e).Matches)
        };

        public IQueryable<RecognizedSongEntity> GetQueryableAll()
        {
            return _context.RecognizedSongs
                .Include(r => r.Song);
        }
        public async Task Remove(int id)
        {
            var removedElement = await _context.RecognizedSongs.FindAsync(id);
            if (removedElement != null)
            {
                _context.RecognizedSongs.Remove(removedElement);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<RecognizedSongEntity> SearchByName(IQueryable<RecognizedSongEntity> items, string? searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return items;

            return items.Where(r =>
                r.Song.Title.Contains(searchString) ||
                r.Song.Artist.Contains(searchString));
        }
    }
}
