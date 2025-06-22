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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            try
            {
                var result = await _authService.LoginAsync(loginRequest);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "An unexpected error occurred."
                });
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerRequestDto);

                if (result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = StatusCodes.Status500InternalServerError,
                    message = "An unexpected error occurred."
                });
            }
        }
    }
}
