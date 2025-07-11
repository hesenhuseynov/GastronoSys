using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Quantity).IsRequired();

            builder.Property(sm => sm.MovementDate)
                .HasMaxLength(300);

            builder.Property(sm => sm.Note)
                .HasMaxLength(300);

            builder.HasOne(sm => sm.StockItem)
                .WithMany(si => si.StockMovements)
                .HasForeignKey(sm => sm.StockItemId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(sm => sm.StockMovementType)
               .WithMany(smt => smt.StockMovements)
               .HasForeignKey(sm => sm.StockMovementTypeId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.Property(sm => sm.CreatedAt).IsRequired();
            builder.Property(sm => sm.IsDeleted).HasDefaultValue(false);
            builder.Property(sm => sm.RowVersion).IsRowVersion();


            builder.ToTable(t =>
            {
                t.HasCheckConstraint("СK_StockMovement_Quantity", "[Quantity]>=0");
            });

        }
    }
}
