using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);





            builder.Property(d => d.Percentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(d => d.StartDate)
                .IsRequired();

            builder.Property(d => d.EndDate)
               .IsRequired();

            builder.HasMany(d => d.OrderItems)
                 .WithOne(oi => oi.Discount)
                 .HasForeignKey(oi => oi.DiscountId)
                 .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Discount_Percentage", "[Percentage] >= 0 AND [Percentage] <= 100");
                t.HasCheckConstraint("CK_Discount_Date", "[StartDate] <= [EndDate]");
            });

            builder.HasIndex(d => d.Name).IsUnique();
            builder.Property(d => d.CreatedAt).IsRequired();
            builder.Property(d => d.IsDeleted).HasDefaultValue(false);
            builder.Property(d => d.RowVersion).IsRowVersion();
        }
    }
}
