using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IAudioRecognitionService
    {
        event Action<int> AnalysisProgress;

        event Action<int> ImportProgress;

        Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath);

        Task<ImportTracksResult> AddTracksFromFolderAsync(string folderPath);
    }

    public record ImportTracksResult(int Added, int Failed, List<string> Errors);
}
