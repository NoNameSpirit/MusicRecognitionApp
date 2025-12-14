using MusicRecognitionApp.Core.Models.Entities;

namespace MusicRecognitionApp.Services.Data.Interfaces
{
    public interface IRecognizedSongRepository : IRepositoryCrud<RecognizedSongEntity>
    {
        IEnumerable<RecognizedSongEntity> GetRecent(int limit = 10);
        IEnumerable<RecognizedSongEntity> GetAllOrderedByDate();
        IEnumerable<(string Artist, int SongCount)> GetArtistsStatistics();
    }
}
