namespace GastronoSys.Domain.Entities
{
    public class Receipt : BaseEntity
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
