using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class RecognizedSongService : IRecognizedSongService
    {
        private readonly IRecognizedSongRepository _recognizedSongRepository;
        private readonly ILogger<RecognizedSongService> _logger;

        public RecognizedSongService(
            IRecognizedSongRepository recognizedSongRepository,
            ILogger<RecognizedSongService> logger)
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
                    _logger.LogWarning("Skipping save: {Matches} matches for song {SongId}", matches, songId);
                    return;
                }

                var recognizedSong = ModelToEntity.ToRecognizedSongEntity(songId, matches);
                
                await _recognizedSongRepository.InsertAsync(recognizedSong);
                await _recognizedSongRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save recognized song {SongId} with {Matches} matches", songId, matches);
                throw;
            }
        }

        public async Task<List<RecognizedSongModel>> GetRecognizedSongsAsync()
        {
            try
            {
                var recognizedSongEntity = await _recognizedSongRepository.GetAllOrderedByDateAsync();

                return recognizedSongEntity
                    .Select(EntityToModel.ToRecognizedSongModel)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recognized songs");
                return new List<RecognizedSongModel>();
            }
        }

        public async Task<List<ArtistStatisticModel>> GetArtistsStatisticsAsync()
        {
            try
            {
                return await _recognizedSongRepository.GetArtistsStatisticsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving artist statistics");
                return new List<ArtistStatisticModel>();
            }
        }
    }
}
