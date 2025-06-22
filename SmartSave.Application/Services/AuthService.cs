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

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            if (string.IsNullOrEmpty(registerRequestDto.FirstName)
                || string.IsNullOrEmpty(registerRequestDto.LastName)
                || string.IsNullOrEmpty(registerRequestDto.Email)
                || string.IsNullOrEmpty(registerRequestDto.Password))
                return new RegisterResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Every field is required" };

            if (!ValidatorHelper.IsValidEmail(registerRequestDto.Email))
                return new RegisterResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Enter a valid email." };

            if (!ValidatorHelper.IsStrongPassword(registerRequestDto.Password))
                return new RegisterResponseDto { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Password must be at least 8 characters and contain uppercase, lowercase and number." };

            if (registerRequestDto.Password != registerRequestDto.ConfirmPassword)
                return new RegisterResponseDto() { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "Passwords must be the same." };

            if (await _userRepository.IsEmailTakenAsync(registerRequestDto.Email))
                return new RegisterResponseDto() { HasError = true, StatusCode = StatusCodes.Status400BadRequest, ErrorMessage = "That email is already taken." };

            var user = new User()
            {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Email = registerRequestDto.Email,
                Password = registerRequestDto.Password,
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

            var token = _jwTokenGeneratorService.GenerateToken(user.FirstName, user.Email);
            return new LoginResponseDto
            {
                FirstName = user.FirstName,
                Email = user.Email,
                Token = token,
            };
        }
    }
}
