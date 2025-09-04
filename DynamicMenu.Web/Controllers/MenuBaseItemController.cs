using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;
using DynamicMenu.API;

namespace DynamicMenu.Web.Controllers
{
    public class MenuBaseItemController : Controller
    {
        private readonly IMenuBaseItemRepository _menuBaseItemRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuBaseItemController(IMenuBaseItemRepository menuBaseItemRepository, RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
            _menuBaseItemRepository = menuBaseItemRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ResultStatus<IEnumerable<MenuBaseItemDto>>?> GetAll()
        {
            var url = "MenuBaseItem/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<IEnumerable<MenuBaseItemDto>>(url);
            return res;
        }

        public async Task<IEnumerable<MenuBaseItemDto>?> GetAllData()
        {
            var res = await GetAll();
            return res?.objects;
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
        public async Task<ResultStatus<MenuBaseItem>> Insert(CreateMenuBaseItemDto item)
        {
            var url = "MenuBaseItem/Insert";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonDataResultStatus<MenuBaseItem>(url, item);
            return res ?? ResultStatus<MenuBaseItem>.Error();
        }


        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var url = $"MenuBaseItem/GetById/{id}";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<MenuBaseItem>(url);

            if (!res.success)
            {
                HttpContext.Session.SetString("feedback", FeedBack.Error(res?.message).ToJson());
                return View(new UpdateMenuBaseItemDto());
            }

            var dto = new UpdateMenuBaseItemDto
            {
                Id = res.objects.Id,
                IconPath = res.objects.IconPath,
                Text = res.objects.Text,
                TextEn = res.objects.TextEn
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMenuBaseItemDto item)
        {
            var url = "MenuBaseItem/Update";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonData<ResultStatus<bool>>(url, item);
            return Ok(res ?? new ResultStatus<bool> { feedback = new FeedBack { message = "Hata oluştu" } });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var url = $"MenuBaseItem/Delete/{id}";
            var res = await _remoteServiceDynamicMenuAPI.DeleteData<ResultStatus<bool>>(url);
            return Ok(res ?? new ResultStatus<bool> { feedback = new FeedBack { message = "Hata oluştu" } });
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

    }
}
