using CSMapi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CSMapi.Helpers
{
    public class AuthUserHelper
    {
        private readonly IConfiguration _configuration;

        public AuthUserHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Fetch authenticated user first name and last name
        public static string GetFullName(ClaimsPrincipal user)
        {
            var firstName = user.FindFirst("FirstName")?.Value ?? string.Empty;
            var lastName = user.FindFirst("LastName")?.Value ?? string.Empty;
            return $"{firstName} {lastName}".Trim();
        }
        // Fetch authenticated user id
        public static int GetUserId(ClaimsPrincipal user)
        {
            if (user == null)
                return 0;

            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(value, out int userId))
            {
                return userId;
            }

            return 0;
        }
        // Fetch authenticated user username
        public static string GetUsername(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }
        // Fetch authenticated user access token
        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim("FirstName", user.Firstname), 
                new Claim("LastName", user.Lastname), 
                new Claim(ClaimTypes.Name, user.Username), 
                new Claim(ClaimTypes.Role, user.Role) 
            };
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), 
                Expires = DateTime.UtcNow.AddHours(12), 
                Issuer = _configuration["Jwt:Issuer"], 
                Audience = _configuration["Jwt:Audience"], 
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

}
