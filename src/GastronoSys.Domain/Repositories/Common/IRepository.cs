using System.Linq.Expressions;

namespace GastronoSys.Domain.Repositories.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);

        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        IQueryable<T> Query(Expression<Func<T, bool>> filter = null);

        Task SaveChangesAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}