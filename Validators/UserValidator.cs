using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSMapi.Validators
{
    public class UserValidator
    {
        private readonly AppDbContext _context;
        public UserValidator(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateUserRequest(UserRequest request)
        {
            var trimmedRoles = request.Roles
                .Select(r => r.Trim()).ToList();

            var existingRoles = await _context.Roles
                .Where(r => trimmedRoles.Contains(r.Rolename))
                .Select(r => r.Rolename)
                .ToListAsync();

            var invalidRoles = trimmedRoles.Except(existingRoles).ToList();

            if (string.IsNullOrWhiteSpace(request.Firstname))
            {
                throw new ArgumentException("First Name required.");
            }
            if (string.IsNullOrWhiteSpace(request.Lastname))
            {
                throw new ArgumentException("Last Name required.");
            }
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new ArgumentException("Username required.");
            }
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                throw new ArgumentException("Username taken.");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Password required.");
            }
            if (request.Password.Length < 6)
            {
                throw new ArgumentException("Password must be atleast 6 character long.");
            }
            if (string.IsNullOrWhiteSpace(request.Businessunit))
            {
                throw new ArgumentException("Business Unit required.");
            }
            if (string.IsNullOrWhiteSpace(request.Businessunitlocation))
            {
                throw new ArgumentException("Business Unit Location required.");
            }
            if (string.IsNullOrWhiteSpace(request.Department))
            {
                throw new ArgumentException("Department required.");
            }
            if (string.IsNullOrWhiteSpace(request.Position))
            {
                throw new ArgumentException("Position required.");
            }
            if (request.Roles == null || !request.Roles.Any())
            {
                throw new ArgumentException("Atleast one role must be assigned.");
            }
            if (invalidRoles.Any())
            {
                throw new ArgumentException($"Invalid roles: {string.Join(", ", invalidRoles)}");
            }
        }
        public async Task ValidateUserUpdateRequest(UserRequest request, int id)
        {
            var user = await _context.Users
               .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException($"User id {id} not found.");
            }
        }
        public async Task ValidateUserLoginRequest(Login request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new ArgumentException("Not authenticated.");
            }
        }
        public Task ValidateUserESignature(IFormFile file)
        {
            var allowedTypes = new[]
            {
                 "image/jpeg", "image/png", "image/jpg", "application/pdf"
            };

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File required.");
            }
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
            {
                throw new ArgumentException("JPEG, PNG, or PDF files allowed only.");
            }
            return Task.CompletedTask;
        }
        public async Task ValidateSpecificId(int id)
        {
            if(!await _context.Users.AnyAsync(u => u.Id == id))
            {
                throw new ArgumentException($"User ID {id} not found.");
            }
            if(!await _context.Roles.AnyAsync(r => r.Id == id))
            {
                throw new ArgumentException($"Role ID {id} not found.");
            }
        }
        public static int ValidateUserClaim(ClaimsPrincipal user)
        {
            var useridClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (useridClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }
            if (!int.TryParse(useridClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID claim.");
            }
            return userId;
        }
    }
}
