using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Audio.Interfaces;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class SongSearchService : ISongSearchService
    {
        private readonly ISongService _songService;
        private readonly IAudioHashService _audioHashService;
        private readonly IAudioHashGenerator _audioHashGenerator;

        public SongSearchService(
            IAudioHashService audioHashService,
            ISongService songService,
            IAudioHashGenerator audioHashGenerator)
        {
            _audioHashService = audioHashService;
            _songService = songService;
            _audioHashGenerator = audioHashGenerator;
        }

        public async Task<List<SearchResultModel>> SearchSong(List<AudioHash> queryHashes)
        {
            if (queryHashes == null || queryHashes.Count == 0)
                return new List<SearchResultModel>();

            try
            {
                var hashValues = queryHashes.Select(h => h.Hash).ToList();
                var matches = await _audioHashService.FindSongMatchesAsync(hashValues);

                var results = new List<SearchResultModel>();
                foreach (var (songId, count) in matches)
                {
                    var song = await _songService.GetByIdAsync(songId);

                    if (song != null)
                    {
                        var result = new SearchResultModel
                        {
                            Song = song,
                            Matches = count,
                            Confidence = count / queryHashes.Count
                        };

                        results.Add(result);
                    }
                }

                List<SearchResultModel> sortedResults = results
                    .OrderByDescending(r => r.Confidence)
                    .ThenByDescending(r => r.Matches)
                    .ToList();

                return sortedResults;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception while searching a song: {ex.Message}");
                return new List<SearchResultModel>();
            }
        }
    }
}
