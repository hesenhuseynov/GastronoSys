namespace GastronoSys.Domain.Entities
{
    public class TableStatus : BaseEntity
    {
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        public string? Description { get; set; }
    }
}
