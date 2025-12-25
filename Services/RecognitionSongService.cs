using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Services.Data;
using MusicRecognitionApp.Services.Data.Mappers;
using MusicRecognitionApp.Services.Data.Repositories;
using System.Diagnostics;

namespace MusicRecognitionApp.Services.History
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
