using MusicRecognitionApp.Core.Models.Audio;
using System.Threading;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IAudioHashService
    {
        Task<List<(int SongId, int Count)>> FindSongMatchesAsync(List<uint> hashValues);
    }
}
