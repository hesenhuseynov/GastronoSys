using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class ProductIngredientConfiguration : IEntityTypeConfiguration<ProductIngredient>
    {
        public void Configure(EntityTypeBuilder<ProductIngredient> builder)
        {
            builder.HasKey(pi => pi.Id);
            builder.Property(pi => pi.Quantity)
                .IsRequired()
                .HasPrecision(10, 4);

            builder.Property(pi => pi.Unit)
               .IsRequired().HasMaxLength(10);

            builder.HasOne(pi => pi.Product)
                 .WithMany(p => p.ProductIngredients)
                 .HasForeignKey(pi => pi.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pi => pi.StockItem)
                .WithMany(si => si.ProductIngredients)
                .HasForeignKey(pi => pi.StockItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pi => new { pi.ProductId, pi.StockItemId }).IsUnique();

        }
    }
}
