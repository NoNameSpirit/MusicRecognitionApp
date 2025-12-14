using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data;
using MusicRecognitionApp.Services.Data.Interfaces;

namespace MusicRecognitionApp.Services.Data.Repositories
{
    public class SongRepository : RepositoryCrud<SongEntity>, ISongRepository 
    {
        public SongRepository(MusicRecognitionContext context)
            : base(context)
        {
            
        }

        public async Task<SongEntity?> GetSongByTitleAndArtistAsync(string title, string artist)
        {
            return await Context.Set<SongEntity>()
                                .FirstOrDefaultAsync(s => s.Title == title && s.Artist == artist);
        }
    }
}
