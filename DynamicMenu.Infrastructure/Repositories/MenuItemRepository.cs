using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            return await _context.MenuItems
                //.Include(x => x.MenuItemRoles)
                //  .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems
                //.Include(x => x.MenuItemRoles)
                //.Include(x => x.Children)
                .Where(x => x.Pid == null)
                .OrderBy(x => x.SortOrder)
                .Select(x => new MenuItem
                {
                    Id = x.Id,
                    Keyword = x.Keyword ?? string.Empty,
                    Pid = x.Pid,
                    Text = x.Text ?? x.Keyword ?? string.Empty,
                    TextEn = x.TextEn ?? x.Text ?? x.Keyword ?? string.Empty,
                    //DisplayType = x.DisplayType,
                    //AppId = x.AppId,
                    NewTag = x.NewTag,
                    IconPath = x.IconPath ?? string.Empty,
                    SortOrder = x.SortOrder,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    //MenuItemRoles = x.MenuItemRoles ?? new List<MenuItemRole>(),
                    Children = x.Children ?? new List<MenuItem>()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetByAppTypeAsync(AppType appType)
        {
            return await _context.MenuItems
                //.Include(x => x.MenuItemRoles)
                //.Include(x => x.Children)
                //.Where(x => x.AppId == appType && x.Pid == null)
                .OrderBy(x => x.SortOrder)
                .ToListAsync();
        }

        //public async Task<IEnumerable<MenuItem>> GetByRoleIdAsync(int roleId)
        //{
        //    return await _context.MenuItems
        //        .Include(x => x.MenuItemRoles)
        //        .Include(x => x.Children)
        //        .Where(x => x.MenuItemRoles.Any(r => r.RoleId == roleId) && x.Pid == null)
        //        .OrderBy(x => x.SortOrder)
        //        .ToListAsync();
        //}


        public async Task<IEnumerable<MenuItem>> GetByMenuIdAsync(int menuId)
        {
            return await _context.MenuItems
                .Where(x => x.MenuId == menuId)
                .ToListAsync();
        }

        public async Task<MenuItem> AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
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
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
    }
} 