using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Infrastructure.Services.Interfaces
{
    public interface ISongService
    {
        Task<SongModel?> GetByIdAsync(int id);
        Task<SongModel?> GetByTitleAndArtistAsync(string title, string artist);
        Task<SongModel> CreateAsync(string title, string artist);
    }
}
