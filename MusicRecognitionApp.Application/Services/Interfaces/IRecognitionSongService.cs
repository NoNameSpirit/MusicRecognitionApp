using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IRecognitionSongService
    {
        Task SaveRecognizedSongsAsync(int songId, int matches, CancellationToken cancellationToken = default);

        Task<List<RecognizedSongModel>> GetRecognizedSongsAsync();

        Task<List<ArtistStatisticModel>> GetRecognizedArtistsAsync();
    }
}
