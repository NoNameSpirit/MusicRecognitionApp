using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Data.Interfaces;
using MusicRecognitionApp.Services.Data.Mappers;
using MusicRecognitionApp.Services.Interfaces;
using NAudio.Wave;
using System.Data.SQLite;
using System.Diagnostics;

namespace MusicRecognitionApp.Services
{
    public class AudioDatabaseService : IAudioDatabase
    {
        private readonly MusicRecognitionContext _context;
        private readonly ISongRepository _songRepository;
        private readonly IAudioHashRepository _audioHashRepository;
        private readonly IRecognizedSongRepository _recognizedSongRepository;

        public AudioDatabaseService(
            MusicRecognitionContext context,
            ISongRepository songRepository,
            IAudioHashRepository audioHashRepository,
            IRecognizedSongRepository recognizedSongRepository)
        {
            _context = context;
            _songRepository = songRepository;
            _audioHashRepository = audioHashRepository;
            _recognizedSongRepository = recognizedSongRepository;

            _context.Database.EnsureCreated();

            if (!TestConnection())
            {
                Debug.WriteLine("[ERROR] Can't connect to the Db");
            }
            else
            {
                Debug.WriteLine("[INFO] Connection to the db success");
            }
        }

        public List<SearchResultModel> SearchSong(List<AudioHash> queryHashes)
        {
            if (queryHashes == null || queryHashes.Count == 0)
                return new List<SearchResultModel>();

            try
            {
                var hashGenerator = new AudioHashGenerator();
                var hashValues = queryHashes
                    .Select(h => h.Hash)
                    .ToList();

                var matchingHashes = _audioHashRepository.GetByHashes(hashValues, "Song");

                var convertedHashes = matchingHashes
                    .Select(EntityToModel.ToAudioHash)
                    .ToList();

                var databaseDict = convertedHashes
                    .GroupBy(h => h.Hash)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var matches = hashGenerator.FindMatches(queryHashes, databaseDict);
                var results = new List<SearchResultModel>();

                foreach (var match in matches)
                {
                    var songEntity = _songRepository.GetByIdAsync(match.songId).Result;
                    
                    if (songEntity != null)
                    {
                        var songModel = EntityToModel.ToSearchResultModel(songEntity, match.matches, match.confidence);
                        results.Add(songModel);
                    }
                }

                return results.OrderByDescending(r => r.Matches)
                       .ThenByDescending(r => r.Confidence)
                       .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while searching a song: {ex.Message}");
                return new List<SearchResultModel>();
            }
        }

        public async Task SaveRecognizedSongsAsync(int songId, int matches)
        {
            try
            {
                var recognizedSong = ModelToEntity.ToRecognizedSongEntity(songId, matches);

                await _recognizedSongRepository.InsertAsync(recognizedSong);
                await _recognizedSongRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while saving: {ex.Message}");
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

        public List<ArtistStatisticModel> GetRecognizedArtists()
        {
            try
            {
                return _recognizedSongRepository.GetArtistsStatistics()
                    .Select(EntityToModel.ToArtistStatisticModel)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Ошибка при получении статистики артистов: {ex.Message}");
                return new List<ArtistStatisticModel>();
            }
        }

        public async Task AddSongWithHashesAsync(string title, string artist, List<AudioHash> hashes)
        {
            try
            {
                var song = await _songRepository.GetSongByTitleAndArtistAsync(title, artist);
                
                if (song != null)
                {
                    Debug.WriteLine($"[ERROR] This song already exist in Database!");
                    return;
                }

                var newSong = new SongEntity
                {
                    Title = title,
                    Artist = artist,
                };

                await _songRepository.InsertAsync(newSong);
                await _songRepository.SaveChangesAsync();

                foreach (var hash in hashes)
                {
                    var audioHashEntity = EntityToModel.ToAudioHashEntity(hash, newSong.Id);

                    await _audioHashRepository.InsertAsync(audioHashEntity);
                }

                await _audioHashRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while adding track: {ex}");
                throw;
            }
        }

        public bool TestConnection()
        {
            try
            {
                return _context.Database.CanConnect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Ошибка подключения к БД: {ex.Message}");
                return false;
            }
        }
    }
}
