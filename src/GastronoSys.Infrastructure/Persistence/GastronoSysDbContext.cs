using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace GastronoSys.Infrastructure.Persistence
{
    public class GastronoSysDbContext : DbContext
    {
        public GastronoSysDbContext(DbContextOptions<GastronoSysDbContext> options) : base(options)
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

        public DbSet<ProductIngredient> ProductIngredient { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {

                    var parameter = Expression.Parameter(entityType.ClrType);

                    var isDeletedProperty = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));

                    var compare = Expression.Equal(isDeletedProperty, Expression.Constant(false));

                    var lambda = Expression.Lambda(compare, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            modelBuilder.Entity<TableStatus>().HasData(
                new TableStatus { Id = 1, Name = "Available", Description = "Boş masa", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new TableStatus { Id = 2, Name = "Occupied", Description = "Dolu masa", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new TableStatus { Id = 3, Name = "Reserved", Description = "Rezerv olunub", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) }
                );

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Pending", Description = "Yeni sifariş", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new OrderStatus { Id = 2, Name = "Preparing ", Description = "Hazırlanır", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new OrderStatus { Id = 3, Name = "Served", Description = "Verildi", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new OrderStatus { Id = 4, Name = "Paid", Description = "Odenildi", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new OrderStatus { Id = 5, Name = "Cancelled", Description = "Leğv olundu", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) }
                );

            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType { Id = 1, Name = "Cash", Description = "Nağd", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new PaymentType { Id = 2, Name = "Card", Description = "Kart", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) }

                );

            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "Admin", Description = "Adminstrator", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                 new Role { Id = 2, Name = "Waiter", Description = "Garson", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                 new Role { Id = 3, Name = "Cook", Description = "Aşpaz", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) }
                 );

            modelBuilder.Entity<StockMovementType>().HasData(
                new StockMovementType { Id = 1, Name = "In", Description = "Stocka Elave edildi", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new StockMovementType { Id = 2, Name = "Out", Description = "Stocktan çıxış ", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) },
                new StockMovementType { Id = 3, Name = "Waste", Description = "Israf", CreatedAt = new DateTime(2025, 6, 29, 5, 19, 0, DateTimeKind.Utc) }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
