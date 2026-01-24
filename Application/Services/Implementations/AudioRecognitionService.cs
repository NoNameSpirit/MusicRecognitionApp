using MusicRecognitionApp.Application.Interfaces.Audio;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AudioRecognitionService : IAudioRecognitionService
    {
        private readonly IAudioProcessor _audioProcessor;
        private readonly ISpectrogramBuilder _spectrogramBuilder;
        private readonly IPeakDetector _peakDetector;
        private readonly IAudioHashGenerator _hashGenerator;
        private readonly ISongSearchService _searchService;

        public event Action<int> AnalysisProgress;

        public AudioRecognitionService(
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

        public async Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath)
        {
            if (string.IsNullOrEmpty(audioFilePath))
                return new List<SearchResultModel>();

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
