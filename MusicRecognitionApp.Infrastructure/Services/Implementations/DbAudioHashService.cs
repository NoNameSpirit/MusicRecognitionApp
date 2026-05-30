using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Dto;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class DbAudioHashService : IDbAudioHashService
    {
        private readonly IAudioHashRepository _audioHashRepository;
        private readonly ILogger<DbAudioHashService> _logger;

        public DbAudioHashService(
            IAudioHashRepository audioHashRepository,
            ILogger<DbAudioHashService> logger)
        {
            _audioHashRepository = audioHashRepository;
            _logger = logger;
        }

        public async Task<List<SongMatchDto>> FindSongMatchesAsync(List<uint> hashValues, CancellationToken cancellationToken = default)
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
                _logger.LogError(ex, $"Error while getting song matches for {hashValues?.Count ?? 0} hashes");
                return new List<SongMatchDto>();
            }
        }
    }
}
