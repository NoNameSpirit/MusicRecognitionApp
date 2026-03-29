using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Infrastructure.Data.Contexts;

namespace MusicRecognitionApp.Test.Infrastructure
{
    public class TestDbFactory
    {
        public MusicRecognitionContext Create()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<MusicRecognitionContext>()
                .UseSqlite(connection)
                .Options;

            var context = new MusicRecognitionContext(options);

            context.Database.ExecuteSqlRaw("Pragma foreign_keys = on");

            context.Database.EnsureCreated();

            return context;
        }
    }
}
