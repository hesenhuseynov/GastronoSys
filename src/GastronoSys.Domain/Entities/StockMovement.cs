namespace GastronoSys.Domain.Entities
{
    public class StockMovement : BaseEntity
    {
        public int StockItemId { get; set; }
        public StockItem StockItem { get; set; }

        public int Quantity { get; set; }

        public int StockMovementTypeId { get; set; }

        public StockMovementType StockMovementType { get; set; }

        public DateTime MovementDate { get; set; }

        public string? Note { get; set; }
    }
}
