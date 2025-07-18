﻿namespace GastronoSys.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        //Special not  for OrderItem  ample: 1 pizza with 2 toppings
        public string? Note { get; set; }
        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
