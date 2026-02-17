using MusicRecognitionApp.Application.Models;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IAnalyzingSessionService
    {
        event Action<int> AnalyzingSession;

        void StopAnalyzing();

        Task<List<SearchResult>?> StartAnalyzingAsync(string? recordedAudioFile);
    }
}
