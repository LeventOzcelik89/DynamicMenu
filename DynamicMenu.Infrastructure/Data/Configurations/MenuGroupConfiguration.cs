using DynamicMenu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicMenu.Infrastructure.Data.Configurations
{
    public class MenuGroupConfiguration : IEntityTypeConfiguration<MenuGroup>
    {
        public void Configure(EntityTypeBuilder<MenuGroup> builder)
        {
            //  builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.MenuType)
                .IsRequired(true);

            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

        }
    }
} 