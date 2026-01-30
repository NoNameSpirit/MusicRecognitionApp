using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface ISongRepository : IRepositoryCrud<SongEntity>
    {
        Task<SongEntity?> GetSongByTitleAndArtistAsync(string title, string artist);
    }
}
