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
    public class MenuItemController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuItemController(IMenuGroupRepository menuGroupRepository, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
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

        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
        {

            var url = "MenuItem/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetData<IEnumerable<MenuItemDto>>(url);

            return Ok(res);

        }

        public async Task<ActionResult<List<MenuItemResponse>?>> GetMenuItemsByMenu(int menuGroupId, int menuId)
        {
            var url = $"MenuItem/GetMenuItemsByMenu/{menuGroupId}/{menuId}";
            var res = await _remoteServiceDynamicMenuAPI.GetData<List<MenuItemResponse>>(url);
            return res;
        }

        public async Task<ActionResult<List<MenuItemResponseUpdate>?>> GetMenuItemsByMenuUpdate(int menuGroupId, int menuId)
        {
            var url = $"MenuItem/GetMenuItemsByMenuUpdate/{menuGroupId}/{menuId}";
            var res = await _remoteServiceDynamicMenuAPI.GetData<List<MenuItemResponseUpdate>>(url);
            return res;
        }

        [HttpGet]
        public async Task<ActionResult> Insert()
        {
            return View(new CreateMenuItemDto { Keyword = Guid.NewGuid().ToString().Substring(0, 8) });
        }

        [HttpPost]
        public async Task<ActionResult> Insert(CreateMenuItemDto item)
        {
            var dto = new MenuItem
            {
                CreatedDate = DateTime.Now,
                IsNew = item.IsNew,
                Keyword = item.Keyword,
                MenuBaseItemId = item.MenuBaseItemId,
                MenuGroupId = item.MenuGroupId,
                MenuId = item.MenuId,
                Pid = item.Pid
            };

            var res = await _menuItemRepository.AddAsync(dto);
            return Ok(new ResultStatus<MenuItem> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var item = await _menuItemRepository.GetByIdAsync(id);
            var itemDto = new UpdateMenuItemDto
            {
                Id = item.Id,
                Keyword = item.Keyword,
                MenuBaseItemId = item.MenuBaseItemId,
                MenuGroupId = item.MenuGroupId,
                IsNew = item.IsNew,
                MenuId = item.MenuId,
                Pid = item.Pid,
                SortOrder = item.SortOrder
            };
            return View(itemDto);
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateMenuItemDto item)
        {
            var dto = new MenuItem
            {
                Id = item.Id,
                CreatedDate = DateTime.Now,
                IsNew = item.IsNew,
                Keyword = item.Keyword,
                MenuBaseItemId = item.MenuBaseItemId,
                MenuGroupId = item.MenuGroupId,
                MenuId = item.MenuId,
                Pid = item.Pid
            };

            var res = await _menuItemRepository.UpdateAsync(dto);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _menuItemRepository.DeleteAsync(id);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = true });
        }

    }
}
