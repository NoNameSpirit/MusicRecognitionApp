using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Business;
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

        public async Task<List<RecognizedSongEntity>> GetRecentAsync(int limit = 10, CancellationToken cancellationToken = default)
        {
            return await GetAsync(
                orderBy: q => q.OrderByDescending(r => r.RecognitionDate),
                take: limit,
                cancellationToken: cancellationToken,
                includes: "Song");
        }

        public async Task<List<RecognizedSongEntity>> GetAllOrderedByDateAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync(
                orderBy: q => q.OrderByDescending(r => r.RecognitionDate),
                cancellationToken: cancellationToken,
                includes: "Song");
        }

        public async Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync(string? search = null, CancellationToken cancellationToken = default)
        {
            IQueryable<RecognizedSongEntity> query = Context.Set<RecognizedSongEntity>()
                .Include(r => r.Song);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(r => r.Song.Artist.Contains(search));

            return await query
                .GroupBy(r => r.Song.Artist)
                .Select(g => new ArtistStatisticModel
                {
                    Artist = g.Key,
                    SongCount = g.Count()
                })
                .OrderByDescending(x => x.SongCount)
                .ToListAsync(cancellationToken);
        }
    }
}
