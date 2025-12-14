using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Services.Interfaces
{
    public interface IAudioRecognition
    {
        event Action<int> AnalysisProgress;

        event Action<int> ImportProgress;

        Task<string> RecordAudioAsync(int durationTime = 15, CancellationToken cancellationToken = default);

        Task<List<SearchResultModel>> RecognizeFromMicrophoneAsync(string audioFilePath);

        Task<(int added, int failed, List<string> errors)> AddTracksFromFolderAsync(string folderPath);
    }
}
