using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories.Common;

namespace GastronoSys.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetPendingOrderAsync();



    }
}
