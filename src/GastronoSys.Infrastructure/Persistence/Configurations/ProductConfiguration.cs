using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(p => p.ProductCategory)
                  .WithMany(p => p.Products)
                  .HasForeignKey(p => p.ProductCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.OrderItems)
                 .WithOne(p => p.Product)
                 .HasForeignKey(p => p.ProductId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.StockItems)
                 .WithOne(si => si.Product)
                 .HasForeignKey(si => si.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);


            builder.Property(p => p.CreatedAt).IsRequired();

            builder.Property(p => p.IsDeleted).HasDefaultValue(false);

            builder.Property(p => p.RowVersion)
                .IsRowVersion();
        }
    }
}
