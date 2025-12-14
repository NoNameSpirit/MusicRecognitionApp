using MusicRecognitionApp.Core.Models.Entities;

namespace MusicRecognitionApp.Services.Data.Interfaces
{
    public interface ISongRepository : IRepositoryCrud<SongEntity>
    {
        Task<SongEntity?> GetSongByTitleAndArtistAsync(string title, string artist);
    }
}
