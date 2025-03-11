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
                .HasMaxLength(50);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TextEn)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.DescriptionEn)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.IconPath)
                .HasMaxLength(200)
                .IsRequired(false);

            //builder.Property(x => x.CategoryName)
            //    .HasMaxLength(100)
            //    .IsRequired(false);

            builder.Property(x => x.MenuId)
                .HasDefaultValue(1);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.Pid)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Menu)
                .WithMany(x => x.MenuItems)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 