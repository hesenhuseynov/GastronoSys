namespace GastronoSys.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
