using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IDbRecognizedSongService
    {
        Task SaveRecognizedSongAsync(int songId, int matches, CancellationToken cancellationToken = default);
        Task<List<RecognizedSongModel>> GetRecognizedSongsAsync(CancellationToken cancellationToken = default);
        Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync(string? search = null, CancellationToken cancellationToken = default);
    }
}
