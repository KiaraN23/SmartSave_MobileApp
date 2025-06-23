using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SmartSave.Application.DTOs;
using SmartSave.Application.Helper;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;
using SmartSave.Core.Entities;

namespace SmartSave.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public readonly IConfiguration _configuration;
        public readonly IJwTokenGeneratorService _jwTokenGeneratorService;

        public AuthService(IUserRepository userRepository,
                            IConfiguration configuration,
                            IJwTokenGeneratorService jwTokenGeneratorService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _jwTokenGeneratorService = jwTokenGeneratorService;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto req)
        {
            if (string.IsNullOrEmpty(req.FirstName)
                || string.IsNullOrEmpty(req.LastName)
                || string.IsNullOrEmpty(req.Email)
                || string.IsNullOrEmpty(req.Password))
                return new RegisterResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Every field is required" };

            if (!ValidatorHelper.IsValidEmail(req.Email))
                return new RegisterResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Enter a valid email." };

            if (!ValidatorHelper.IsStrongPassword(req.Password))
                return new RegisterResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase and number." };

            if (req.Password != req.ConfirmPassword)
                return new RegisterResponseDto() { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Passwords must be the same." };

            if (await _userRepository.IsEmailTakenAsync(req.Email))
                return new RegisterResponseDto() { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "That email is already taken." };

            var user = new User()
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                Password = req.Password,
            };
            await _userRepository.RegisterAsync(user);

            return new RegisterResponseDto { };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto req)
        {
            if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
                return new LoginResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Every field is required" };

            var user = await _userRepository.GetByEmailAndPasswordAsync(req.Email, req.Password);

            if (user is null)
                return new LoginResponseDto { HasError = true, StatusCode = StatusCodes.Status401Unauthorized, ErrorMessage = "Invalid credentials" };

            var token = _jwTokenGeneratorService.GenerateToken(user.Id.ToString(), user.FirstName, user.Email);
            return new LoginResponseDto
            {
                FirstName = user.FirstName,
                Email = user.Email,
                Token = token,
            };
        }

        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(string userId, ResetPasswordRequestDto req)
        {
            if (string.IsNullOrEmpty(req.CurrentPassword) || string.IsNullOrEmpty(req.NewPassword)
                || string.IsNullOrEmpty(req.ConfirmNewPassword))
                return new ResetPasswordResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Every field is required" };

            if (req.CurrentPassword == req.NewPassword)
                return new ResetPasswordResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "New password must be different" };

            if (!ValidatorHelper.IsStrongPassword(req.NewPassword))
                return new ResetPasswordResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase and number." };

            if (req.NewPassword != req.ConfirmNewPassword)
                return new ResetPasswordResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "New password and confirm password must be the same." };

            var result = await _userRepository.ResetPassword(userId, req.CurrentPassword, req.NewPassword);

            if (!result)
                return new ResetPasswordResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "That's not the current password." };

            return new ResetPasswordResponseDto { };
        }
    }
}
