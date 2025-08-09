using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSMapi.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserValidator _userValidator;
        private readonly AuthUserHelper _authHelper;
        private readonly UserQueries _userQueries;
        public UserService(AppDbContext context, IMapper mapper, UserValidator userValidator, AuthUserHelper authHelper, UserQueries userQueries) : base(context, mapper)
        {
            _userValidator = userValidator;
            _authHelper = authHelper;
            _userQueries = userQueries;
        }
        // [HttpGet("users")]
        public async Task<Pagination<UserResponse>> AllUsers(
            int pageNumber = 1,
            int pageSize = 1,
            string? searchTerm = null)
        {
            var query = _userQueries.UsersQuery(searchTerm);
            return await PaginationHelper.PaginateAndMap<User, UserResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("roles")]
        public async Task<List<RoleResponse>> AllRoles()
        {
            var roles = await _userQueries.RolesQuery();

            return _mapper.Map<List<RoleResponse>>(roles);
        }
        // [HttpGet("lessors")]
        public async Task<List<UserResponse>> AllLessors()
        {
            var lessors = await _userQueries.LessorsQuery();

            return _mapper.Map<List<UserResponse>>(lessors);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserResponse> GetUser(int id)
        {
            var user = await GetUserId(id);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpGet("role/{id}")]
        public async Task<RoleResponse> GetRole(int id)
        {
            var role = await GetRoleId(id);

            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpGet("user-detail")]
        public async Task<UserResponse> GetUserDetail(ClaimsPrincipal detail)
        {
            int userId = UserValidator.ValidateUserClaim(detail);
            var user = await _userQueries.GetUserId(userId);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpGet("users/count-all")]
        public async Task<int> TotalCount()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => !u.Removed)
                .CountAsync();
        }
        // [HttpPost("login")]
        public async Task<object> Login(Login request)
        {
            await _userValidator.ValidateUserLoginRequest(request);

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == request.Username) ??
                throw new ArgumentException("User not found.");

            var accessToken = _authHelper.GenerateAccessToken(user);

            await _context.SaveChangesAsync();

            return new
            {
                AccessToken = accessToken,
                User = new
                {
                    user.Role
                }
            };
        }
        // [HttpPost("user")]
        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            await _userValidator.ValidateUserRequest(request);

            var requestRoles = request.Roles
                .Select(r => r.Trim())
                .ToList();

            var existingRoles = await _context.Roles
                .Where(r => requestRoles.Contains(r.Rolename))
                .ToListAsync();

            var roleNames = string.Join(", ", existingRoles.Select(r => r.Rolename).OrderBy(r => r));

            var user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Role = roleNames;
            user.Createdon = TimeHelper.GetPhilippineStandardTime();
            user.Active = true;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("role")]
        public async Task<RoleResponse> AddRole(RoleRequest request)
        {
            var role = _mapper.Map<Role>(request);
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpPatch("role/update/{id}")]
        public async Task<RoleResponse> UpdateRole(RoleRequest request, int id)
        {
            var role = await PatchRoleId(id) ??
                throw new ArgumentException("Role not found.");

            _mapper.Map(request, role);
            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserResponse> UpdateUser(UserRequest request, int id)
        {
            await _userValidator.ValidateUserUpdateRequest(request, id);
            var roleNames = await GetValidRoleNames(request.Roles);

            var user = await PatchUserId(id) ??
                throw new ArgumentException("User not found.");

            _mapper.Map(request, user);
            user.Role = roleNames;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpPatch("user/e-signature/{id}")]
        public async Task<UserEsignResponse> AddEsign(IFormFile file, int id, ClaimsPrincipal requestor)
        {
            await _userValidator.ValidateUserESignature(file);
            var user = await _context.Users.FindAsync(id) ??
                throw new ArgumentException("User not found.");

            string fullName = AuthUserHelper.GetFullName(requestor);
            string savedPath = await FileHelper.SaveEsignAsync(file, fullName);

            user.Esignature = savedPath;
            user.Updatedon = TimeHelper.GetPhilippineStandardTime();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserEsignResponse>(user);
        }
        // [HttpPatch("user/toggle-active")]
        public async Task<UserResponse> ToggleActive(int id)
        {
            var user = await PatchUserId(id);

            user.Active = !user.Active;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task<UserResponse> HideUser(int id)
        {
            var user = await PatchUserId(id) ??
                throw new ArgumentException("User not found.");

            user.Removed = !user.Removed;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpPatch("role/hide/{id}")]
        public async Task<RoleResponse> HideRole(int id)
        {
            var role = await PatchRoleId(id) ??
                throw new ArgumentException("Role not found.");

            role.Removed = !role.Removed;

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task<UserResponse> DeleteUser(int id)
        {
            var user = await PatchUserId(id) ??
                throw new ArgumentException("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpDelete("role/delete/{id}")]
        public async Task<RoleResponse> DeleteRole(int id)
        {
            var role = await PatchRoleId(id) ??
                throw new ArgumentException("Role not found");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // Helpers
        private async Task<string> GetValidRoleNames(List<string> roles)
        {
            var requestRoles = roles
                .Select(r => r.Trim())
                .ToList();

            var existingRoles = await _context.Roles
                .Where(r => requestRoles.Contains(r.Rolename))
                .ToListAsync();

            var roleNames = string.Join(", ", existingRoles
                .Select(r => r.Rolename)
                .OrderBy(r => r));

            return roleNames;
        }
        private async Task<User?> PatchUserId(int id)
        {
            return await _userQueries.PatchUserId(id);
        }

        private async Task<Role?> PatchRoleId(int id)
        {
            return await _userQueries.PatchRoleId(id);
        }

        private async Task<User?> GetUserId(int id)
        {
            return await _userQueries.GetUserId(id);
        }
        private async Task<Role?> GetRoleId(int id)
        {
            return await _userQueries.GetRoleId(id);
        }
        private async Task<UserResponse> UserResponse(int id)
        {
            var response = await GetUserId(id);
            return _mapper.Map<UserResponse>(response);
        }
        private async Task<RoleResponse> RoleResponse(int id)
        {
            var response = await GetRoleId(id);
            return _mapper.Map<RoleResponse>(response);
        }
    }
}
