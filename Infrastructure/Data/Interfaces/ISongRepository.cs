using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Interfaces
{
    public interface ISongRepository : IRepositoryCrud<SongEntity>
    {
        Task<SongEntity?> GetSongByTitleAndArtistAsync(string title, string artist);
    }
}
