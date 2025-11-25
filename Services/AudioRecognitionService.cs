using MusicRecognitionApp.Data;
using MusicRecognitionApp.Services.Audio;
using MusicRecognitionApp.Services.Interfaces;

namespace MusicRecognitionApp.Services
{
    public class AudioRecognitionService : IAudioRecognition
    {
        private readonly IAudioDatabase _databaseService;
        private readonly IAudioRecorder _recorderService;
        private readonly AudioProcessor _audioProcessor;
        private readonly SpectrogramBuilder _spectrogramBuilder;
        private readonly PeakDetector _peakDetector;
        private readonly AudioHashGenerator _hashGenerator;

        public AudioRecognitionService(
            IAudioDatabase databaseService,
            IAudioRecorder recorderService)
        {
            _databaseService = databaseService;
            _recorderService = recorderService;
            _audioProcessor = new AudioProcessor();
            _spectrogramBuilder = new SpectrogramBuilder();
            _peakDetector = new PeakDetector();
            _hashGenerator = new AudioHashGenerator();
        }

        public async Task<List<(int songId, string title, string artist, int matches, double confidence)>> RecognizeFromMicrophoneAsync()
        {
            string tempWavFile = await _recorderService.RecordAudioFromMicrophoneAsync(15);
            
            if (string.IsNullOrEmpty(tempWavFile))
                return new List<(int, string, string, int, double)>();

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
        }
    }
}
