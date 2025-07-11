using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories.Common;

namespace GastronoSys.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetByIdAsync(IEnumerable<int> productIds);
    }
}
