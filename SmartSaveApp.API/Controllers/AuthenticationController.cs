using Microsoft.AspNetCore.Mvc;

namespace SmartSaveApp.API.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
