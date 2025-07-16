using CSMapi.Models;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IContractService
    {
        Task<Pagination<ContractResponse>> allcontracts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<ContractResponse> getcontract(int id);
        Task<ContractResponse> addcontract(ContractRequest request, ClaimsPrincipal user);
        Task<ContractResponse> updatecontract(ContractRequest request, int id, ClaimsPrincipal user);
        Task<ContractResponse> toggleactive(int id);
        Task<ContractResponse> hidecontract(int id);
        Task<ContractResponse> deletecontract(int id);
    }
}
