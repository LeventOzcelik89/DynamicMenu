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
    public class MenuBaseItemController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly IMenuBaseItemRepository _menuBaseItemRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuBaseItemController(IMenuBaseItemRepository menuBaseItemRepository, IMenuGroupRepository menuGroupRepository, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _menuGroupRepository = menuGroupRepository;
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
            _menuBaseItemRepository = menuBaseItemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<IEnumerable<MenuBaseItemDto>>> GetAll()
        {

            var url = "MenuBaseItem/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetData<IEnumerable<MenuBaseItemDto>>(url);

            return Ok(res);

        }

        public async Task<ActionResult<List<MenuItemResponse>?>> GetMenuItemsByMenu(int menuGroupId, int menuId)
        {
            var url = $"MenuItem/GetMenuItemsByMenu/{menuGroupId}/{menuId}";
            var res = await _remoteServiceDynamicMenuAPI.GetData<List<MenuItemResponse>>(url);
            return res;
        }

        [HttpGet]
        public async Task<ActionResult> Insert()
        {
            return View(new CreateMenuBaseItemDto());
        }


        [HttpPost]
        public async Task<ActionResult> Insert(CreateMenuBaseItemDto item)
        {
            var dto = new MenuBaseItem
            {
                CreatedDate = DateTime.Now,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn
            };
            var res = await _menuBaseItemRepository.AddAsync(dto);
            return Ok(new ResultStatus<MenuBaseItem> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }


        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var item = await _menuBaseItemRepository.GetByIdAsync(id);
            return View(MapToDto(item));
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateMenuBaseItemDto item)
        {
            //  var dbItem = await _menuBaseItemRepository.GetByIdAsync(item.Id);
            var dto = new MenuBaseItem
            {
                Id = item.Id,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn
            };
            var res = await _menuBaseItemRepository.UpdateAsync(dto);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Güncelleme işlemi tamamlandı" }, objects = res });
        }

        private static UpdateMenuBaseItemDto MapToDto(MenuBaseItem item)
        {
            if (item == null) return null;

            return new UpdateMenuBaseItemDto
            {
                Id = item.Id,
                IconPath = item.IconPath,
                Text = item.Text,
                TextEn = item.TextEn
            };
        }

        [HttpGet]
        public async Task<ActionResult> MenuIcons()
        {
            var url = "Present/geticons";
            var res = await _remoteServiceDynamicMenuAPI.GetData<string[]>(url);

            return View(res);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> GetIcons()
        {

            var url = "Present/geticons";
            var res = await _remoteServiceDynamicMenuAPI.GetData<string[]>(url);

            return Ok(res);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _menuBaseItemRepository.DeleteAsync(id);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = true });
        }

    }
}
