using Microsoft.Extensions.DependencyInjection;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Application.Services.Implementations
{
    public class AnalyzingSessionService : IAnalyzingSessionService
    {
        public event Action<int> AnalyzingSession;

        private CancellationTokenSource? _cts;

        private readonly IServiceScopeFactory _scopeFactory;
        
        public AnalyzingSessionService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<List<SearchResult>?> StartAnalyzingAsync(string? recordedAudioFile)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _recognitionService = scope.ServiceProvider.GetRequiredService<IRecognitionService>();

                List<SearchResult>? RecognitionResults;

                try
                {
                    _cts = new CancellationTokenSource();

                    _recognitionService.AnalysisProgress += OnAnalyzingSession;

                    RecognitionResults = await _recognitionService.RecognizeFromMicrophoneAsync(recordedAudioFile, _cts.Token);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                finally
                {
                    _cts.Dispose();
                    _cts = null;

                    _recognitionService.AnalysisProgress -= OnAnalyzingSession;

                    if (File.Exists(recordedAudioFile))
                    {
                        File.Delete(recordedAudioFile);
                    }
                }

                return RecognitionResults;
            }
        }

        public void StopAnalyzing()
        {
            _cts?.Cancel();
        }

        private void OnAnalyzingSession(int progress)
        {
            AnalyzingSession?.Invoke(progress);
        }
    }
}
