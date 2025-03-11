using DynamicMenu.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicMenu.Infrastructure.Data.Configurations
{
    public class MenuItemRoleConfiguration : IEntityTypeConfiguration<MenuItemRole>
    {
        public void Configure(EntityTypeBuilder<MenuItemRole> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(x => x.MenuItem)
            //    .WithMany(x => x.MenuItemRoles)
            //    .HasForeignKey(x => x.MenuItemId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 