using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Application.Interfaces.Services
{
    public interface ISongService
    {
        Task<SongModel?> GetByIdAsync(int id);
        Task<SongModel?> GetByTitleAndArtistAsync(string title, string artist);
        Task<SongModel> CreateAsync(string title, string artist);
    }
}
