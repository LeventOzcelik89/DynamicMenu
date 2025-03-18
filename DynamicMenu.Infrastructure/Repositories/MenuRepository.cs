using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace DynamicMenu.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DynamicMenuDbContext _context;

        public MenuRepository(DynamicMenuDbContext context)
        {
            _context = context;
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _context.Menu.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Menu.ToListAsync();
        }

        public async Task<Menu> AddAsync(Menu menuItem)
        {
            await _context.Menu.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task UpdateAsync(Menu menuItem)
        {
            _context.Entry(menuItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.Menu.FindAsync(id);
            if (menuItem != null)
            {
                _context.Menu.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}