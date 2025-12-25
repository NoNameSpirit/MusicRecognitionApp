using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Services.Data
{
    public interface IAudioHashService
    {
        Dictionary<uint, List<AudioHash>> GetHashesDictionary(List<uint> hashValues);
        Task AddHashesAsync(List<AudioHash> hashes, int songId);
    }
}
