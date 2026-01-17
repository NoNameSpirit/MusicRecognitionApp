using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IAnalyzingSessionService
    {
        event Action<int> AnalyzingSession;

        Task<List<SearchResultModel>?> StartAnalyzingAsync(string? recordedAudioFile);
    }
}
