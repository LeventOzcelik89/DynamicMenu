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

        public PresentController(
            IMenuItemRepository menuItemRepository,
            IMenuRepository menuRepository,
            IRemoteMenusRepository remoteMenuConfigRepository,
            IMenuGroupRepository menuGroupRepository,
            DynamicMenuDbContext context)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteMenuConfigRepository = remoteMenuConfigRepository;
            _menuGroupRepository = menuGroupRepository;
            _context = context;
        }

        [HttpGet("geticons")]
        public async Task<IEnumerable<string>> GetIcons()
        {

            var files = System.IO.Directory.GetFiles(@"Z:\DynamicMenu\DynamicMenu.Web\wwwroot\img\icons");
            return files.Select(a => { return a.Split('\\').LastOrDefault().Split('.').FirstOrDefault(); }).ToArray();

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
                    text = item.Text,
                    textEn = item.TextEn,
                    icon = item.IconPath,
                    isNew = item.NewTag,
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
                    text = subItem.Text,
                    textEn = subItem.TextEn,
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