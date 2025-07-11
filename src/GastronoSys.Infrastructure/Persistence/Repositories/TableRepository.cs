using GastronoSys.Domain.Repositories;
using GastronoSys.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

using GastronoTable = GastronoSys.Domain.Entities.Table;

namespace GastronoSys.Infrastructure.Persistence.Repositories
{
    public class TableRepository : BaseRepository<GastronoTable>, ITableRepository
    {
        public TableRepository(GastronoSysDbContext context) : base(context) { }

        public async Task<List<GastronoTable>> GetAvailableTablesAsync()
        {
            return await _dbSet.Where(t => t.StatusId == 1).ToListAsync();
        }
        public async Task<List<GastronoTable>> GetTablesStatusAsync(int statusId)
        {
            return await _dbSet.Where(t => t.StatusId == statusId).ToListAsync();
        }

        public async Task<GastronoTable?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.Name == name);
        }
    }
}
