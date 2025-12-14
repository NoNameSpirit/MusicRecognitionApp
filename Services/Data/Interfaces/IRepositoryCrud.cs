using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MusicRecognitionApp.Services.Data.Interfaces
{
    public interface IRepositoryCrud<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity> GetByIdAsync(int id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params string[] includes);

        Task<int> SaveChangesAsync();

        DbContext Context { get; }
    }
}
