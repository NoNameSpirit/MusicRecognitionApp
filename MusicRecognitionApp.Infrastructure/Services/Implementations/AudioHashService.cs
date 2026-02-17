using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class AudioHashService : IAudioHashService
    {
        private readonly IAudioHashRepository _audioHashRepository;
        private readonly ILogger<AudioHashService> _logger;

        public AudioHashService(
            IAudioHashRepository audioHashRepository,
            ILogger<AudioHashService> logger)
        {
            _audioHashRepository = audioHashRepository;
            _logger = logger;
        }

        public async Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _audioHashRepository.GetMatchesAsync(hashValues, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting song matches for {HashCount} hashes", hashValues?.Count ?? 0);
                return new List<(int, int)>();
            }
        }
    }
}
