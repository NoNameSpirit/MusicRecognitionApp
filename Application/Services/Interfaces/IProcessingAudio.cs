namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface IProcessingAudio
    {
        event Action<int> ImportProgress;
        
        Task<ImportTracksResult> AddTracksFromFolderAsync(string folderPath);
    }

    public record ImportTracksResult(int Added, int Failed, List<string> Errors);
}
