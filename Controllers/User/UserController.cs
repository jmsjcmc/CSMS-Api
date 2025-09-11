using csms_backend.Models;
using csms_backend.Utils;
using Microsoft.AspNetCore.Mvc;


namespace csms_backend.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(Context context, UserService userService) : base(context)
        {
            _userService = userService;
        }

        [HttpPost("user/create")]
        public async Task<ActionResult<UserResponse>> CreateUser(UserRequest request)
        {
            try
            {
                var response = await _userService.CreateUser(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost("user/log-in")]
        public async Task<ActionResult<UserLoginResponse>> UserLogin(UserLoginRequest request)
        {
            try
            {
                var response = await _userService.UserLogin(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("users/paginated")]
        public async Task<ActionResult<Pagination<UserResponse>>> PaginatedUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _userService.PaginatedUsers(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("users/list")]
        public async Task<ActionResult<List<UserResponse>>> ListedUsers(string? searchTerm)
        {
            try
            {
                var response = await _userService.ListedUsers(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("user/user-detail")]
        public async Task<ActionResult<UserResponse>> AuthUserDetail()
        {
            try
            {
                var response = await _userService.AuthUserDetail(User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
