using MusicRecognitionApp.Application.Models;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IRecognitionService
    {
        event Action<int> AnalysisProgress;

        Task<List<SearchResult>> RecognizeFromMicrophoneAsync(string audioFilePath, CancellationToken cancellationToken = default);
    }
}
