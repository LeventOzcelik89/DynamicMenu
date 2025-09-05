using DynamicMenu.API;
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
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuItemController(RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ResultStatus<IEnumerable<MenuItemDto>>> GetAll()
        {
            var url = "MenuItem/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<IEnumerable<MenuItemDto>>(url);

            return res ?? ResultStatus<IEnumerable<MenuItemDto>>.Error();
        }

        [HttpGet]
        public async Task<ActionResult> Insert()
        {
            return View(new CreateMenuItemDto { Keyword = Guid.NewGuid().ToString().Substring(0, 8) });
        }

        [HttpPost]
        public async Task<ResultStatus<MenuItem>> Insert(CreateMenuGroupDto item)
        {
            var url = "MenuItem/Insert";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonDataResultStatus<MenuItem>(url, item);
            return res ?? ResultStatus<MenuItem>.Error();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var url = $"MenuItem/GetById/{id}";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<MenuItemDto>(url);

            if (!res.success)
            {
                HttpContext.Session.SetString("feedback", FeedBack.Error(res?.message).ToJson());
                return View(new UpdateMenuItemDto { Keyword = Guid.NewGuid().ToString().Substring(0, 8) });
            }

            var item = res.objects;
            var dto = new UpdateMenuItemDto
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
            return View(dto);
        }

        [HttpPost]
        public async Task<ResultStatus<bool>> Update(UpdateMenuItemDto item)
        {
            var url = "MenuItem/Update";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonData<ResultStatus<bool>>(url, item);
            return res ?? ResultStatus<bool>.Error();
        }

        [HttpDelete]
        public async Task<ResultStatus<bool>> Delete(int id)
        {
            var url = $"MenuItem/Delete/{id}";
            var res = await _remoteServiceDynamicMenuAPI.DeleteData<ResultStatus<bool>>(url);
            return res ?? ResultStatus<bool>.Error();
        }

        //  todo: buradan aşağısıı kontrol edilecek.















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


    }
}
