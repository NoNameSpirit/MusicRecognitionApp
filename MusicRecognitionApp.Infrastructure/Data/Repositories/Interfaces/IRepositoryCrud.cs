using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System.Threading;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces
{
    public interface IRepositoryCrud<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? take = null,
            params string[] includes);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbContext Context { get; }
    }
}
