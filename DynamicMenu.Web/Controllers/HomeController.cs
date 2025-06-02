using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DynamicMenu.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;
        public HomeController(RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, IMenuGroupRepository menuGroupRepository)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
            _menuGroupRepository = menuGroupRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> MenuUpdate(int menuGroupId)
        {
            var model = await _menuGroupRepository.GetByIdAsync(menuGroupId);
            return View(model);
        }

        public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuGroup(int menuGroupId)
        {

            var url = "Present/GetMenuGroup/" + menuGroupId;
            var res = await _remoteServiceDynamicMenuAPI.GetData<MenuGroupModelResponse>(url);

            return Ok(res);

        }

        public async Task<ActionResult<IEnumerable<string>>> GetIcons(int menuId)
        {

            var url = "Present/geticons";
            var res = await _remoteServiceDynamicMenuAPI.GetData<string[]>(url);

            return Ok(res);

        }

        public async Task<ActionResult<ResultStatus<bool>>> Save(int menuGroupId, MenuItemProcessDTO[] items)
        {

            var menuGroup = await _menuGroupRepository.GetByIdAsync(menuGroupId);
            var menus = await _menuRepository.GetByMenuGroupIdAsync(menuGroup.Id);
            var menuItems = await _menuItemRepository.GetByMenuIdsAsync(menus.Select(a => a.Id));

            //  Edit
            foreach (var item in items.Where(a => a.processType == MenuItemProcessType.edit))
            {
                var menuItem = menuItems.FirstOrDefault(a => a.Id == item.menuItem.Id);
                if (menuItem != null)
                {
                    //  menuItem.Text = item.menuItem.Text;
                    //  menuItem.IconPath = item.menuItem.IconPath;
                    menuItem.MenuBaseItemId = item.menuItem.MenuBaseItemId; //  todo: kontrol et.
                    menuItem.Keyword = item.menuItem.Keyword;
                    menuItem.SortOrder = item.menuItem.SortOrder;
                    menuItem.IsNew = item.menuItem.IsNew;
                    menuItem.Pid = item.menuItem.Pid;
                    menuItem.ModifiedDate = DateTime.Now;

                    await _menuItemRepository.UpdateAsync(menuItem);
                }

            }

            //  Remove
            foreach (var item in items.Where(a => a.processType == MenuItemProcessType.remove))
            {
                var menuItem = menuItems.FirstOrDefault(a => a.Id == item.menuItem.Id);
                if (menuItem != null)
                {
                    await _menuItemRepository.DeleteAsync(menuItem.Id);
                }
            }

            //  Add
            foreach (var item in items.Where(a => a.processType == MenuItemProcessType.add))
            {
                var menuItem = new MenuItem
                {
                    //  Text = item.menuItem.Text,
                    //  TextEn = item.menuItem.TextEn,
                    //  IconPath = item.menuItem.IconPath,
                    MenuBaseItemId = item.menuItem.MenuBaseItemId,  //  todo: bak
                    Keyword = item.menuItem.Keyword,
                    SortOrder = item.menuItem.SortOrder,
                    IsNew = item.menuItem.IsNew,
                    Pid = item.menuItem.Pid,
                    CreatedDate = DateTime.Now
                };
                await _menuItemRepository.AddAsync(menuItem);
            }

            //menuItems

            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = true });
        }

    }
}