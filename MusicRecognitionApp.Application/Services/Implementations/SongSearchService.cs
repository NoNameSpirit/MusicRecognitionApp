using Microsoft.Extensions.Logging;
using MusicRecognitionApp.Application.Interfaces.Services;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class SongSearchService : ISongSearchService
    {
        private readonly ISongService _songService;
        private readonly IAudioHashService _audioHashService;
        private readonly ILogger<SongSearchService> _logger;

        public SongSearchService(
            IAudioHashService audioHashService,
            ISongService songService,
            ILogger<SongSearchService> logger)
        {
            _audioHashService = audioHashService;
            _songService = songService;
            _logger = logger;
        }

        public async Task<List<SearchResult>> SearchSong(List<AudioHash> queryHashes, CancellationToken cancellationToken = default)
        {
            if (queryHashes == null || queryHashes.Count == 0)
                return new List<SearchResult>();

            try
            {
                var hashValues = queryHashes.Select(h => h.Hash).ToList();
                var matches = await _audioHashService.FindSongMatchesAsync(hashValues, cancellationToken);

                var results = new List<SearchResult>();
                foreach (var (songId, count) in matches)
                {
                    var song = await _songService.GetByIdAsync(songId, cancellationToken);

                    if (song != null)
                    {
                        var confidence = (double)count / queryHashes.Count;

                        var result = new SearchResult(song, count, confidence);

                        results.Add(result);
                    }
                }

                List<SearchResult> sortedResults = results
                    .OrderByDescending(r => r.Confidence)
                    .ThenByDescending(r => r.Matches)
                    .ToList();

                return sortedResults;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while searching a song by hashes: ", queryHashes?.Count ?? 0);
                return new List<SearchResult>();
            }
        }
    }
}
