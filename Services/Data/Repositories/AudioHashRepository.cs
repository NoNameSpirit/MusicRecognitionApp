using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data;
using MusicRecognitionApp.Services.Data.Interfaces;

namespace MusicRecognitionApp.Services.Data.Repositories
{
    public class AudioHashRepository : RepositoryCrud<AudioHashEntity>, IAudioHashRepository
    {
        public AudioHashRepository(MusicRecognitionContext context) 
            : base(context) 
        {

        }

        public IEnumerable<AudioHashEntity> GetByHashes(IEnumerable<uint> hashes, params string[] includes)
        {
            var query = Context.Set<AudioHashEntity>()
                .Where(h => hashes.Contains(h.Hash));

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }
    }
}
