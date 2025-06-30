using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserResponse>> allusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<RoleResponse>> allroles();
        Task<List<UserResponse>> alllessors();
        Task<UserResponse> getuser(int id);
        Task<RoleResponse> getrole(int id);
        Task<UserResponse> getuserdetail(ClaimsPrincipal detail);
        Task<object> login(Login request);
        Task<UserResponse> createuser(UserRequest request);
        Task<RoleResponse> addrole(RoleRequest request);
        Task<RoleResponse> updaterole(RoleRequest request, int id);
        Task<UserResponse> updateuser(UserRequest request, int id);
        Task <UserEsignResponse> addesign(IFormFile file, int id, ClaimsPrincipal requestor);
        Task<UserResponse> hideuser(int id);
        Task<RoleResponse> hiderole(int id);
        Task<UserResponse> deleteuser(int id);
        Task<RoleResponse> deleterole(int id);
    }
}
