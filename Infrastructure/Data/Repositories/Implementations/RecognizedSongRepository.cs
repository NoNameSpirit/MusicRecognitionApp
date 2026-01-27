using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations
{
    public class RecognizedSongRepository : RepositoryCrud<RecognizedSongEntity>, IRecognizedSongRepository
    {
        public RecognizedSongRepository(MusicRecognitionContext context)
            : base(context)
        {

        }

        public IEnumerable<RecognizedSongEntity> GetRecent(int limit = 10)
        {
            return Get(
                orderBy: q => q.OrderByDescending(r => r.RecognitionDate),
                take: limit,
                includes: "Song");
        }

        public IEnumerable<RecognizedSongEntity> GetAllOrderedByDate()
        {
            return Get(
                orderBy: q => q.OrderByDescending(r => r.RecognitionDate),
                includes: "Song");
        }

        public IEnumerable<(string Artist, int SongCount)> GetArtistsStatistics()
        {
            return Context.Set<RecognizedSongEntity>()
                .Include(r => r.Song)
                .AsEnumerable()
                .GroupBy(r => r.Song.Artist)
                .Select(g => (Artist: g.Key, SongCount: g.Count()))
                .OrderByDescending(g => g.SongCount)
                .ToList();
        }
    }
}
