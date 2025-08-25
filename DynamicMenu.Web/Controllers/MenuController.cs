using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMenu.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuController(IMenuGroupRepository menuGroupRepository, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _menuGroupRepository = menuGroupRepository;
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<IEnumerable<MenuGroup>>> GetAll()
        {

            var url = "Menu/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetData<IEnumerable<MenuGroup>>(url);

            return Ok(res);

        }

        public async Task<ActionResult<IEnumerable<Menu>>> GetByMenuGroup(int id)
        {
            var menus = await _menuRepository.GetByMenuGroupIdAsync(id);
            return Ok(menus);
        }

        public async Task<IActionResult> Update(VMMenuItemUpdate request)
        {
            //var model = await _menuGroupRepository.GetByIdAsync(menuGroupId);
            return View(request);
        }

        public async Task<ActionResult<ResultStatus<bool>>> Save(int menuId, MenuItemProcessDTO[] items)
        {
            try
            {
                var menuItems = await _menuItemRepository.GetByMenuIdAsync(menuId);

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

                return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = true });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<MenuDto[]> GetDataSource()
        {
            var url = "Menu/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetData<IEnumerable<Menu>>(url);
            var items = res.Select(a => new MenuDto
            {
                Id = a.Id,
                Description = a.Description,
                IsActive = a.IsActive,
                Name = a.Name
            }).ToArray();
            return items;
        }

    }
}
