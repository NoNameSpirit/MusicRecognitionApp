using MusicRecognitionApp.Application.Models;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IProcessingAudio
    {
        event Action<int> ImportProgress;
        
        Task<ImportTracksResult> AddTracksFromFolderAsync(string folderPath);

        Task AddTrackAsync(string audioFilePath, string title, string artist, CancellationToken cancellationToken = default);
    }
}
