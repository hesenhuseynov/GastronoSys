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

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.Products.AnyAsync(c => c.Name == name, cancellationToken);
        }

        public async Task<List<Product>> GetByIdAsync(IEnumerable<int> productIds, CancellationToken cancellationToken)
        {

            return await _dbSet.Where(p => productIds.Contains(p.Id)).ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> GetByIdsWithIngredientsAsync(List<int> productIds, CancellationToken cancellationToken)
        {
            return await _context.Products.Where(p => productIds.Contains(p.Id))
                 .Include(p => p.ProductIngredients)
                 .ThenInclude(pi => pi.StockItem)
                 .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int productId, CancellationToken cancellationToken)
        {
            return await _context.Products.Include(p => p.ProductCategory)
               .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }
    }
}
