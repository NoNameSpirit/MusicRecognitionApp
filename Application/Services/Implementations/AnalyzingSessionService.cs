using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AnalyzingSessionService : IAnalyzingSessionService
    {
        public event Action<int> AnalyzingSession;

        private readonly IServiceProvider _serviceProvider;
        
        public AnalyzingSessionService(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        

        public async Task<List<SearchResultModel>?> StartAnalyzingAsync(string? recordedAudioFile)
        {
            List<SearchResultModel>? RecognitionResults;

            var recognitionService = _serviceProvider.GetRequiredService<IAudioRecognitionService>();
            try
            {
                recognitionService.AnalysisProgress += OnAnalyzingSession;
                
                RecognitionResults = await recognitionService.RecognizeFromMicrophoneAsync(recordedAudioFile);
            }
            finally
            {
                recognitionService.AnalysisProgress -= OnAnalyzingSession;
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
