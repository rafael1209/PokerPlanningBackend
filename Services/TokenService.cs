using System.IdentityModel.Tokens.Jwt;
using PokerPlanningBackend.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PokerPlanningBackend.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly string _secret = configuration["Jwt:SecretKey"]!;
        private readonly string? _issuer = configuration["Jwt:Issuer"];

        private const int AccessTokenExpirationMinutes = 15;

        public string GenerateAccessToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(AccessTokenExpirationMinutes),
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
