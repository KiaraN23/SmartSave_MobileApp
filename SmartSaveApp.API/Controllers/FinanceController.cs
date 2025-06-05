using Microsoft.AspNetCore.Mvc;

namespace SmartSaveApp.API.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
