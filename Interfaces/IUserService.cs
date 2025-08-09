using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserResponse>> AllUsers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<RoleResponse>> AllRoles();
        Task<List<UserResponse>> AllLessors();
        Task<UserResponse> GetUser(int id);
        Task<RoleResponse> GetRole(int id);
        Task<UserResponse> GetUserDetail(ClaimsPrincipal detail);
        Task<int> TotalCount();
        Task<object> Login(Login request);
        Task<UserResponse> CreateUser(UserRequest request);
        Task<RoleResponse> AddRole(RoleRequest request);
        Task<RoleResponse> UpdateRole(RoleRequest request, int id);
        Task<UserResponse> UpdateUser(UserRequest request, int id);
        Task <UserEsignResponse> AddEsign(IFormFile file, int id, ClaimsPrincipal requestor);
        Task<UserResponse> ToggleActive(int id);
        Task<UserResponse> HideUser(int id);
        Task<RoleResponse> HideRole(int id);
        Task<UserResponse> DeleteUser(int id);
        Task<RoleResponse> DeleteRole(int id);
    }
}
