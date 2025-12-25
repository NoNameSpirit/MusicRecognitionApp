using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Services.Data.Mappers;
using MusicRecognitionApp.Services.Data.Repositories;
using System.Diagnostics;

namespace MusicRecognitionApp.Services.Data
{
    public class RecognizedSongService : IRecognizedSongService
    {
        private readonly IRecognizedSongRepository _recognizedSongRepository;

        public RecognizedSongService(IRecognizedSongRepository recognizedSongRepository) 
        {
            _recognizedSongRepository = recognizedSongRepository;
        }

        public async Task SaveRecognizedSongAsync(int songId, int matches)
        {
            try
            {
                if (matches < 1)
                {
                    Debug.WriteLine($"[WARN] Not saving recognition with {matches} matches for song {songId}");
                    return;
                }

                var recognizedSong = ModelToEntity.ToRecognizedSongEntity(songId, matches);
                await _recognizedSongRepository.InsertAsync(recognizedSong);
                await _recognizedSongRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while saving recognition: {ex.Message}");
                throw;
            }
        }

        public List<RecognizedSongModel> GetRecognizedSongs()
        {
            try
            {
                return _recognizedSongRepository.GetAllOrderedByDate()
                    .Select(EntityToModel.ToRecognizedSongModel)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while getting recognized songs: {ex.Message}");
                return new List<RecognizedSongModel>();
            }
        }

        public List<ArtistStatisticModel> GetArtistsStatistics()
        {
            try
            {
                return _recognizedSongRepository.GetArtistsStatistics()
                    .Select(EntityToModel.ToArtistStatisticModel)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while getting artists statistics: {ex.Message}");
                return new List<ArtistStatisticModel>();
            }
        }
    }
}
