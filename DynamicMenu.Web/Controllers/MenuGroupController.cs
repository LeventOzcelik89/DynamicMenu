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

        public async Task<ActionResult<IEnumerable<MenuGroup>>> GetAll()
        {
            var url = "MenuGroup/GetAll";
            var res = await _remoteServiceDynamicMenuAPI.GetData<IEnumerable<MenuGroup>>(url);

            return Ok(res);

        }

        public async Task<ActionResult<IEnumerable<KeyValue>>> GetMenuTypes()
        {
            var menuTypes = Enum.GetValues(typeof(MenuTypeEnum))
                .Cast<MenuTypeEnum>()
                .Select(a => new KeyValue(((byte)a).ToString(), a.ToString()))
                .ToArray();

            return Ok(menuTypes);
        }

        [HttpGet]
        public async Task<ActionResult> Insert()
        {
            return View(new CreateMenuGroupDto());
        }

        [HttpPost]
        public async Task<ActionResult<MenuGroup>> Insert(CreateMenuGroupDto item)
        {
            var dto = new MenuGroup
            {
                CreatedDate = DateTime.Now,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuType = item.MenuType,
                Name = item.Name
            };
            var res = await _menuGroupRepository.AddAsync(dto);
            return Ok(new ResultStatus<MenuGroup> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpGet]
        public async Task<ActionResult<MenuGroup>> Update(int id)
        {
            var item = await _menuGroupRepository.GetByIdAsync(id);
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
        public async Task<ActionResult<MenuGroup>> Update(UpdateMenuGroupDto item)
        {
            //  API giderek işletmemiz gerekecek.
            var dto = new MenuGroup
            {
                Id = item.Id,
                Description = item.Description,
                IsActive = item.IsActive,
                MenuType = item.MenuType,
                Name = item.Name,
                ModifiedDate = DateTime.Now
            };
            var res = await _menuGroupRepository.UpdateAsync(dto);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = res });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _menuGroupRepository.DeleteAsync(id);
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "Silme işlemi tamamlandı" }, objects = res });
        }

    }
}
