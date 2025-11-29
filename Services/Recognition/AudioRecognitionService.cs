using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Interfaces;
using System.Threading;

namespace MusicRecognitionApp.Services
{
    public class AudioRecognitionService : IAudioRecognition
    {
        private readonly IAudioDatabase _databaseService;
        private readonly IAudioRecorder _recorderService;
        private readonly IAudioProcessor _audioProcessor;
        private readonly ISpectrogramBuilder _spectrogramBuilder;
        private readonly IPeakDetector _peakDetector;
        private readonly IAudioHashGenerator _hashGenerator;

        public event Action<int> AnalysisProgress;

        public AudioRecognitionService(
            IAudioDatabase databaseService,
            IAudioRecorder recorderService,
            IAudioProcessor audioProcessor,
            ISpectrogramBuilder spectrogramBuilder,
            IPeakDetector peakDetector,
            IAudioHashGenerator hashGenerator)
        {
            _databaseService = databaseService;
            _recorderService = recorderService;
            _audioProcessor = audioProcessor;
            _spectrogramBuilder = spectrogramBuilder;
            _peakDetector = peakDetector;
            _hashGenerator = hashGenerator;
        }

        public async Task<string> RecordAudioAsync(int durationTime = 15, CancellationToken cancellationToken = default) 
        {
            return await _recorderService.RecordAudioFromMicrophoneAsync(durationTime, cancellationToken);
        }

        public async Task<List<(int songId, string title, string artist, int matches, double confidence)>> RecognizeFromMicrophoneAsync(string audioFilePath)
        {
            if (string.IsNullOrEmpty(audioFilePath))
                return new List<(int, string, string, int, double)>();
            
            return await Task.Run(() =>
            {
                AnalysisProgress?.Invoke(10);
                float[] processedAudio = _audioProcessor
                    .PreprocessAudio(audioFilePath);

                AnalysisProgress?.Invoke(20);
                SpectrogramData spectrogramData = _spectrogramBuilder
                    .ProcessAudio(processedAudio, 11025);

                AnalysisProgress?.Invoke(40);
                List<Peak> allPeaks = _peakDetector
                    .ProcessPeekDetector(spectrogramData);

                AnalysisProgress?.Invoke(50);
                List<AudioHash> queryHashes = _hashGenerator
                    .GenerateHashes(allPeaks);

                AnalysisProgress?.Invoke(70);
                var results = _databaseService.SearchSong(queryHashes);
                
                AnalysisProgress?.Invoke(100);
                return results;
            });
        }
    }
}
