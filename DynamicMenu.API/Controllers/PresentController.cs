using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Enums;
using DynamicMenu.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using DynamicMenu.Infrastructure.Helpers;
using DynamicMenu.API.Models.Dtos;
using DynamicMenu.Application;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Core.Models;
using Newtonsoft;

namespace DynamicMenu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRemoteMenusRepository _remoteMenuConfigRepository;
        private readonly DynamicMenuDbContext _context;

        public PresentController(
            IMenuItemRepository menuItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            DynamicMenuDbContext context)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _context = context;
        }

        [HttpGet("geticons")]
        public async Task<IEnumerable<string>> GetIcons()
        {

            var files = System.IO.Directory.GetFiles(@"D:\Repositories\DynamicMenu\DynamicMenu.Web\wwwroot\img\icons");
            return files.Select(a => { return a.Split('\\').LastOrDefault().Split('.').FirstOrDefault(); }).ToArray();

        }
        [HttpGet("getall/{menuId}")]
        public async Task<ActionResult<IEnumerable<MenuItemExport>>> GetAll(int menuId)
        {

            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
            {
                return NotFound();
            }

            var res = new List<MenuItemExport>();
            var items = await _menuItemRepository.GetByMenuIdAsync(menu.Id);
            foreach (var item in items.Where(a => a.Pid == null).ToArray())
            {

                res.Add(new MenuItemExport
                {
                    id = item.Id,
                    key = item.Keyword,
                    text = item.Text,
                    icon = item.IconPath,
                    isNew = item.NewTag,
                    sortOrder = item.SortOrder,
                    items = GetMenuItems(items.ToArray(), item)
                });

            }

            return res;
        }

        private MenuItemExport[] GetMenuItems(MenuItem[] items, MenuItem item)
        {

            var itemss = items.Where(a => a.Pid == item.Id).ToArray();
            if (!itemss.Any())
            {
                return null;
            }

            var res = new List<MenuItemExport>();
            foreach (var subItem in itemss)
            {

                res.Add(new MenuItemExport
                {
                    id = subItem.Id,
                    parent = item.Id,
                    key = subItem.Keyword,
                    text = subItem.Text,
                    icon = subItem.IconPath,
                    isNew = subItem.NewTag,
                    sortOrder = subItem.SortOrder,
                    items = GetMenuItems(items, subItem)
                });

            }

            return res.ToArray();

        }

    }
}