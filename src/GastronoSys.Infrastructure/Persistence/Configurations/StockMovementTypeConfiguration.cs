using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class StockMovementTypeConfiguration : IEntityTypeConfiguration<StockMovementType>
    {
        public void Configure(EntityTypeBuilder<StockMovementType> builder)
        {
            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Name).IsRequired().HasMaxLength(60);
            builder.Property(sm => sm.Description).HasMaxLength(150);

            builder.HasMany(st => st.StockMovements)
                .WithOne(s => s.StockMovementType)
                .HasForeignKey(s => s.StockMovementTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(st => st.CreatedAt).IsRequired();

            builder.Property(st => st.IsDeleted).HasDefaultValue(false);

            builder.Property(st => st.RowVersion).IsRowVersion();

        }
    }
}
