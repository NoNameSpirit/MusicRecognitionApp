using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Interfaces.UnitOfWork;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Data.Mappers;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Services.Implementations
{
    public class RecognizedSongService : IRecognizedSongService
    {
        private readonly IRecognizedSongRepository _recognizedSongRepository;
        private readonly ILogger<RecognizedSongService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RecognizedSongService(
            IRecognizedSongRepository recognizedSongRepository,
            ILogger<RecognizedSongService> logger,
            IUnitOfWork unitOfWork)
        {
            _recognizedSongRepository = recognizedSongRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
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

                await _recognizedSongRepository.InsertAsync(recognizedSong, cancellationToken);
                await _recognizedSongRepository.SaveChangesAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                _unitOfWork.Clear();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save recognized song {SongId} with {Matches} matches", songId, matches);
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
