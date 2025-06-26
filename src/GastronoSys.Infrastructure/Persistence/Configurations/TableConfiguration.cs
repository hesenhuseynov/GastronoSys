using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Capacity)
                .IsRequired();

            builder.HasOne(t => t.TableStatus)
               .WithMany(ts => ts.Tables)
               .HasForeignKey(t => t.StatusId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Orders)
               .WithOne(o => o.Table)
               .HasForeignKey(o => o.TableId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Property(t => t.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(t => t.RowVersion)
                .IsRowVersion();
        }
    }
}
