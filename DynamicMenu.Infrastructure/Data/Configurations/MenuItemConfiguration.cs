using DynamicMenu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicMenu.Infrastructure.Data.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Keyword)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.MenuBaseItemId)
                .IsRequired(true);

            builder.HasOne(a => a.MenuBaseItem)
                .WithMany(a => a.MenuItems)
                .HasForeignKey(a => a.MenuBaseItemId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(x => x.MenuGroupId)
                .HasDefaultValue(1);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.Pid)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

        }
    }
} 