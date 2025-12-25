using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Interfaces
{
    public interface IAudioHashRepository : IRepositoryCrud<AudioHashEntity>
    {
        IEnumerable<AudioHashEntity> GetByHashes(IEnumerable<uint> hashes, params string[] includes);
    }
}
