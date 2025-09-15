using csms_backend.Models;

namespace csms_backend.Controllers
{
    public interface BusinessUnitInterface
    {
        Task<Pagination<BusinessUnitResponse>> PaginatedBUs(
            int pageNumber,
            int pageSize,
            string? searchTerm
        );
        Task<List<BusinessUnitResponse>> ListedBUs(string? searchTerm);
        Task<BusinessUnitResponse> GetBUById(int id);
        Task<BusinessUnitResponse> CreateBU(BusinessUnitRequest request);
        Task<BusinessUnitResponse> ToggleStatus(int id);
        Task<BusinessUnitResponse> DeleteBU(int id);
    }
}
