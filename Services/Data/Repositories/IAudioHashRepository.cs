using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data.Interfaces;

namespace MusicRecognitionApp.Services.Data.Repositories
{
    public interface IAudioHashRepository : IRepositoryCrud<AudioHashEntity>
    {
        IEnumerable<AudioHashEntity> GetByHashes(IEnumerable<uint> hashes, params string[] includes);
    }
}
