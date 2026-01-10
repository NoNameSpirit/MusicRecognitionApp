using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecognitionSongService : IRecognitionSongService
    {
        private readonly IRecognizedSongService _recognizedSongService;

        public RecognitionSongService(IRecognizedSongService recognizedSongService)
        {
            _recognizedSongService = recognizedSongService;
        }

        public async Task SaveRecognizedSongsAsync(int songId, int matches)
        {
            await _recognizedSongService.SaveRecognizedSongAsync(songId, matches);
        }

        public List<RecognizedSongModel> GetRecognizedSongs()
        {
            return _recognizedSongService.GetRecognizedSongs();
        }

        public List<ArtistStatisticModel> GetRecognizedArtists()
        {
            return _recognizedSongService.GetArtistsStatistics();
        }
    }
}
