using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using DynamicMenu.Core.Entities;
using DynamicMenu.Infrastructure.Data;
using DynamicMenu.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicMenu.Infrastructure.Helpers
{
    public class MenuImportHelper
    {
        private readonly DynamicMenuDbContext _context;

        public MenuImportHelper(DynamicMenuDbContext context)
        {
            _context = context;
        }

        public async Task ImportMenuFromJson(string jsonContent, int menuGroupId = 1)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var menuData = JsonSerializer.Deserialize<List<MenuImportModel>>(jsonContent, options);

            foreach (var menu in menuData)
            {
                foreach (var item in menu.MenuList)
                {
                    await ProcessMenuItems(item, null, menuGroupId);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task ProcessMenuItems(MenuListItem item, int? parentId, int menuGroupId)
        {

            var menuBaseItem = new MenuBaseItem
            {
                Text = item.ItemText,
                TextEn = item.ItemText,
            };

            await _context.MenuBaseItem.AddAsync(menuBaseItem);

            var menuItem = new MenuItem
            {
                Keyword = item.ItemKey,
                Pid = parentId,
                //DisplayType = item.DisplayType == 1,
                IsNew = item.IsNew,
                //CategoryName = item.CategoryName ?? string.Empty,
                //AppId = Core.Enums.AppType.Android,
                CreatedDate = DateTime.UtcNow,
                MenuGroupId = menuGroupId,
                MenuBaseItemId = menuBaseItem.Id,
            };

            await _context.MenuItem.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            if (item.SubList != null && item.SubList.Any())
            {
                foreach (var subItem in item.SubList)
                {
                    if (subItem.MenuList != null)
                    {
                        foreach (var menuListItem in subItem.MenuList)
                        {
                            await ProcessMenuItems(menuListItem, menuItem.Id, menuGroupId);
                        }
                    }
                }
            }
        }
    }
}