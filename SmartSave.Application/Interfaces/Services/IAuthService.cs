using SmartSave.Application.DTOs;

namespace SmartSave.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ResetPasswordResponseDto> ResetPasswordAsync(string userId, ResetPasswordRequestDto resetPasswordRequestDto);
    }
}
