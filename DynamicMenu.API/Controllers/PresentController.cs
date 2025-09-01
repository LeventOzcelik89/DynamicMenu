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

        //[HttpGet("GetMenuGroup/{menuGroupId}")]
        //public async Task<ActionResult<MenuGroupModelResponse>> GetMenuGroup(int menuGroupId)
        //{

        //    var menuGroup = await _menuGroupRepository.GetByIdAsync(menuGroupId);
        //    if (menuGroup == null)
        //    {
        //        return NotFound();
        //    }

        //    var menus = await _menuRepository.GetByMenuGroupIdAsync(menuGroup.Id);

        //    var res = new MenuGroupModelResponse
        //    {
        //        menuGroup = new MenuGroupResponse
        //        {
        //            Id = menuGroup.Id,
        //            Name = menuGroup.Name,
        //            IsActive = menuGroup.IsActive,
        //            MenuType = menuGroup.MenuType,
        //            Description = menuGroup.Description,
        //        },
        //        menus = new MenuTargetResponse
        //        {
        //            Cards = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Cards)?.Id ?? 0, 1),
        //            Profile = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Profile)?.Id ?? 0, 1),
        //            Transactions = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Transactions)?.Id ?? 0, 1),
        //            Applications = await GetMenu(menus.FirstOrDefault(a => a.MenuTarget == MenuTarget.Applications)?.Id ?? 0, 1),
        //        }
        //    };

        //    return res;
        //}



        

        

    }
}