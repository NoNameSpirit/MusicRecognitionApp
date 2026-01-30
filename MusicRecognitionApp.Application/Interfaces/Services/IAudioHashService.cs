using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IAudioHashService
    {
        Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues);

        Task AddHashesAsync(List<AudioHash> hashes, int songId);
    }
}
