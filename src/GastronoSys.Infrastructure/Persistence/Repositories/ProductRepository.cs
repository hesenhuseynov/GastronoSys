using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace GastronoSys.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(GastronoSysDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetByIdAsync(IEnumerable<int> productIds)
        {

            return await _dbSet.Where(p => productIds.Contains(p.Id)).ToListAsync();
        }
    }
}
