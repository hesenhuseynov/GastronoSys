using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories.Common;

namespace GastronoSys.Domain.Repositories
{
    public interface IProductIngredientRepository : IBaseRepository<ProductIngredient>
    {
        Task<List<ProductIngredient>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task AddAsync(ProductIngredient entity, CancellationToken cancellationToken);

        Task<ProductIngredient?> GetByIdWithDetailAsync(int id, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int productId, int stockItemId, CancellationToken cancellationToken);


    }
}
