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

        public async Task<ActionResult<IEnumerable<Menu>>> GetAll(VMMenuGroupDetail? MenuGroupDetail = null)
        {
            var url = $"Menu/GetAll";
            url = MenuGroupDetail?.MenuGroupId != null ? url + $"?menuGroupId={MenuGroupDetail.MenuGroupId}" : url;
            var res = await _remoteServiceDynamicMenuAPI.GetData<IEnumerable<Menu>>(url);

            return Ok(res);
        }

        public async Task<ActionResult<IEnumerable<Menu>>> GetByMenuGroup(int id)
        {
            var menus = await _menuRepository.GetByMenuGroupIdAsync(id);
            return Ok(menus);
        }

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

        [HttpGet]
        public async Task<ActionResult> Insert(int MenuGroupId)
        {
            return View(new CreateMenuDto());
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> Insert(CreateMenuDto item)
        {
            var dto = new Menu
            {
                CreatedDate = DateTime.Now,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuTarget = item.MenuTarget,
                MenuGroupId = item.MenuGroupId,
                Name = item.Name
            };
            var res = await _menuRepository.AddAsync(dto);
            return Ok(new ResultStatus<Menu> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpGet]
        public async Task<ActionResult<Menu>> Update(int id)
        {
            var item = await _menuRepository.GetByIdAsync(id);
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
        public async Task<ActionResult<Menu>> Update(UpdateMenuDto item)
        {
            //  API giderek işletmemiz gerekecek.
            var dto = new Menu
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                Name = item.Name,
                ModifiedDate = DateTime.Now,
                MenuGroupId = item.MenuGroupId,
                MenuTarget = item.MenuTarget
            };
            var res = await _menuRepository.UpdateAsync(dto);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _menuRepository.DeleteAsync(id);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = true });
        }

    }
}
