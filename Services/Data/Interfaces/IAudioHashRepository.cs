using MusicRecognitionApp.Core.Models.Entities;

namespace MusicRecognitionApp.Services.Data.Interfaces
{
    public interface IAudioHashRepository : IRepositoryCrud<AudioHashEntity>
    {
        IEnumerable<AudioHashEntity> GetByHashes(IEnumerable<uint> hashes, params string[] includes);
    }
}
