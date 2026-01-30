namespace MusicRecognitionApp.Presentation.Services.Interfaces
{
    public interface ISongAddingService
    {
        Task<ImportResult> ImportTracksFromFolderAsync();
    }
    
    public record ImportResult(bool Success, string Message, int Added = 0, int Failed = 0);
}
