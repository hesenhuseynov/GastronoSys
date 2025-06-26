using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastronoSys.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<StockItem> StockItems { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<StockMovement> StockMovements { get; set; }

        public DbSet<StockMovementType> StockMovementTypes { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<TableStatus> TableStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your model configurations here
        }


    }
}
