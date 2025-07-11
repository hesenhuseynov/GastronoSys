using GastronoSys.Domain.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GastronoSys.Infrastructure.Persistence.Repositories.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly GastronoSysDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(GastronoSysDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.CountAsync(filter);
        }

        public virtual Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? _dbSet : _dbSet.Where(filter);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }


    }
}
