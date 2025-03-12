using Microsoft.AspNetCore.Mvc;

namespace DynamicMenu.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 