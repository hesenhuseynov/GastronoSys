using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(pt => pt.Id);
            builder.Property(pt => pt.Name).IsRequired().HasMaxLength(50);

            builder.HasIndex(pt => pt.Name).IsUnique();

            builder.Property(pt => pt.Description).HasMaxLength(200);

            builder.HasMany(pt => pt.Orders)
                .WithOne(o => o.PaymentType)
                .HasForeignKey(o => o.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(pt => pt.CreatedAt).IsRequired();

            builder.Property(pt => pt.IsDeleted).HasDefaultValue(false);

            builder.Property(pt => pt.RowVersion).IsRowVersion();
        }
    }
}
