using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface IRecognizedSongRepository : IRepositoryCrud<RecognizedSongEntity>
    {
        Task<List<RecognizedSongEntity>> GetRecentAsync(int limit = 10);
        Task<List<RecognizedSongEntity>> GetAllOrderedByDateAsync();
        Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync();
    }
}
