using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
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

        public async Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues)
        {
            try
            {
                return await _audioHashRepository.GetMatchesAsync(hashValues);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting song matches for {HashCount} hashes", hashValues?.Count ?? 0);
                return new List<(int, int)>();
            }
        }

        public async Task AddHashesAsync(List<AudioHash> hashes, int songId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (hashes == null || hashes.Count == 0)
                {
                    _logger.LogWarning("No hashes to add for song {SongId}", songId);
                    return;
                }

                foreach (AudioHash hash in hashes)
                {
                    var entity = EntityToModel.ToAudioHashEntity(hash, songId);
                    await _audioHashRepository.InsertAsync(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding hashes for song {SongId}", songId);
                throw;
            }
        }
    }
}
