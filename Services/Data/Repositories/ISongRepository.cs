using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data.Repositories;

namespace MusicRecognitionApp.Services.Data.Interfaces
{
    public interface ISongRepository : IRepositoryCrud<SongEntity>
    {
        Task<SongEntity?> GetSongByTitleAndArtistAsync(string title, string artist);
    }
}
