using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DynamicMenu.Infrastructure.Repositories
{
    public class MenuBaseItemRepository : IMenuBaseItemRepository
    {
        private readonly DynamicMenuDbContext _context;

        public MenuBaseItemRepository(DynamicMenuDbContext context)
        {
            _context = context;
        }

        public async Task<MenuBaseItem> GetByIdAsync(int id)
        {
            return await _context.MenuBaseItem
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<IEnumerable<MenuBaseItem>> GetAllAsync()
        {
            return await _context.MenuBaseItem
                .Select(x => new MenuBaseItem
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    IconPath = x.IconPath,
                    //  MenuItems = x.MenuItems,
                    Text = x.Text,
                    TextEn = x.TextEn,
                })
                .ToListAsync();
        }

        public async Task<MenuBaseItem> AddAsync(MenuBaseItem MenuBaseItem)
        {
            await _context.MenuBaseItem.AddAsync(MenuBaseItem);
            await _context.SaveChangesAsync();
            return MenuBaseItem;
        }

        public async Task UpdateAsync(MenuBaseItem MenuBaseItem)
        {
            _context.Entry(MenuBaseItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var MenuBaseItem = await _context.MenuBaseItem.FindAsync(id);
            if (MenuBaseItem != null)
            {
                _context.MenuBaseItem.Remove(MenuBaseItem);
                await _context.SaveChangesAsync();
            }
        }



    }
}