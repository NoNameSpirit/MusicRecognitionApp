using MusicRecognitionApp.Core.Models.Audio;

namespace MusicRecognitionApp.Infrastructure.Services.Interfaces
{
    public interface IAudioHashService
    {
        Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues);

        Task AddHashesAsync(List<AudioHash> hashes, int songId);
    }
}
