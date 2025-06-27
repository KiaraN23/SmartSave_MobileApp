using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Services;
using System.Security.Claims;

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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
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
                return InternalServerError();
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
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
                return InternalServerError();
            }
        }

        [HttpPost("resetPassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var result = await _authService.ResetPasswordAsync(userId, resetPasswordRequestDto);

                if(result.HasError)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        status = result.StatusCode,
                        message = result.ErrorMessage
                    });
                }

                return Ok(new { message = "Password reseted correctly" });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
