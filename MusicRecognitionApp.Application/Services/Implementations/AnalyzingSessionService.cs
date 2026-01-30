using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AnalyzingSessionService : IAnalyzingSessionService
    {
        public event Action<int> AnalyzingSession;

        private readonly IRecognitionService _recognitionService;
        
        public AnalyzingSessionService(IRecognitionService recognitionService)
        {
            _recognitionService = recognitionService;
        }

        public async Task<List<SearchResult>?> StartAnalyzingAsync(string? recordedAudioFile)
        {
            List<SearchResult>? RecognitionResults;

            try
            {
                _recognitionService.AnalysisProgress += OnAnalyzingSession;

                RecognitionResults = await _recognitionService.RecognizeFromMicrophoneAsync(recordedAudioFile);
            }
            finally
            {
                _recognitionService.AnalysisProgress -= OnAnalyzingSession;
            }

            if (File.Exists(recordedAudioFile))
            {
                File.Delete(recordedAudioFile);
            }

            return RecognitionResults;
        }

        private void OnAnalyzingSession(int progress)
        {
            AnalyzingSession?.Invoke(progress);
        }
    }
}
