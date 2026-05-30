using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class SongImportService : ISongImportService
    {
        private readonly IDbSongService _songService;
        private readonly ILogger<SongImportService> _logger;

        public SongImportService(
            IDbSongService songService,
            ILogger<SongImportService> logger)
        {
            _songService = songService;
            _logger = logger;
        }

        public async Task AddSongAsync(string title, string artist, List<AudioHash> hashes, CancellationToken cancellationToken = default)
        {
            try
            {
                await _songService.CreateAsync(title, artist, hashes, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding track Title - Artist:", title, artist);
                throw;
            }
        }
    }
}