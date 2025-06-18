using DynamicMenu.API.DTOs;
using DynamicMenu.API.Models;
using DynamicMenu.Core.Entities;
using DynamicMenu.Core.Interfaces;
using DynamicMenu.Core.Models;
using DynamicMenu.Infrastructure.Repositories;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DynamicMenu.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMenuRepository _menuRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;
        public HomeController(RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI, IMenuRepository menuRepository, IMenuItemRepository menuItemRepository, IMenuGroupRepository menuGroupRepository)
        {
            _menuRepository = menuRepository;
            _menuItemRepository = menuItemRepository;
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
            _menuGroupRepository = menuGroupRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        //public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuGroup(int menuGroupId)
        //{

        //    var url = "Present/GetMenuGroup/" + menuGroupId;
        //    var res = await _remoteServiceDynamicMenuAPI.GetData<MenuGroupModelResponse>(url);

        //    return Ok(res);

        //}

    }
}