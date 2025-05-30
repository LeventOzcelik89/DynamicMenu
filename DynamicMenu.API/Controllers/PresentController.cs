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
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly IRemoteMenusRepository _remoteMenuConfigRepository;
        private readonly DynamicMenuDbContext _context;
        private readonly AppSettings _appSettings;

        public PresentController(
            IMenuItemRepository menuItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            IMenuGroupRepository menuGroupRepository,
            AppSettings appSettings,
            DynamicMenuDbContext context)
        {
            _appSettings = appSettings;
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _menuGroupRepository = menuGroupRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _context = context;
        }

        [HttpGet("geticons")]
        public async Task<IEnumerable<string>> GetIcons()
        {

            var dir = string.Join("\\", System.IO.Directory.GetCurrentDirectory().Split('\\').SkipLast(1));
            dir += "\\" + "DynamicMenu.Web" + "\\wwwroot\\img\\icons";

            var files = System.IO.Directory.GetFiles(dir);
            return files.Select(a => { return a.Split('\\').LastOrDefault().Split('.').FirstOrDefault(); }).ToArray();

        }

        [HttpGet("GetMenuGroups")]
        public async Task<IEnumerable<MenuGroup>> GetMenuGroup()
        {
            var menuGroup = await _menuGroupRepository.GetAllAsync();
            return menuGroup;
        }

        [HttpGet("GetMenuGroup/{menuGroupId}")]
        public async Task<ActionResult<MenuGroupModelResponse>> GetMenuGroup(int menuGroupId)
        {

            var menuGroup = await _menuGroupRepository.GetByIdAsync(menuGroupId);
            if (menuGroup == null)
            {
                return NotFound();
            }

            var menus = await _menuRepository.GetByMenuGroupIdAsync(menuGroup.Id);

            var res = new MenuGroupModelResponse
            {
                menuGroup = new MenuGroupResponse
                {
                    Id = menuGroup.Id,
                    Name = menuGroup.Name,
                    IsActive = menuGroup.IsActive,
                    MenuType = menuGroup.MenuType,
                    Description = menuGroup.Description,
                },
                menus = new MenuTargetResponse
                {
                    Cards = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Cards)?.Id),
                    Profile = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Profile)?.Id),
                    Transactions = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Transactions)?.Id),
                    Applications = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Applications)?.Id),
                }
            };

            return res;
        }

        private async Task<List<MenuItemResponse>?> GetMenu(int? menuId)
        {

            if (!menuId.HasValue)
            {
                return null;
            }

            var menuItems = new List<MenuItemResponse>();
            var items = await _menuItemRepository.GetByMenuIdAsync(menuId.Value);
            foreach (var item in items.Where(a => a.Pid == null).OrderBy(a => a.SortOrder).ToArray())
            {
                menuItems.Add(new MenuItemResponse
                {
                    id = item.Id,
                    key = item.Keyword,
                    //  text = item.Text,
                    //  textEn = item.TextEn,
                    //  icon = item.IconPath,
                    MenuBaseItem = item.MenuBaseItem,   //  todo:   dolacak mı kontrol et
                    isNew = item.IsNew,
                    sortOrder = item.SortOrder,
                    items = GetMenuItems(items.ToArray(), item)
                });
            }

            return menuItems;

        }

        private MenuItemResponse[] GetMenuItems(MenuItem[] items, MenuItem item)
        {

            var itemss = items.Where(a => a.Pid == item.Id).OrderBy(a => a.SortOrder).ToArray();
            if (!itemss.Any())
            {
                return null;
            }

            var res = new List<MenuItemResponse>();
            foreach (var subItem in itemss)
            {

                res.Add(new MenuItemResponse
                {
                    id = subItem.Id,
                    pid = item.Id,
                    key = subItem.Keyword,
                    //  text = subItem.Text,
                    //  textEn = subItem.TextEn,
                    //  icon = subItem.IconPath,
                    MenuBaseItem = subItem.MenuBaseItem,
                    isNew = subItem.IsNew,
                    sortOrder = subItem.SortOrder,
                    items = GetMenuItems(items, subItem)
                });

            }

            return res.ToArray();

        }

    }
}