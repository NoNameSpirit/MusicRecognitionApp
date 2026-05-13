using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Dto;
using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations
{
    public class AudioHashRepository : RepositoryCrud<AudioHashEntity>, IAudioHashRepository
    {
        public AudioHashRepository(MusicRecognitionContext context)
            : base(context)
        {

        }

        public async Task<List<SongMatchDto>> GetMatchesAsync(IEnumerable<uint> queryHashes, CancellationToken cancellationToken = default)
        {
            var result = await Context.Set<AudioHashEntity>()
                .Where(h => queryHashes.Contains(h.Hash))
                .GroupBy(g => g.SongId)
                .Select(el => new { SongId = el.Key, Count = el.Count() })
                .Where(c => c.Count >= 2)
                .OrderByDescending(el => el.Count)
                .Take(5)
                .ToListAsync(cancellationToken);

            return result.Select(el => new SongMatchDto() { SongId = el.SongId, Count = el.Count }).ToList();
        }
    }
}