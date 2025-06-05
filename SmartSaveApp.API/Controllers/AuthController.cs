using Microsoft.AspNetCore.Mvc;

namespace SmartSaveApp.API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
