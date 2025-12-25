using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IRecognitionSongService
    {
        Task SaveRecognizedSongsAsync(int songId, int matches);

        List<RecognizedSongModel> GetRecognizedSongs();

        List<ArtistStatisticModel> GetRecognizedArtists();
    }
}
