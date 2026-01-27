using MusicRecognitionApp.Infrastructure.Data.Contexts;
using MusicRecognitionApp.Infrastructure.Data.Entities;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations
{
    public class SongRepository : RepositoryCrud<SongEntity>, ISongRepository
    {
        public SongRepository(MusicRecognitionContext context)
            : base(context)
        {

        }

        public async Task<SongEntity?> GetSongByTitleAndArtistAsync(string title, string artist)
        {
            var songs = Get(
                filter: s => s.Title == title && s.Artist == artist);

            return songs.FirstOrDefault();
        }
    }
}
