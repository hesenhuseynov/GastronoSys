using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories.Common;

namespace GastronoSys.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetByIdAsync(IEnumerable<int> productIds, CancellationToken cancellationToken);
        Task<List<Product>> GetByIdsWithIngredientsAsync(List<int> productIds, CancellationToken cancellationToken);
        Task<Product?> GetByIdWithCategoryAsync(int productId, CancellationToken cancellationToken);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    }
}
