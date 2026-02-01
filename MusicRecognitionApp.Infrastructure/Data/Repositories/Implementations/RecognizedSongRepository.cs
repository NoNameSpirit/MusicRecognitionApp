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

        public async Task<List<RecognizedSongEntity>> GetRecentAsync(int limit = 10)
        {
            return await GetAsync(
                orderBy: q => q.OrderByDescending(r => r.RecognitionDate),
                take: limit,
                includes: "Song");
        }

        public async Task<List<RecognizedSongEntity>> GetAllOrderedByDateAsync()
        {
            return await GetAsync(
                orderBy: q => q.OrderByDescending(r => r.RecognitionDate),
                includes: "Song");
        }

        public async Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync()
        {
            return await Context.Set<RecognizedSongEntity>()
                .Include(r => r.Song)
                .GroupBy(r => r.Song.Artist)
                .Select(g => new ArtistStatisticModel
                {
                    Artist = g.Key,
                    SongCount = g.Count()
                })
                .ToListAsync();
        }
    }
}
