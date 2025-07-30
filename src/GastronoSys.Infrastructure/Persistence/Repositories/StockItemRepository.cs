using GastronoSys.Domain.Entities;
using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence.Repositories.Common;

namespace GastronoSys.Infrastructure.Persistence.Repositories
{
    public class StockItemRepository : BaseRepository<StockItem>, IStockItemRepository
    {
        public StockItemRepository(GastronoSysDbContext context) : base(context)
        {

        }

    }
}
