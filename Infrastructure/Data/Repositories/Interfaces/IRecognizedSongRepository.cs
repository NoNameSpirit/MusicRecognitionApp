using MusicRecognitionApp.Infrastructure.Data.Entities;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface IRecognizedSongRepository : IRepositoryCrud<RecognizedSongEntity>
    {
        IEnumerable<RecognizedSongEntity> GetRecent(int limit = 10);
        IEnumerable<RecognizedSongEntity> GetAllOrderedByDate();
        IEnumerable<(string Artist, int SongCount)> GetArtistsStatistics();
    }
}
