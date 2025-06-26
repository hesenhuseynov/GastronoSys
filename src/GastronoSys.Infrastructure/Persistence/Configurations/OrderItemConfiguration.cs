using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);


            builder.HasOne(oi => oi.Discount)
                  .WithMany(d => d.OrderItems)
                  .HasForeignKey(oi => oi.DiscountId)
                  .OnDelete(DeleteBehavior.SetNull);


            builder.Property(oi => oi.Quantity)
                 .IsRequired();


            builder.Property(oi => oi.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            builder.Ignore(oi => oi.TotalPrice);

            builder.Property(oi => oi.Note)
                .HasMaxLength(260);

            builder.Property(oi => oi.CreatedAt).IsRequired();

            builder.Property(oi => oi.IsDeleted).HasDefaultValue(false);

            builder.Property(oi => oi.RowVersion).IsRowVersion();
        }
    }
}
