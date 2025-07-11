namespace GastronoSys.Application.Features.Orders.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string? TableName { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public int OrderStatusId { get; set; }
        public string? OrderStatusName { get; set; }
        public int? PaymentTypeId { get; set; }
        public string? PaymentTypeName { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? ServedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
