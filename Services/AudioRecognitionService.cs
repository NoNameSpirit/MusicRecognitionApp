using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Audio.Interfaces;
using MusicRecognitionApp.Services.Interfaces;

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

        public async Task<List<(int songId, string title, string artist, int matches, double confidence)>> RecognizeFromMicrophoneAsync()
        {
            string tempWavFile = await _recorderService.RecordAudioFromMicrophoneAsync(15);
            
            if (string.IsNullOrEmpty(tempWavFile))
                return new List<(int, string, string, int, double)>();
            return await Task.Run(() =>
            {
                float[] processedAudio = _audioProcessor
                .PreprocessAudio(tempWavFile);

                SpectrogramData spectrogramData = _spectrogramBuilder
                    .ProcessAudio(processedAudio, 11025);

                List<Peak> allPeaks = _peakDetector
                    .ProcessPeekDetector(spectrogramData);

                List<AudioHash> queryHashes = _hashGenerator
                    .GenerateHashes(allPeaks);

                var results = _databaseService.SearchSong(queryHashes);
                return results;
            });
        }
    }
}
