using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
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

    }
}
