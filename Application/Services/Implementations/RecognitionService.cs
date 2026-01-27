using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class RecognitionService : IRecognitionService
    {
        private readonly IAudioProcessor _audioProcessor;
        private readonly ISpectrogramBuilder _spectrogramBuilder;
        private readonly IPeakDetector _peakDetector;
        private readonly IAudioHashGenerator _hashGenerator;
        private readonly ISongSearchService _searchService;

        public event Action<int> AnalysisProgress;

        public RecognitionService(
            IAudioProcessor audioProcessor,
            ISpectrogramBuilder spectrogramBuilder,
            IPeakDetector peakDetector,
            IAudioHashGenerator hashGenerator,
            ISongSearchService searchService)
        {
            _audioProcessor = audioProcessor;
            _spectrogramBuilder = spectrogramBuilder;
            _peakDetector = peakDetector;
            _hashGenerator = hashGenerator;
            _searchService = searchService;
        }

        public async Task<List<SearchResult>> RecognizeFromMicrophoneAsync(string audioFilePath)
        {
            if (string.IsNullOrEmpty(audioFilePath))
                return new List<SearchResult>();

            AnalysisProgress?.Invoke(10);
            float[] processedAudio = await Task.Run(()
                => _audioProcessor.PreprocessAudio(audioFilePath));

            AnalysisProgress?.Invoke(20);
            SpectrogramData spectrogramData = await Task.Run(()
                => _spectrogramBuilder.ProcessAudio(processedAudio, 11025));

            AnalysisProgress?.Invoke(40);
            List<Peak> allPeaks = await Task.Run(()
                => _peakDetector.ProcessPeekDetector(spectrogramData));

            AnalysisProgress?.Invoke(50);
            List<AudioHash> queryHashes = await Task.Run(()
                => _hashGenerator.GenerateHashes(allPeaks));

            AnalysisProgress?.Invoke(70);
            var results = await _searchService.SearchSong(queryHashes);

            AnalysisProgress?.Invoke(100);
            return results;
        }
    }
}
