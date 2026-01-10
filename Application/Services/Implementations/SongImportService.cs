using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using System.Diagnostics;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class SongImportService : ISongImportService
    {
        private readonly IAudioHashService _audioHashService;
        private readonly ISongService _songService;
        public SongImportService(
            IAudioHashService audioHashService,
            ISongService songService)
        {
            _audioHashService = audioHashService;
            _songService = songService;
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
                Debug.WriteLine($"[ERROR] Exception while adding track: {ex}");
                throw;
            }
        }
    }
}
