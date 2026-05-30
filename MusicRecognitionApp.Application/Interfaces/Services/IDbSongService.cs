using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface IDbSongService
    {
        Task<SongModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<SongModel?> GetByTitleAndArtistAsync(string title, string artist, CancellationToken cancellationToken = default);
        Task CreateAsync(string title, string artist,
            List<AudioHash> hashes, CancellationToken cancellationToken = default);
    }
}
