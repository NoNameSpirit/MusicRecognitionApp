using Microsoft.EntityFrameworkCore;
using MusicRecognitionApp.Infrastructure.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MusicRecognitionApp.Infrastructure.Data.Repositories.Implementations
{
    public abstract class RepositoryCrud<TEntity> : IRepositoryCrud<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        public DbContext Context { get; }

        public RepositoryCrud(DbContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? take = null,
            params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
                query = query.Where(filter);

            if (take != null)
                query.Take(take.Value);

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }


        public virtual async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
