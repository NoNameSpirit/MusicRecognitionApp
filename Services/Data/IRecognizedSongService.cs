using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Services.Data
{
    public interface IRecognizedSongService
    {
        Task SaveRecognizedSongAsync(int songId, int matches);
        List<RecognizedSongModel> GetRecognizedSongs();
        List<ArtistStatisticModel> GetArtistsStatistics();
    }
}
