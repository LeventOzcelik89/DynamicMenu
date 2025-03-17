using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Models;
using DynamicMenu.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DynamicMenu.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly RemoteServiceDynamicMenuAPI _remoteServiceDynamicMenuAPI;
        public HomeController(RemoteServiceDynamicMenuAPI remoteServiceDynamicMenuAPI)
        {
            _remoteServiceDynamicMenuAPI = remoteServiceDynamicMenuAPI;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<IEnumerable<MenuItemExport>>> GetMenu(int menuId)
        {

            var url = "Present/getall/" + menuId;
            var res = await _remoteServiceDynamicMenuAPI.GetData<MenuItemExport[]>(url);

            return Ok(res);

        }

        public async Task<ActionResult<IEnumerable<string>>> GetIcons(int menuId)
        {

            var url = "Present/geticons";
            var res = await _remoteServiceDynamicMenuAPI.GetData<string[]>(url);

            return Ok(res);

        }

        public async Task<ActionResult<ResultStatus<bool>>> Save(MenuItemDto[] items)
        {
            return Ok(new ResultStatus<bool> { feedback = new FeedBack { message = "işlem tamamlandı" }, objects = true });
        }

    }
}