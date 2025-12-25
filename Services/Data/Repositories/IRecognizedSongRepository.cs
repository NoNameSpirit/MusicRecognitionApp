using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data.Interfaces;

namespace MusicRecognitionApp.Services.Data.Repositories
{
    public interface IRecognizedSongRepository : IRepositoryCrud<RecognizedSongEntity>
    {
        IEnumerable<RecognizedSongEntity> GetRecent(int limit = 10);
        IEnumerable<RecognizedSongEntity> GetAllOrderedByDate();
        IEnumerable<(string Artist, int SongCount)> GetArtistsStatistics();
    }
}
