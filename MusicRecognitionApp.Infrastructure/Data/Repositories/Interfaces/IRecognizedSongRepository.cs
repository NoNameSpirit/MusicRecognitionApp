using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface IRecognizedSongRepository : IRepositoryCrud<RecognizedSongEntity>
    {
        Task<List<RecognizedSongEntity>> GetRecentAsync(int limit = 10, CancellationToken cancellationToken = default);
        Task<List<RecognizedSongEntity>> GetAllOrderedByDateAsync(CancellationToken cancellationToken = default);
        Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync(string? search = null, CancellationToken cancellationToken = default);
    }
}
