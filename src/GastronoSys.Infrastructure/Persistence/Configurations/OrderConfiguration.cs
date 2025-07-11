using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(o => o.TotalAmount).HasPrecision(18, 4);

            builder.Property(o => o.Note).HasMaxLength(300);

            builder.HasOne(p => p.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(o => o.User)
              .WithMany(u => u.Orders)
              .HasForeignKey(o => o.UserId)
              .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(o => o.OrderItems)
                 .WithOne(oi => oi.Order)
                 .HasForeignKey(oi => oi.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.OrderStatus)
                .WithMany(os => os.Orders)
                .HasForeignKey(o => o.OrderStatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.PaymentType)
                .WithMany(pt => pt.Orders)
                .HasForeignKey(o => o.PaymentTypeId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.IsDeleted).HasDefaultValue(false);

            builder.Property(o => o.CreatedAt).IsRequired();

            builder.Property(p => p.RowVersion).IsRowVersion();

            builder.HasIndex(o => o.OrderStatusId);
            builder.HasIndex(o => o.TableId);
        }
    }
}
