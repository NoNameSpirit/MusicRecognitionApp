using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Data;

namespace MusicRecognitionApp.Services.History
{
    public interface IRecognitionSongService
    {
        Task SaveRecognizedSongsAsync(int songId, int matches);

        List<RecognizedSongModel> GetRecognizedSongs();

        List<ArtistStatisticModel> GetRecognizedArtists();
    }
}
