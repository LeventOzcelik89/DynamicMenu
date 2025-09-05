using DynamicMenu.API;
using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Enums;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMenu.Web.Controllers
{
    public class MenuGroupController : Controller
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuGroupController(IMenuGroupRepository menuGroupRepository, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _menuRepository = menuRepository;
            _menuGroupRepository = menuGroupRepository;
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<ResultStatus<IEnumerable<MenuGroup>>> GetAll()
        {
            var url = "MenuGroup/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<IEnumerable<MenuGroup>>(url);

            return res ?? ResultStatus<IEnumerable<MenuGroup>>.Error();
        }

        [HttpGet]
        public async Task<ActionResult> Insert()
        {
            return View(new CreateMenuGroupDto());
        }

        [HttpPost]
        public async Task<ResultStatus<MenuGroup>> Insert(CreateMenuGroupDto item)
        {
            var url = "MenuGroup/Insert";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonDataResultStatus<MenuGroup>(url, item);
            return res ?? ResultStatus<MenuGroup>.Error();
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var url = $"MenuGroup/GetById/{id}";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<MenuGroup>(url);

            if (!res.success)
            {
                HttpContext.Session.SetString("feedback", FeedBack.Error(res?.message).ToJson());
                return View(new UpdateMenuGroupDto());
            }

            var item = res.objects;
            var dto = new UpdateMenuGroupDto
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuType = item.MenuType,
                Name = item.Name
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<ResultStatus<bool>> Update(UpdateMenuDto item)
        {
            var url = "MenuGroup/Update";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonData<ResultStatus<bool>>(url, item);
            return res ?? ResultStatus<bool>.Error();
        }

        [HttpDelete]
        public async Task<ResultStatus<bool>> Delete(int id)
        {
            var url = $"MenuGroup/Delete/{id}";
            var res = await _remoteServiceDynamicMenuAPI.DeleteData<ResultStatus<bool>>(url);
            return res ?? ResultStatus<bool>.Error();
        }

        public async Task<ActionResult<IEnumerable<KeyValue>>> GetMenuTypes()
        {
            var menuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                .Cast<MenuTypeEnum>()
                .Select(a => new KeyValue(((byte)a).ToString(), a.ToString()))
                .ToArray();

            return Ok(menuTypes);
        }

    }
}
