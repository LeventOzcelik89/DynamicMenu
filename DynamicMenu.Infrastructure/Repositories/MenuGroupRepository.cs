using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace DynamicMenu.Infrastructure.Repositories
{
    public class MenuGroupRepository : IMenuGroupRepository
    {
        private readonly DynamicMenuDbContext _context;

        public MenuGroupRepository(DynamicMenuDbContext context)
        {
            _context = context;
        }

        public async Task<MenuGroup> GetByIdAsync(int id)
        {
            return await _context.MenuGroup.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MenuGroup>> GetAllAsync()
        {
            return await _context.MenuGroup.Include(a => a.Menus).ToListAsync();
        }

        public async Task<MenuGroup> AddAsync(MenuGroup menuGroupItem)
        {
            await _context.MenuGroup.AddAsync(menuGroupItem);
            await _context.SaveChangesAsync();
            return menuGroupItem;
        }

        public async Task<bool> UpdateAsync(MenuGroup menuGroupItem)
        {
            _context.Entry(menuGroupItem).State = EntityState.Modified;
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.MenuGroup.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuGroup.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }

    }
}