using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecognitionSongService : IRecognitionSongService
    {
        private readonly IRecognizedSongService _recognizedSongService;

        public RecognitionSongService(IRecognizedSongService recognizedSongService)
        {
            _recognizedSongService = recognizedSongService;
        }

        public async Task SaveRecognizedSongsAsync(int songId, int matches, CancellationToken cancellationToken = default)
        {
            await _recognizedSongService.SaveRecognizedSongAsync(songId, matches, cancellationToken);
        }

        public async Task<List<RecognizedSongModel>> GetRecognizedSongsAsync()
        {
            return await _recognizedSongService.GetRecognizedSongsAsync();
        }

        public async Task<List<ArtistStatisticModel>> GetRecognizedArtistsAsync()
        {
            return await _recognizedSongService.GetArtistsStatisticsAsync();
        }
    }
}
