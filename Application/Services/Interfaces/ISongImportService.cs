using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Services.Interfaces
{
    public interface ISongImportService
    {
        Task AddSongAsync(string title, string artist, List<AudioHash> hashes);
    }
}
