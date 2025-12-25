using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;
using MusicRecognitionApp.Infrastructure.Audio.Interfaces;
using MusicRecognitionApp.Infrastructure.Services.Interfaces;
using System.Diagnostics;

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

        public List<SearchResultModel> SearchSong(List<AudioHash> queryHashes)
        {
            if (queryHashes == null || queryHashes.Count == 0)
                return new List<SearchResultModel>();

            try
            {
                var hashValues = queryHashes.Select(h => h.Hash).ToList();
                var databaseDict = _audioHashService.GetHashesDictionary(hashValues);

                var matches = _audioHashGenerator.FindMatches(queryHashes, databaseDict);

                var results = new List<SearchResultModel>();
                foreach (var match in matches)
                {
                    var song = _songService.GetByIdAsync(match.songId).Result;

                    if (song != null)
                    {
                        var result = new SearchResultModel
                        {
                            Song = song,
                            Matches = match.matches,
                            Confidence = match.confidence
                        };

                        results.Add(result);
                    }
                }

                List<SearchResultModel> sortedResults = results
                    .OrderByDescending(r => r.Matches)
                    .ThenByDescending(r => r.Confidence)
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
