namespace GastronoSys.Domain.Entities
{
    public class Receipt : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime PaidAt { get; set; }
        public string? Note { get; set; }
    }
}
