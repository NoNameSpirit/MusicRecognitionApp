using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IAudioRecognition
    {
        event Action<int> AnalysisProgress;

        event Action<int> ImportProgress;

        Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath);

        Task<(int added, int failed, List<string> errors)> AddTracksFromFolderAsync(string folderPath);
    }
}
