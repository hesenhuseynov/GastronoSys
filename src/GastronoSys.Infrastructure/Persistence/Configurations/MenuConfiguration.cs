using GastronoSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GastronoSys.Infrastructure.Persistence.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).IsRequired().HasMaxLength(150);

            builder.Property(m => m.Description).HasMaxLength(250);

            builder.Property(m => m.IsActive).HasDefaultValue(true);

            builder.HasMany(m => m.Products)
                .WithMany(p => p.Menus)
                .UsingEntity(j => j.ToTable("MenuProducts"));

            builder.Property(m => m.CreatedAt).IsRequired();
            builder.Property(m => m.IsDeleted).HasDefaultValue(false);
            builder.Property(m => m.RowVersion).IsRowVersion();
        }
    }
}
