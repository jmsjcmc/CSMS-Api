using System.Security.Claims;

namespace csms_backend.Utils.Validators
{
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
    }
}
