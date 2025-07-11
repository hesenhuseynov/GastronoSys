namespace GastronoSys.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int TableId { get; set; }
        public Table Table { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int? PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public Receipt Receipt { get; set; }

        public decimal? TotalAmount { get; set; }
        public DateTime? ServedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        //addition number or note or something 
        public string? Note { get; set; }
    }
}
