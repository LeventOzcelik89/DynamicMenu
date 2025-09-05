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
    public class MenuController : Controller
    {
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;

        public MenuController(RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
        }

        public IActionResult Index(VMMenuGroupDetail? MenuGroupDetail = null)
        {
            return View(MenuGroupDetail);
        }

        public async Task<ResultStatus<IEnumerable<Menu>>> GetAll(VMMenuGroupDetail? MenuGroupDetail = null)
        {
            var url = MenuGroupDetail?.MenuGroupId != null
                ? $"Menu/GetAll"
                : $"Menu/GetAll?menuGroupId={MenuGroupDetail?.MenuGroupId}";

            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<IEnumerable<Menu>>(url);
            return res ?? ResultStatus<IEnumerable<Menu>>.Error();
        }

        [HttpPost]
        public async Task<ResultStatus<Menu>> Insert(CreateMenuDto item)
        {
            var url = "Menu/Insert";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonDataResultStatus<Menu>(url, item);
            return res ?? ResultStatus<Menu>.Error();
        }

        [HttpGet]
        public async Task<ActionResult> Insert(int MenuGroupId)
        {
            return View(new CreateMenuDto { MenuGroupId = MenuGroupId });
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var url = $"Menu/GetById/{id}";
            var res = await _remoteServiceDynamicMenuAPI.GetDataResultStatus<Menu>(url);

            if (!res.success)
            {
                HttpContext.Session.SetString("feedback", FeedBack.Error(res?.message).ToJson());
                return View(new UpdateMenuDto());
            }
            
            var item = res.objects;
            var dto = new UpdateMenuDto
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuGroupId = item.MenuGroupId,
                MenuTarget = item.MenuTarget,
                Name = item.Name
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<ResultStatus<bool>> Update(UpdateMenuDto item)
        {
            var url = "Menu/Update";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonData<ResultStatus<bool>>(url, item);
            return res ?? ResultStatus<bool>.Error();
        }

        [HttpDelete]
        public async Task<ResultStatus<bool>> Delete(int id)
        {
            var url = $"Menu/Delete/{id}";
            var res = await _remoteServiceDynamicMenuAPI.DeleteData<ResultStatus<bool>>(url);
            return res ?? ResultStatus<bool>.Error();
        }

        public async Task<ActionResult<ResultStatus<bool>>> Save(MenuItemProcessDTO processItem)
        {
            var url = $"Menu/Save";
            var res = await _remoteServiceDynamicMenuAPI.PostJsonDataResultStatus<bool>(url, processItem);
            return res ?? ResultStatus<bool>.Error();
        }

        //  todo: buradan aşağısı kontrol edilecek.



        public async Task<IActionResult> Management(VMMenuDetail request)
        {
            //var model = await _menuGroupRepository.GetByIdAsync(menuGroupId);
            return View(request);
        }


        public async Task<ActionResult<IEnumerable<KeyValue>>> GetMenuTargets()
        {
            var menuTypes = Enum.GetValues(typeof(MenuTargetEnum))
                .Cast<MenuTargetEnum>()
                .Select(a => new KeyValue(((byte)a).ToString(), a.ToString()))
                .ToArray();

            return Ok(menuTypes);
        }


    }
}
