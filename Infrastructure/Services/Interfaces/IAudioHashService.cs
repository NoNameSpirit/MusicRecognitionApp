using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Services.Interfaces
{
    public interface IAudioHashService
    {
        Dictionary<uint, List<AudioHash>> GetHashesDictionary(List<uint> hashValues);
        Task AddHashesAsync(List<AudioHash> hashes, int songId);
    }
}
