using csms_backend.Models;

namespace csms_backend.Controllers
{
    public interface RoleInterface
    {
        Task<Pagination<RoleResponse>> PaginatedRoles(
            int pageNumber,
            int pageSize,
            string? searchTerm);
        Task<List<RoleResponse>> ListedRoles(string? searchTerm);
        Task<List<RoleResponse>> ActiveListedRoles(string? searchTerm);
        Task<RoleResponse> GetRoleById(int id);
        Task<RoleResponse> CreateRole(RoleRequest request);
        Task<RoleResponse> ToggleStatus(int id);
        Task<RoleResponse> DeleteRole(int id);
    }
}
