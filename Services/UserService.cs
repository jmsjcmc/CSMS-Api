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
    public class UserService : BaseService , IUserService
    {
        private readonly UserValidator _userValidator;
        private readonly AuthUserHelper _authHelper;
        private readonly UserQueries _userQueries;
        public UserService(AppDbContext context, IMapper mapper, UserValidator userValidator, AuthUserHelper authHelper, UserQueries userQueries) : base (context, mapper)
        {
            _userValidator = userValidator;
            _authHelper = authHelper;
            _userQueries = userQueries;
        }
        // [HttpGet("users")]
        public async Task<Pagination<UserResponse>> allusers(
            int pageNumber = 1,
            int pageSize = 1,
            string? searchTerm = null)
        {
            var query = _userQueries.usersquery(searchTerm);
            return await PaginationHelper.paginateandmap<User, UserResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("roles")]
        public async Task<List<RoleResponse>> allroles()
        {
            var roles = await _userQueries.rolesquery();

            return _mapper.Map<List<RoleResponse>>(roles);
        }
        // [HttpGet("lessors")]
        public async Task<List<UserResponse>> alllessors()
        {
            var lessors = await _userQueries.lessorsquery();

            return _mapper.Map<List<UserResponse>>(lessors);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserResponse> getuser(int id)
        {
            var user = await getuserdata(id);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpGet("role/{id}")]
        public async Task<RoleResponse> getrole(int id)
        {
            var role = await getroledata(id);

            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpGet("user-detail")]
        public async  Task<UserResponse> getuserdetail(ClaimsPrincipal detail)
        {
            int userId = UserValidator.ValidateUserClaim(detail);
            var user = await _userQueries.getmethoduserid(userId);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("login")]
        public async Task<object> login(Login request)
        {
            await _userValidator.ValidateUserLoginRequest(request);

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == request.Username);

            var accessToken = _authHelper.GenerateAccessToken(user);

            await _context.SaveChangesAsync();

            return new
            {
                AccessToken = accessToken,
                User = new
                {
                    user.Id,
                    user.Username,
                    user.Role
                }
            };
        }
        // [HttpPost("user")]
        public async Task<UserResponse> createuser(UserRequest request)
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

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("role")]
        public async Task<RoleResponse> addrole(RoleRequest request)
        {
            var role = _mapper.Map<Role>(request);
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpPatch("role/update/{id}")]
        public async Task<RoleResponse> updaterole(RoleRequest request, int id)
        {
            var role = await getroleid(id);

            _mapper.Map(request, role);
            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserResponse> updateuser(UserRequest request, int id)
        {
            await _userValidator.ValidateUserUpdateRequest(request, id);
            var roleNames = await GetValidRoleNames(request.Roles);

            var user = await getuserid(id);

            _mapper.Map(request, user);
            user.Role = roleNames;
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpPatch("user/e-signature/{id}")]
        public async Task<UserEsignResponse> addesign(IFormFile file, int id, ClaimsPrincipal requestor)
        {
            await _userValidator.ValidateUserESignature(file);
            var user = await _context.Users.FindAsync(id);

            string fullName = AuthUserHelper.GetFullName(requestor);
            string savedPath = await FileHelper.SaveEsignAsync(file, fullName);

            user.Esignature = savedPath;
            user.Updatedon = TimeHelper.GetPhilippineStandardTime();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserEsignResponse>(user);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task<UserResponse> hideuser(int id)
        {
            var user = await getuserid(id);

            user.Removed = !user.Removed;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpPatch("role/hide/{id}")]
        public async Task<RoleResponse> hiderole(int id)
        {
            var role = await getroleid(id);

            role.Removed = !role.Removed;

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task<UserResponse> deleteuser(int id)
        {
            var user = await getuserid(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpDelete("role/delete/{id}")]
        public async Task<RoleResponse> deleterole(int id)
        {
            var role = await getroleid(id);

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
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
        private async Task<User?> getuserid(int id)
        {
            return await _userQueries.patchmethoduserid(id);
        }

        private async Task<Role?> getroleid(int id)
        {
            return await _userQueries.patchmethodroleid(id);
        }

        private async Task<User?> getuserdata(int id)
        {
             return await _userQueries.getmethoduserid(id);
        }
        private async Task<Role?> getroledata(int id)
        {
            return await _userQueries.getmethodroleid(id);
        }
        private async Task<UserResponse> userResponse(int id)
        {
            var response = await getuserdata(id);
            return _mapper.Map<UserResponse>(response);
        }
        private async Task<RoleResponse> roleResponse(int id)
        {
            var response = await getroledata(id);
            return _mapper.Map<RoleResponse>(response);
        }
    }
}
