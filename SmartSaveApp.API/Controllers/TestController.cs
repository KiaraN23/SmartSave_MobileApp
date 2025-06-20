using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartSaveApp.API.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet("secure")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSecureData()
        {
            var user = User.Identity?.Name ?? "usuario anónimo";
            return Ok($"Acceso autorizado. Hola, {user}.");
        }
    }
}
