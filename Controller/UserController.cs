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
        // Fetch all users 
        [HttpGet("users")]
        public async Task<ActionResult<Pagination<UserResponse>>> allusers(
            [FromQuery] int pageNumber = 1, 
            [FromQuery] int pageSize = 10, 
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _userService.allusers(pageNumber, pageSize, searchTerm);
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all roles
        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleResponse>>> allroles()
        {
            try
            {
                var response = await _userService.allroles();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all users belongs Business Unit "SubZero Ice and Cold Storage Inc"
        [HttpGet("lessors")]
        public async Task<ActionResult<List<UserResponse>>> alllessors()
        {
            try
            {
                var response = await _userService.alllessors();
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific user
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponse>> getUser(int id)
        {
            try
            {
                var response = await _userService.getuser(id);
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific role
        [HttpGet("role/{id}")]
        public async Task<ActionResult<RoleResponse>> getrole(int id)
        {
            try
            {
                var response = await _userService.getrole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch details for authenticated user
        [HttpGet("user-detail")]
        public async Task<ActionResult<UserResponse>> getuserdetail()
        {
            try
            {
               
                var response = await _userService.getuserdetail(User);
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // User Login
        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] Login request)
        {
            try
            {
                var response = await _userService.login(request);
                return Ok(response);
            }
            catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Create user
        [HttpPost("user")]
        public async Task<ActionResult<UserResponse>> createUser([FromBody] UserRequest request)
        {
            try
            {
                var response = await _userService.createuser(request);
                return response;
            }catch(Exception e)
            {
                return HandleException(e);
            }
        }
        // Create role
        [HttpPost("role")]
        public async Task<ActionResult<RoleResponse>> addrole([FromBody] RoleRequest request)
        {
            try
            {
                var response = await _userService.addrole(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update role
        [HttpPatch("role/update/{id}")]
        public async Task<ActionResult<RoleResponse>> updaterole([FromBody] RoleRequest request, int id)
        {
            try
            {
                var response = await _userService.updaterole(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update user
        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<UserResponse>> updateuser([FromBody] UserRequest request, int id)
        {
            try
            {
                var response = await _userService.updateuser(request, id);
                return response;
                
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Add E signature to specific user
        [HttpPatch("user/e-signature/{id}")]
        public async Task<ActionResult<UserEsignResponse>> addesign(IFormFile file, int id)
        {
            try
            {
                var response = await _userService.addesign(file, id, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific user without removing in Database (Soft Delete)
        [HttpPatch("user/hide/{id}")]
        public async Task<ActionResult> hideuser(int id)
        {
            try
            {
                await _userService.hideuser(id);
                return Ok("User removed.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific role without removing in Database (Soft Delete)
        [HttpPatch("role/hide/{id}")]
        public async Task<ActionResult> hiderole(int id)
        {
            try
            {
                await _userService.hiderole(id);
                return Ok("Role removed.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific user in Database
        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult> deleteuser(int id)
        {
            try
            {
                await _userService.deleteuser(id);
                return Ok("User removed permanently.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific role in Database
        [HttpDelete("role/delete/{id}")]
        public async Task<ActionResult> deleterole(int id)
        {
            try
            {
                await _userService.deleterole(id);
                return Ok("Role removed permanently.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
