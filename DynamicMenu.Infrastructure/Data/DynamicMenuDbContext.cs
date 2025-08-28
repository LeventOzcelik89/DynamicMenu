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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var selectedEntityList = ChangeTracker.Entries()
                .Where(x => x.Entity.GetType().IsAssignableTo(typeof(BaseEntity)) && (x.State == EntityState.Added || x.State == EntityState.Modified));
            
            foreach (var entity in selectedEntityList)
            {

                switch (entity.State)
                {
                    case EntityState.Added:
                        ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                        ((BaseEntity)entity.Entity).ModifiedDate = null;
                        break;

                    case EntityState.Modified:
                        Entry(((BaseEntity)entity.Entity)).Property(x => x.CreatedDate).IsModified = false;
                        ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {

            var selectedEntityList = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in selectedEntityList)
            {

                switch (entity.State)
                {
                    case EntityState.Added:
                        ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                        ((BaseEntity)entity.Entity).ModifiedDate = null;
                        break;

                    case EntityState.Modified:
                        Entry(((BaseEntity)entity.Entity)).Property(x => x.CreatedDate).IsModified = false;
                        ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChanges();

        }


    }
}