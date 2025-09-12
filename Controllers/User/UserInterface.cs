using csms_backend.Models;
using System.Security.Claims;

namespace csms_backend.Controllers
{
    public interface UserInterface
    {
        Task<Pagination<UserResponse>> PaginatedUsers(
            int pageNumber,
            int pageSize,
            string? searchTerm);
        Task<List<UserResponse>> ListedUsers(string? searchTerm);
        Task<UserResponse> GetUserById(int id);
        Task<UserResponse> AuthUserDetail(ClaimsPrincipal detail);
        Task<UserResponse> CreateUser(UserRequest request);
        Task<UserLoginResponse> UserLogin(UserLoginRequest request);
        Task<UserResponse> ToggleStatus(int id);
        Task<UserResponse> UpdateUser(UserRequest request, int id);
        Task<UserResponse> DeleteUser(int id);
    }
}
