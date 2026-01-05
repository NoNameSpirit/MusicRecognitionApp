using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Infrastructure.Services.Interfaces
{
    public interface IRecognizedSongService
    {
        Task SaveRecognizedSongAsync(int songId, int matches);
        List<RecognizedSongModel> GetRecognizedSongs();
        List<ArtistStatisticModel> GetArtistsStatistics();
    }
}
