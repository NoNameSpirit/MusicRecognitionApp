using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class DbRecognizedSongService : IDbRecognizedSongService
    {
        private readonly IRecognizedSongRepository _recognizedSongRepository;
        private readonly ILogger<DbRecognizedSongService> _logger;

        public DbRecognizedSongService(
            IRecognizedSongRepository recognizedSongRepository,
            ILogger<DbRecognizedSongService> logger)
        {
            _recognizedSongRepository = recognizedSongRepository;
            _logger = logger;
        }

        public async Task SaveRecognizedSongAsync(int songId, int matches, CancellationToken cancellationToken = default)
        {
            try
            {
                if (matches < 1)
                {
                    _logger.LogWarning($"Skipping save: {matches} matches for song {songId}");
                    return;
                }

                var recognizedSong = ModelToEntity.ToRecognizedSongEntity(songId, matches);

                await _recognizedSongRepository.InsertAsync(recognizedSong, cancellationToken);
                await _recognizedSongRepository.SaveChangesAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to save recognized song {songId} with {matches} matches");
                throw;
            }
        }

        public async Task<List<RecognizedSongModel>> GetRecognizedSongsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var recognizedSongEntity = await _recognizedSongRepository.GetAllOrderedByDateAsync(cancellationToken);

                return recognizedSongEntity
                    .Select(EntityToModel.ToRecognizedSongModel)
                    .ToList();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recognized songs");
                return new List<RecognizedSongModel>();
            }
        }

        public async Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync(string? search = null, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _recognizedSongRepository.GetArtistsStatisticsAsync(search, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving artist statistics");
                return new List<ArtistStatisticModel>();
            }
        }
    }
}
