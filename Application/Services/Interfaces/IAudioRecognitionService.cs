using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IAudioRecognitionService
    {
        event Action<int> AnalysisProgress;

        Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath);
    }
}
