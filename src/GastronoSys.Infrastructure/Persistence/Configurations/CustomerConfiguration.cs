using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .IsRequired().HasMaxLength(155);

            builder.Property(c => c.Phone).HasMaxLength(30);

            builder.Property(c => c.Email).HasMaxLength(120);

            builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.Property(c => c.RowVersion).IsRowVersion();
        }
    }
}
