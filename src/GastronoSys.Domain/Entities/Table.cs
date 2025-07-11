namespace GastronoSys.Domain.Entities
{
    public class Table : BaseEntity
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public TableStatus TableStatus { get; set; }
        public int Capacity { get; set; }
        public List<Order> Orders { get; set; }
    }
}
