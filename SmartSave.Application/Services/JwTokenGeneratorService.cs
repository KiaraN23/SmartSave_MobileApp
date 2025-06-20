using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartSave.Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartSave.Application.Services
{
    public class JwTokenGeneratorService : IJwTokenGeneratorService
    {
        private readonly IConfiguration _configuration;

        public JwTokenGeneratorService(IConfiguration configuration)
            => _configuration = configuration;

        public string GenerateToken(string firstName, string email)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, firstName),
            new Claim(ClaimTypes.Email, email)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
