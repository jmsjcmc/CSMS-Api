using csms_backend.Models;

namespace csms_backend.Controllers
{
    public interface CompanyInterface
    {
        Task<Pagination<CompanyResponse>> PaginatedCompanies(
            int pageNumber,
            int pageSize,
            string? searchTerm
        );
        Task<List<CompanyResponse>> ListedCompanies(string? searchTerm);
        Task<CompanyResponse> GetCompanyById(int id);
        Task<CompanyResponse> CreateCompany(CompanyRequest request);
        Task<CompanyResponse> ToggleStatus(int id);
        Task<CompanyResponse> DeleteCompany(int id);
    }

    public interface RepresentativeInterface
    {
        Task<List<RepresentativeResponse>> ListedRepresentatives(string? searchTerm);
    }
}
