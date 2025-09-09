using AutoMapper;
using csms_backend.Models;
using csms_backend.Models.Entities;
using csms_backend.Utils;

namespace csms_backend.Controllers
{
    public class RoleService : BaseService, RoleInterface
    {
        private readonly RoleQuery _roleQuery;
        public RoleService(Context context, IMapper mapper, RoleQuery roleQuery) : base(context, mapper)
        {
            _roleQuery = roleQuery;
        }
        // [HttpGet("roles/paginated")]
        public async Task<Pagination<RoleResponse>> PaginatedRoles(
            int pageNumber,
            int pageSize,
            string? searchTerm)
        {
            var query = _roleQuery.PaginatedRoles(searchTerm);
            return await PaginationHelper.PaginateAndMap<Role, RoleResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("roles/list")]
        public async Task<List<RoleResponse>> ListedRoles(string? searchTerm)
        {
            var roles = await _roleQuery.ListedRoles(searchTerm);
            return _mapper.Map<List<RoleResponse>>(roles);
        }
        // [HttpGet("roles/active")]
        public async Task<List<RoleResponse>> ActiveListedRoles(string? searchTerm)
        {
            var roles = await _roleQuery.ActiveListedRoles(searchTerm);
            return _mapper.Map<List<RoleResponse>>(roles);
        }
        // [HttpGet("role/{id}")]
        public async Task<RoleResponse> GetRoleById(int id)
        {
            var role = await _roleQuery.GetRoleById(id);
            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpPost("role/create")]
        public async Task<RoleResponse> CreateRole(RoleRequest request)
        {
            var role = _mapper.Map<Role>(request);
            role.Status = Status.Active;
            role.DateCreated = DateTimeHelper.GetPhilippineTime();

            await _context.Role.AddAsync(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpPut("role/toggle-status")]
        public async Task<RoleResponse> ToggleStatus(int id)
        {
            var role = await _roleQuery.PatchRoleById(id);

            if (role == null)
            {
                throw new Exception($"Role with ID {id} not found.");
            }
            else
            {
                role.Status = role.Status == Status.Active
                    ? Status.Inactive
                    : Status.Active;
                role.DateUpdated = DateTimeHelper.GetPhilippineTime();

                await _context.SaveChangesAsync();

                return _mapper.Map<RoleResponse>(role);
            }
        }
        // [HttpDelete("role/delete/{id}")]
        public async Task<RoleResponse> DeleteRole(int id)
        {
            var role = await _roleQuery.PatchRoleById(id);
            if (role == null)
            {
                throw new Exception($"Role with ID {id} not found.");
            }
            else
            {
                _context.Role.Remove(role);
                await _context.SaveChangesAsync();

                return _mapper.Map<RoleResponse>(role);
            }
        }
    }
}
