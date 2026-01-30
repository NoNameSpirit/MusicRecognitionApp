using MusicRecognitionApp.Infrastructure.Data.Contexts;

namespace MusicRecognitionApp.Infrastructure.Services
{
    public class DatabaseInitializer
    {
        private readonly MusicRecognitionContext _context;

        public DatabaseInitializer(MusicRecognitionContext context)
        {
            _context = context;
        }

        public void EnsureCreated()
        {
            _context.Database.EnsureCreated();
        }
    }
}