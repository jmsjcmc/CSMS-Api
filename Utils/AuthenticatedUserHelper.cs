using csms_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace csms_backend.Utils
{
    public class AuthenticatedUserHelper
    {
        private readonly IConfiguration _configuration;
        public AuthenticatedUserHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Fetch authenticated User ID
        public static int GetUserId(ClaimsPrincipal user)
        {
            if (user == null)
                return 0;

            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(value, out int userId))
                return userId;

            return 0;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            if (user.UserRole != null)
            {
                foreach (var userRole in user.UserRole.Where(ur => ur.Status == Status.Active))
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
