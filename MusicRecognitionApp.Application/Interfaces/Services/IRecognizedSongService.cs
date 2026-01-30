using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IRecognizedSongService
    {
        Task SaveRecognizedSongAsync(int songId, int matches);
        List<RecognizedSongModel> GetRecognizedSongs();
        List<ArtistStatisticModel> GetArtistsStatistics();
    }
}
