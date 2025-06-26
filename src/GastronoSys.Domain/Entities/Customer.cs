﻿namespace GastronoSys.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
