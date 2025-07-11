using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories.Common;

namespace GastronoSys.Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrdersByCustomerIdAsyc(int customerId);
        Task<Order?> GetByIdWithDetailAsync(int orderId);
        Task<List<Order>> GetRecentOrdersAsync(int days);

        //Task<Order?> GetOrderWithItemsAsync(int orderId);
    }
}
