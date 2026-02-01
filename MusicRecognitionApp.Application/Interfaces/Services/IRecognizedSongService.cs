using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IRecognizedSongService
    {
        Task SaveRecognizedSongAsync(int songId, int matches);
        Task<List<RecognizedSongModel>> GetRecognizedSongsAsync();
        Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync();
    }
}
