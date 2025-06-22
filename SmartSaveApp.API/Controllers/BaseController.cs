using Microsoft.AspNetCore.Mvc;

namespace SmartSaveApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase 
    {
        protected IActionResult InternalServerError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                status = StatusCodes.Status500InternalServerError,
                message = "An unexpected error occurred."
            });
        }
    }
}
