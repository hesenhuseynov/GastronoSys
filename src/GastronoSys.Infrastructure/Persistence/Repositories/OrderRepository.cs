using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace GastronoSys.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(GastronoSysDbContext context) : base(context) { }

        public async Task<Order?> GetByIdWithDetailAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                  .ThenInclude(oi => oi.Product)
                  .Include(o => o.Customer)
                  .Include(o => o.Table)
                  .Include(o => o.OrderStatus)
                  .Include(o => o.PaymentType)
                  .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersByCustomerIdAsyc(int customerId)
        {
            return await _dbSet.Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                .ToListAsync();

        }

        public async Task<List<Order>> GetRecentOrdersAsync(int days)
        {
            var dateLimit = DateTime.UtcNow.AddDays(-days);

            return await _dbSet.Where(o => o.CreatedAt >= dateLimit)
                .ToListAsync();
        }
    }
}
