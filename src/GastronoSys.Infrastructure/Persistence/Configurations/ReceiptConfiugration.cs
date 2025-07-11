using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class ReceiptConfiugration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Order)
                .WithOne(o => o.Receipt)
                .HasForeignKey<Receipt>(r => r.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.PaidAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.PaymentTypeId)
            .IsRequired();

            builder.HasOne(r => r.PaymentType)
                .WithMany(pt => pt.Receipts)
                .HasForeignKey(r => r.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.PaidAt)
                .IsRequired();

            builder.Property(r => r.Note)
                .HasMaxLength(300);

            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);
            builder.Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
