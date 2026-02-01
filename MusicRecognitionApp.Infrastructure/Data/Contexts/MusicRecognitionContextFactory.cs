using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicRecognitionApp.Infrastructure.Data.Contexts
{
    public class MusicRecognitionContextFactory : IDesignTimeDbContextFactory<MusicRecognitionContext>
    {
        public MusicRecognitionContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<MusicRecognitionContext>()
                .UseSqlite("Data Source=ShazamDB.sqlite")
                .Options;

            return new MusicRecognitionContext(options);
        }
    }
}
