using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DynamicMenu.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;
        public HomeController(RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<IEnumerable<MenuItemExport>>> GetMenu(int menuId)
        {

            var url = "Present/getall/" + menuId;
            var res = await _remoteServiceDynamicMenuAPI.GetData<MenuItemExport[]>(url);

            return Ok(res);

        }

        public async Task<ActionResult<IEnumerable<string>>> GetIcons(int menuId)
        {

            var url = "Present/geticons";
            var res = await _remoteServiceDynamicMenuAPI.GetData<string[]>(url);

            return Ok(res);

        }

        public async Task<ActionResult<ResultStatus<bool>>> Save(int menuId, MenuItemProcessDTO[] items)
        {

            var menuItems = await _menuItemRepository.GetByMenuIdAsync(menuId);

            //  Edit
            foreach (var item in items.Where(a => a.processType == MenuItemProcessType.edit))
            {
                var menuItem = menuItems.FirstOrDefault(a => a.Id == item.menuItem.Id);
                if (menuItem != null)
                {
                    menuItem.Text = item.menuItem.Text;
                    menuItem.Keyword = item.menuItem.Keyword;
                    menuItem.SortOrder = item.menuItem.SortOrder;
                    menuItem.IconPath = item.menuItem.IconPath;
                    menuItem.NewTag = item.menuItem.NewTag;
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
                    Text = item.menuItem.Text,
                    TextEn = item.menuItem.TextEn,
                    Keyword = item.menuItem.Keyword,
                    SortOrder = item.menuItem.SortOrder,
                    IconPath = item.menuItem.IconPath,
                    NewTag = item.menuItem.NewTag,
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