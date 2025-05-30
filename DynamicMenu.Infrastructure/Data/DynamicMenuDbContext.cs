using DynamicMenu.Core.Entities;
using DynamicMenu.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DynamicMenu.Infrastructure.Data
{
    public class DynamicMenuDbContext : DbContext
    {
        public DynamicMenuDbContext(DbContextOptions<DynamicMenuDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<MenuBaseItem> MenuBaseItem { get; set; }
        public DbSet<MenuGroup> MenuGroup { get; set; }
        public DbSet<RemoteMenuConfig> RemoteMenuConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            modelBuilder.ApplyConfiguration(new MenuGroupConfiguration());
            modelBuilder.ApplyConfiguration(new RemoteMenuConfigConfiguration());
        }
    }
} 