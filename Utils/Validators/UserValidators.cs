using System.Security.Claims;

namespace csms_backend.Utils.Validators
{
    public class JwtSetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    public class UserValidators
    {
        public static int ValidateUserClaim(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID claim not found.");

            if (!int.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedAccessException("Invalid user ID claim.");

            return userId;
        }

        public static JwtSetting GetJwtSetting(IConfiguration configuration)
        {
            var key = configuration["Jwt:Key"] ??
                throw new InvalidOperationException("JWT key not configured.");
            var issuer = configuration["Jwt:Issuer"] ??
                throw new InvalidOperationException("JWT issuer not configured.");
            var audience = configuration["Jwt:Audience"] ??
                throw new InvalidOperationException("JWT audience not configured.");
            return new JwtSetting
            {
                Key = key,
                Issuer = issuer,
                Audience = audience
            };
        }
    }
}
