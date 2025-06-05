using Microsoft.AspNetCore.Mvc;

namespace SmartSaveApp.API.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
