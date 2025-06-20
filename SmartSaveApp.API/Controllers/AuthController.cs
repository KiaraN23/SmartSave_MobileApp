using Microsoft.AspNetCore.Mvc;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Services;

namespace SmartSaveApp.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => _authService = authService;

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { status = 400, message = "Every field is required" });

                var result = await _authService.LoginAsync(loginRequest);
                if (result is null)
                    return Unauthorized(new { status = 401, message = "Invalid email or password." });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = 500,
                    message = "An unexpected error occurred."
                });
            }
        }
    }
}
