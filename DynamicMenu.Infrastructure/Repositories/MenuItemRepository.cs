using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DynamicMenu.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DynamicMenuDbContext _context;

        public MenuItemRepository(DynamicMenuDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItem
                //.Include(x => x.MenuItemRoles)
                //  .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItem
                //.Include(x => x.MenuItemRoles)
                //.Include(x => x.Children)
                .Where(x => x.Pid == null)
                .OrderBy(x => x.SortOrder)
                .Select(x => new MenuItem
                {
                    Id = x.Id,
                    Keyword = x.Keyword ?? string.Empty,
                    Pid = x.Pid,
                    //  Text = x.Text ?? x.Keyword ?? string.Empty,
                    //  TextEn = x.TextEn ?? x.Text ?? x.Keyword ?? string.Empty,
                    //  IconPath = x.IconPath ?? string.Empty,
                    IsNew = x.IsNew,
                    SortOrder = x.SortOrder,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    //MenuItemRoles = x.MenuItemRoles ?? new List<MenuItemRole>(),
                    Children = x.Children ?? new List<MenuItem>(),
                    MenuBaseItem = x.MenuBaseItem       //  todo: ?? sonuç ne olacak bir bak.
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByAppTypeAsync(AppType appType)
        {
            return await _context.MenuItem
                //.Include(x => x.MenuItemRoles)
                //.Include(x => x.Children)
                //.Where(x => x.AppId == appType && x.Pid == null)
                .OrderBy(x => x.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByMenuGroupIdAsync(int menuGroupId)
        {
            return await _context.MenuItem
                .Where(x => x.MenuGroupId == menuGroupId)
                .Include(a => a.MenuBaseItem)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByMenuIdAsync(int menuId)
        {
            return await _context.MenuItem
                .Where(x => x.MenuId == menuId)
                .Include(a => a.MenuBaseItem)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByMenuGroupIdMenuIdAsync(int menuGroupId, int menuId)
        {
            return await _context.MenuItem
                .Where(x => x.MenuId == menuId && x.MenuGroupId == menuGroupId)
                .Include(a => a.MenuBaseItem)
                .ToListAsync();
        }

        public async Task<MenuItem> AddAsync(MenuItem menuItem)
        {
            await _context.MenuItem.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            _context.Entry(menuItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItem.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MenuItem>> GetByMenuIdsAsync(IEnumerable<int> menuId)
        {
            return await _context.MenuItem
                .Where(x => menuId.Contains(x.MenuGroupId))
                .ToListAsync();
        }
    }
}