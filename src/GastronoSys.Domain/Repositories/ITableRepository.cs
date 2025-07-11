using GastronoSys.Domain.Repositories.Common;

using GastronoTable = GastronoSys.Domain.Entities.Table;



namespace GastronoSys.Domain.Repositories
{
    public interface ITableRepository : IBaseRepository<GastronoTable>
    {
        Task<List<GastronoTable>> GetAvailableTablesAsync();
        Task<List<GastronoTable>> GetTablesStatusAsync(int statusId);
        Task<GastronoTable?> GetByNameAsync(string name);

    }
}
