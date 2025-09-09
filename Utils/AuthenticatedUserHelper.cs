using System.Security.Claims;

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

        //public string GenerateAccessToken(User user)
        //{
        //    var tokenHandler = 
        //}
    }
}
