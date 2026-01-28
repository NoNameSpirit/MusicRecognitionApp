using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using System.Diagnostics;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class SongImportService : ISongImportService
    {
        private readonly IAudioHashService _audioHashService;
        private readonly ISongService _songService;
        private readonly ILogger<SongImportService> _logger;
        public SongImportService(
            IAudioHashService audioHashService,
            ISongService songService,
            ILogger<SongImportService> logger)
        {
            _audioHashService = audioHashService;
            _songService = songService;
            _logger = logger;
        }

        public async Task AddSongAsync(string title, string artist, List<AudioHash> hashes)
        {
            try
            {
                var song = await _songService.CreateAsync(title, artist);
                await _audioHashService.AddHashesAsync(hashes, song.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while adding track Title - Artist:", title, artist);
                throw;
            }
        }
    }
}
