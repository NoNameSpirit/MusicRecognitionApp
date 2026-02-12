using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Core.Models.Audio;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface ISongService
    {
        Task<SongModel?> GetByIdAsync(int id);
        Task<SongModel?> GetByTitleAndArtistAsync(string title, string artist);
        Task<SongCreationResult> CreateAsync(string title, string artist, 
            List<AudioHash> hashes, CancellationToken cancellationToken = default);
    }
}
