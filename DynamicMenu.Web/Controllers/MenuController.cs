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


        //  todo: buradan aşağısı kontrol edilecek.







        public async Task<IActionResult> Management(VMMenuDetail request)
        {
            //var model = await _menuGroupRepository.GetByIdAsync(menuGroupId);
            return View(request);
        }

        public async Task<ActionResult<ResultStatus<bool>>> Save(int menuId, int menuGroupId, MenuItemProcessDTO[] items)
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
                        //  menuItem.MenuBaseItem.Text = item.menuItem.Text
                        //  menuItem.Text = item.menuItem.Text;
                        //  menuItem.IconPath = item.menuItem.IconPath;
                        //  menuItem.MenuBaseItemId = item.menuItem.MenuBaseItemId; //  todo: kontrol et.
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
                        MenuId = menuId,
                        MenuGroupId = menuGroupId,
                        MenuBaseItemId = item.menuItem.MenuBaseItem.Id,
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
