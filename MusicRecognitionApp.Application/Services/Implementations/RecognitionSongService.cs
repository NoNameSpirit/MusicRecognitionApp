using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecognitionSongService : IRecognitionSongService
    {
        private readonly IDbRecognizedSongService _recognizedSongService;

        public RecognitionSongService(IDbRecognizedSongService recognizedSongService)
        {
            _recognizedSongService = recognizedSongService;
        }

        public async Task SaveRecognizedSongAsync(int songId, int matches, CancellationToken cancellationToken = default)
        {
            await _recognizedSongService.SaveRecognizedSongAsync(songId, matches, cancellationToken);
        }

        public async Task<List<RecognizedSongModel>> GetRecognizedSongsAsync(CancellationToken cancellationToken = default)
        {
            return await _recognizedSongService.GetRecognizedSongsAsync(cancellationToken);
        }

        public async Task<List<ArtistStatisticModel>> GetRecognizedArtistsAsync(string? search = null, CancellationToken cancellationToken = default)
        {
            return await _recognizedSongService.GetArtistsStatisticsAsync(search, cancellationToken);
        }
    }
}
