using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Name).IsRequired();

            builder.Property(pc => pc.Description).HasMaxLength(255);

            builder.Property(pc => pc.SortOrder)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(pc => pc.SortOrder);

            builder.HasMany(pc => pc.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(pc => pc.CreatedAt).IsRequired();
            builder.Property(pc => pc.IsDeleted).HasDefaultValue(false);
            builder.Property(pc => pc.RowVersion).IsRowVersion();
        }
    }
}
