using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data;

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
            return Get(
                filter: h => hashes.Contains(h.Hash),
                includes: includes);
        }
    }
}
