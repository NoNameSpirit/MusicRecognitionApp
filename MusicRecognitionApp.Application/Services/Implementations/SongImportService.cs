using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Interfaces.UnitOfWork;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class SongImportService : ISongImportService
    {
        private readonly IAudioHashService _audioHashService;
        private readonly ISongService _songService;
        private readonly ILogger<SongImportService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public SongImportService(
            IAudioHashService audioHashService,
            ISongService songService,
            ILogger<SongImportService> logger,
            IUnitOfWork unitOfWork)
        {
            _audioHashService = audioHashService;
            _songService = songService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task AddSongAsync(string title, string artist, List<AudioHash> hashes, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _songService.CreateAsync(title, artist, hashes, cancellationToken);
               
                if (result.IsNew)
                {
                    await _unitOfWork.SaveAsync(cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                _unitOfWork.Clear(); 
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
