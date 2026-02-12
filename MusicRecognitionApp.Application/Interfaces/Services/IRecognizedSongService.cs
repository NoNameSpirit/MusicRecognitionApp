using MusicRecognitionApp.Core.Models.Business;
using System.Threading;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IRecognizedSongService
    {
        Task SaveRecognizedSongAsync(int songId, int matches, CancellationToken cancellationToken = default);
        Task<List<RecognizedSongModel>> GetRecognizedSongsAsync();
        Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync();
    }
}
