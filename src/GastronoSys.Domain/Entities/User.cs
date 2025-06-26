﻿namespace GastronoSys.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Order> Orders { get; set; }
    }
}
