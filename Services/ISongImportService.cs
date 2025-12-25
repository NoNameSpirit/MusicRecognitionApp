using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Services.Import
{
    public interface ISongImportService
    {
        Task AddSongAsync(string title, string artist, List<AudioHash> hashes);
    }
}
