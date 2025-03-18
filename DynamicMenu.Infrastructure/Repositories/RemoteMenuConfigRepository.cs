using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DynamicMenu.Infrastructure.Repositories
{
    public class RemoteMenuConfigRepository : IRemoteMenusRepository
    {
        private readonly DynamicMenuDbContext _context;

        public RemoteMenuConfigRepository(DynamicMenuDbContext context)
        {
            _context = context;
        }

        public async Task<RemoteMenuConfig> GetByIdAsync(int id)
        {
            return await _context.RemoteMenuConfig
                //.Include(x => x.MenuItemRoles)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<RemoteMenuConfig>> GetAllAsync()
        {
            return await _context.RemoteMenuConfig
                .ToListAsync();
        }


        public async Task<RemoteMenuConfig> AddAsync(RemoteMenuConfig remoteMenu)
        {
            await _context.RemoteMenuConfig.AddAsync(remoteMenu);
            await _context.SaveChangesAsync();
            return remoteMenu;
        }

        public async Task UpdateAsync(RemoteMenuConfig remoteMenu)
        {
            _context.Entry(remoteMenu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.RemoteMenuConfig.FindAsync(id);
            if (menuItem != null)
            {
                _context.RemoteMenuConfig.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}