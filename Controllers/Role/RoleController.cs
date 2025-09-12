using AutoMapper;
using csms_backend.Models;
using csms_backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Controllers
{
    public class RoleController : BaseController
    {
        private readonly RoleService _roleService;
        public RoleController(Context context, RoleService roleService) : base(context)
        {
            _roleService = roleService;
        }

        [HttpPost("role/create")]
        public async Task<ActionResult<RoleResponse>> CreateRole(
            [FromBody] RoleRequest request)
        {
            try
            {
                var response = await _roleService.CreateRole(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("role/toggle-status")]
        public async Task<ActionResult<RoleResponse>> ToggleStatus(
            [FromQuery] int id)
        {
            try
            {
                var response = await _roleService.ToggleStatus(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("role/delete/{id}")]
        public async Task<ActionResult<RoleResponse>> DeleteRole(
            [FromQuery] int id)
        {
            try
            {
                var response = await _roleService.DeleteRole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("roles/paginated")]
        public async Task<ActionResult<Pagination<RoleResponse>>> PaginatedRoles(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _roleService.PaginatedRoles(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("roles/list")]
        public async Task<ActionResult<List<RoleResponse>>> ListedRoles(
            [FromQuery] string? searchTerm)
        {
            try
            {
                var response = await _roleService.ListedRoles(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("roles/active")]
        public async Task<ActionResult<List<RoleResponse>>> ActiveListedRoles(
            [FromQuery] string? searchTerm)
        {
            try
            {
                var response = await _roleService.ActiveListedRoles(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("role/{id}")]
        public async Task<ActionResult<RoleResponse>> GetRoleById(
            [FromQuery] int id)
        {
            try
            {
                var response = await _roleService.GetRoleById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
