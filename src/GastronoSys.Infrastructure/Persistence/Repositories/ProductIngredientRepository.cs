using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace GastronoSys.Infrastructure.Persistence.Repositories
{
    public class ProductIngredientRepository : BaseRepository<ProductIngredient>, IProductIngredientRepository

    {
        public ProductIngredientRepository(GastronoSysDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ProductIngredient entity, CancellationToken cancellationToken)
        {
            await _context.ProductIngredient.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int productId, int stockItemId, CancellationToken cancellationToken)
        {
            return await _context.ProductIngredient.AnyAsync(pi => pi.ProductId == productId && pi.StockItemId == stockItemId, cancellationToken);
        }

        public async Task<ProductIngredient?> GetByIdWithDetailAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.ProductIngredient
                 .Include(pi => pi.Product)
                 .Include(pi => pi.StockItem)
                 .FirstOrDefaultAsync(pi => pi.Id == id, cancellationToken);
        }

        public async Task<List<ProductIngredient>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            return await _context.ProductIngredient.Where(c => c.ProductId == productId).ToListAsync(cancellationToken);
        }
    }
}
