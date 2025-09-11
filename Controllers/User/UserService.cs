using AutoMapper;
using AutoMapper.QueryableExtensions;
using csms_backend.Models;
using csms_backend.Models.Entities;
using csms_backend.Utils;
using csms_backend.Utils.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace csms_backend.Controllers
{
    public class UserService : BaseService, UserInterface
    {
        private readonly UserQuery _userQuery;
        private readonly AuthenticatedUserHelper _authHelper;
        public UserService(Context context, IMapper mapper, UserQuery userQuery, AuthenticatedUserHelper authHelper) : base(context, mapper)
        {
            _userQuery = userQuery;
            _authHelper = authHelper;
        }
        // [HttpGet("users/paginated")]
        public async Task<Pagination<UserResponse>> PaginatedUsers(
            int pageNumber,
            int pageSize,
            string? searchTerm)
        {
            var query = _userQuery.PaginatedUsers(searchTerm);

            return await PaginationHelper.PaginateAndMap<User, UserResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("users/list")]
        public async Task<List<UserResponse>> ListedUsers(string? searchTerm)
        {
            var users = await _userQuery.ListedUsers(searchTerm);
            return _mapper.Map<List<UserResponse>>(users);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserResponse> GetUserById(int id)
        {
            var user = await _userQuery.GetUserById(id);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpGet("user/user-detail")]
        public async Task<UserResponse> AuthUserDetail(ClaimsPrincipal detail)
        {
            int userId = UserValidators.ValidateUserClaim(detail);
            var user = await _userQuery.GetUserById(userId);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("user/create")]
        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            var buExist = await _context.BusinessUnit.AnyAsync(b => b.Id == request.BusinessUnitId);

            if (!buExist)
                throw new Exception("Invalid Business unit ID.");

            var roleExist = await _context.Role
                .Where(r => request.RoleId.Contains(r.Id))
                .Select(r => r.Id)
                .ToListAsync();

            if (roleExist.Count != request.RoleId.Count)
                throw new Exception("One or more Role IDs are invalid.");

            if (await _context.User.AnyAsync(u => u.Username == request.Username))
                throw new Exception($"Username {request.Username} already used.");

            var user = _mapper.Map<User>(request);
            user.CreatedAt = DateTimeHelper.GetPhilippineTime();
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Status = Status.Active;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            var savedUser = await _context.User
                .Where(u => u.Id == user.Id)
                .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (savedUser == null)
            {
                throw new Exception("No user response");
            }
            else
            {
                return savedUser;
            }
        }
        // [HttpPost("user/log-in")]
        public async Task<UserLoginResponse> UserLogin(UserLoginRequest request)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                throw new NullReferenceException($"Username {request.Username} not found.");
            }
            else
            {
                var accessToken = _authHelper.GenerateAccessToken(user);
                await _context.SaveChangesAsync();

                return new UserLoginResponse
                {
                    AccessToken = accessToken
                };
            }
        }
    }
}
