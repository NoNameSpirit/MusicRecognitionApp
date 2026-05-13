using MusicRecognitionApp.Core.Models.Dto;
using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface IAudioHashRepository : IRepositoryCrud<AudioHashEntity>
    {
        Task<List<SongMatchDto>> GetMatchesAsync(IEnumerable<uint> queryHashes, CancellationToken cancellationToken = default);
    }
}
