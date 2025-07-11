using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);

            builder.Property(c => c.FullName)
                .HasMaxLength(128);

            builder.Property(c => c.Email)
                 .HasMaxLength(128);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.RowVersion).IsRowVersion();
        }
    }
}
