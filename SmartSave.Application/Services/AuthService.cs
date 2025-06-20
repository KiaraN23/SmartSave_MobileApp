using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartSave.Application.DTOs;
using SmartSave.Application.Interfaces.Repositories;
using SmartSave.Application.Interfaces.Services;

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

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(request.Email, request.Password);
            if (user == null) return null;

            var token = _jwTokenGeneratorService.GenerateToken(user.FirstName, user.Email);
            return new LoginResponseDto { FirstName = user.FirstName, Email = user.Email, Token = token };
        }
    }
}
