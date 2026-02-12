using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Application.Interfaces.UnitOfWork;
using MusicRecognitionApp.Infrastructure.Data.Contexts;

namespace MusicRecognitionApp.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicRecognitionContext _context;

        public UnitOfWork(MusicRecognitionContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Clear() 
        {
            _context.ChangeTracker.Clear();
        }
    }
}
