namespace GastronoSys.Domain.Entities
{
    public class StockMovement
    {
        public int StockItemId { get; set; }
        public StockItem StockItem { get; set; }

        public int Quantity { get; set; }

        public StockMovementType Type { get; set; }

        public DateTime MovementDate { get; set; }

        public string? Note { get; set; }
    }
}
