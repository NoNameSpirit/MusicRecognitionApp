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

        public IEnumerable<AudioHashEntity> GetByHashes(IEnumerable<uint> hashes, params string[] includes)
        {
            return Get(
                filter: h => hashes.Contains(h.Hash),
                includes: includes);
        }
    }
}
