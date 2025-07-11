using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(os => os.Id);

            builder.Property(os => os.Name).IsRequired().HasMaxLength(200);

            builder.Property(os => os.Description).HasMaxLength(300);





            builder.HasMany(os => os.Orders)
                .WithOne(o => o.OrderStatus)
                .HasForeignKey(o => o.OrderStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(os => os.CreatedAt).IsRequired();

            builder.Property(os => os.IsDeleted).HasDefaultValue(false);

            builder.Property(os => os.RowVersion).IsRowVersion();

        }
    }
}
