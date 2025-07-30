namespace GastronoSys.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Order> Orders { get; set; }

        //public int SortOrder { get; set; }
    }
}
