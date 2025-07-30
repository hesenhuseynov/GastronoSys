using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.HasKey(si => si.Id);

            builder.Property(si => si.Name).IsRequired().HasMaxLength(150);

            builder.Property(si => si.Unit).IsRequired().HasMaxLength(50);

            builder.HasMany(si => si.StockMovements)
                .WithOne(sm => sm.StockItem)
                .HasForeignKey(sm => sm.StockItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(si => si.CreatedAt).IsRequired();

            builder.Property(si => si.IsDeleted).HasDefaultValue(false);

            builder.Property(si => si.RowVersion).IsRowVersion();

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_StockItem_Quantity", "[Quantity] >= 0 ");

            });
        }
    }
}
