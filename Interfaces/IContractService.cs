using CSMapi.Models;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IContractService
    {
        Task<Pagination<ContractResponse>> AllContracts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<ContractResponse> GetContract(int id);
        Task<ContractResponse> AddContract(ContractRequest request, ClaimsPrincipal user);
        Task<ContractResponse> UpdateContract(ContractRequest request, int id, ClaimsPrincipal user);
        Task<ContractResponse> ToggleActive(int id);
        Task<ContractResponse> HideContract(int id);
        Task<ContractResponse> DeleteContract(int id);
    }
}
