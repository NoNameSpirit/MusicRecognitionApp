using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Interfaces
{
    public interface IAudioHashRepository : IRepositoryCrud<AudioHashEntity>
    {
        Task<List<(int SongId, int Count)>> GetMatchesAsync(IEnumerable<uint> queryHashes);
    }
}
