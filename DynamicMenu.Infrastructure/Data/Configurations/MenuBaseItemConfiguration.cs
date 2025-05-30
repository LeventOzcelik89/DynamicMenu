using DynamicMenu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicMenu.Infrastructure.Data.Configurations
{
    public class MenuBaseItemConfiguration : IEntityTypeConfiguration<MenuBaseItem>
    {
        public void Configure(EntityTypeBuilder<MenuBaseItem> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TextEn)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.IconPath)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

        }
    }
} 