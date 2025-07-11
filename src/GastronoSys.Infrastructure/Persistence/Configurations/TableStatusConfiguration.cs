using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class TableStatusConfiguration : IEntityTypeConfiguration<TableStatus>
    {
        public void Configure(EntityTypeBuilder<TableStatus> builder)
        {
            builder.HasKey(ts => ts.Id);
            builder.Property(ts => ts.Id).ValueGeneratedNever();

            builder.Property(ts => ts.Name).IsRequired().HasMaxLength(40);

            builder.Property(ts => ts.Description).HasMaxLength(150);

            builder.HasMany(ts => ts.Tables)
                 .WithOne(t => t.TableStatus)
                 .HasForeignKey(t => t.StatusId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ts => ts.CreatedAt).IsRequired();
            builder.Property(ts => ts.IsDeleted).HasDefaultValue(false);
            builder.Property(ts => ts.RowVersion).IsRowVersion();

        }
    }
}
