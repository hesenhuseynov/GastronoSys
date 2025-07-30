namespace GastronoSys.Application.Features.StockItem.Dtos
{
    public class StockItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
