using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CSMapi.Controller
{
    public class UserController : BaseApiController
    {
        private readonly UserService _userService;
        
        public UserController(AppDbContext context, IMapper mapper, UserService userService) : base (context, mapper)
        {
            _userService = userService;
        }
        // Count all users
        [HttpGet("users/count-all")]
        public async Task<ActionResult<UsersCount>> CountAll()
        {
            try
            {
                var count = new UsersCount
                {
                    Total = await _userService.TotalCount()
                };

                return count;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all users 
        [HttpGet("users")]
        public async Task<ActionResult<Pagination<UserResponse>>> AllUsers(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _userService.AllUsers(pageNumber, pageSize, searchTerm);
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all roles
        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleResponse>>> AllRoles()
        {
            try
            {
                var response = await _userService.AllRoles();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all users belongs Business Unit "SubZero Ice and Cold Storage Inc"
        [HttpGet("lessors")]
        public async Task<ActionResult<List<UserResponse>>> AllLessors()
        {
            try
            {
                var response = await _userService.AllLessors();
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific user
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            try
            {
                var response = await _userService.GetUser(id);
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific role
        [HttpGet("role/{id}")]
        public async Task<ActionResult<RoleResponse>> GetRole(int id)
        {
            try
            {
                var response = await _userService.GetRole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch details for authenticated user
        [HttpGet("user-detail")]
        public async Task<ActionResult<UserResponse>> GetUserDetail()
        {
            try
            {
               
                var response = await _userService.GetUserDetail(User);
                return response;
            }catch (Exception e)
            {
                return HandleLoginException(e);
            }
        }
        // User Login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login request)
        {
            try
            {
                var response = await _userService.Login(request);
                return Ok(response);
            }
            catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Create user
        [HttpPost("user")]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                var response = await _userService.CreateUser(request);
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Create role
        [HttpPost("role")]
        public async Task<ActionResult<RoleResponse>> AddRole([FromBody] RoleRequest request)
        {
            try
            {
                var response = await _userService.AddRole(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update role
        [HttpPatch("role/update/{id}")]
        public async Task<ActionResult<RoleResponse>> UpdateRole([FromBody] RoleRequest request, int id)
        {
            try
            {
                var response = await _userService.UpdateRole(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update user
        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UserRequest request, int id)
        {
            try
            {
                var response = await _userService.UpdateUser(request, id);
                return response;
                
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Add E signature to specific user
        [HttpPatch("user/e-signature/{id}")]
        public async Task<ActionResult<UserEsignResponse>> AddESign(IFormFile file, int id)
        {
            try
            {
                var response = await _userService.AddEsign(file, id, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific user active status
        [HttpPatch("user/toggle-active")]
        public async Task<ActionResult<UserResponse>> ToggleActive(int id)
        {
            try
            {
                var response = await _userService.ToggleActive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific user without removing in Database (Soft Delete)
        [HttpPatch("user/hide/{id}")]
        public async Task<ActionResult<UserResponse>> HideUser(int id)
        {
            try
            {
                var response = await _userService.HideUser(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific role without removing in Database (Soft Delete)
        [HttpPatch("role/hide/{id}")]
        public async Task<ActionResult<RoleResponse>> HideRole(int id)
        {
            try
            {
                var response = await _userService.HideRole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific user in Database
        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult<UserResponse>> DeleteUser(int id)
        {
            try
            {
                var response = await _userService.DeleteUser(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific role in Database
        [HttpDelete("role/delete/{id}")]
        public async Task<ActionResult<RoleResponse>> DeleteRole(int id)
        {
            try
            {
                var response = await _userService.DeleteRole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
