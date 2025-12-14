using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Core.Models.Entities;
using MusicRecognitionApp.Services.Data;
using MusicRecognitionApp.Services.Data.Interfaces;

namespace MusicRecognitionApp.Services.Data.Repositories
{
    public class RecognizedSongRepository : RepositoryCrud<RecognizedSongEntity>, IRecognizedSongRepository
    {
        public RecognizedSongRepository(MusicRecognitionContext context) 
            : base(context)
        {
        
        }

        public IEnumerable<RecognizedSongEntity> GetRecent(int limit = 10)
        {
            return Context.Set<RecognizedSongEntity>()
                .Include(r => r.Song)
                .OrderByDescending(r => r.RecognitionDate)
                .Take(limit)
                .ToList();
        }

        public IEnumerable<RecognizedSongEntity> GetAllOrderedByDate()
        {
            return Context.Set<RecognizedSongEntity>()
                .Include(r => r.Song)
                .OrderByDescending(r=>r.RecognitionDate)
                .ToList();
        }

        public IEnumerable<(string Artist, int SongCount)> GetArtistsStatistics()
        {
            return Context.Set<RecognizedSongEntity>()
                .Include(r=>r.Song)
                .AsEnumerable()
                .GroupBy(r=>r.Song.Artist)
                .Select(g=> (Artist: g.Key, SongCount: g.Count()))
                .OrderByDescending(g=>g.SongCount)
                .ToList();
        }
    }
}
