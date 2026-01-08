using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories
{
    public class AudioHashRepository : RepositoryCrud<AudioHashEntity>, IAudioHashRepository
    {
        public AudioHashRepository(MusicRecognitionContext context)
            : base(context)
        {

        }

        public async Task<List<(int SongId, int Count)>> GetMatchesAsync(IEnumerable<uint> queryHashes)
        {
            var result = await Context.Set<AudioHashEntity>()
                .Where(h => queryHashes.Contains(h.Hash))
                .GroupBy(g => g.SongId)
                .Select(el => new { SongId = el.Key, Count = el.Count() })
                .Where(c => c.Count >= 2)
                .OrderByDescending(el => el.Count)
                .Take(5)
                .ToListAsync();

            return result.Select(el => (el.SongId, el.Count)).ToList();
        }
    }
}