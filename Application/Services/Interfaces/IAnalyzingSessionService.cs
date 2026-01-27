using MusicRecognitionApp.Application.Models;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IAnalyzingSessionService
    {
        event Action<int> AnalyzingSession;

        Task<List<SearchResult>?> StartAnalyzingAsync(string? recordedAudioFile);
    }
}
