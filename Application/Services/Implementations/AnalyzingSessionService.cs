using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AnalyzingSessionService : IAnalyzingSessionService
    {
        public event Action<int> AnalyzingSession;

        private readonly IAudioRecognitionService _recognitionService;
        
        public AnalyzingSessionService(IAudioRecognitionService recognitionService)
        {
            _recognitionService = recognitionService;
        }

            public async Task<List<SearchResultModel>?> StartAnalyzingAsync(string? recordedAudioFile)
            {
                List<SearchResultModel>? RecognitionResults;

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
