namespace GastronoSys.Application.Features.Orders.Dtos
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Note { get; set; }
        public int? DiscountId { get; set; }
    }
}
