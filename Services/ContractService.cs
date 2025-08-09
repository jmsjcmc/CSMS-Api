using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using System.Security.Claims;

namespace CSMapi.Services
{
    public class ContractService : BaseService , IContractService
    {
       
        private readonly ContractValidator _contractValidator;
        private readonly ContractQueries _contractQueries;
        public ContractService(AppDbContext context, IMapper mapper, ContractValidator contractValidator, ContractQueries contractQueries) : base (context, mapper)
        {
           
            _contractValidator = contractValidator;
            _contractQueries = contractQueries;
        }
        // [HttpGet("contracts")]
        public async Task<Pagination<ContractResponse>> AllContracts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _contractQueries.ContractsQuery(searchTerm);
            return await PaginationHelper.PaginateAndMap<Contract, ContractResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("contract/{id}")]
        public async Task<ContractResponse> GetContract(int id)
        {
            var contract = await GetContractData(id);

            return _mapper.Map<ContractResponse>(contract);
        }
        // [HttpPost("contract")]
        public async Task<ContractResponse> AddContract(ContractRequest request, ClaimsPrincipal user)
        {
            _contractValidator.ValidateContractRequest(request);

            var contract = _mapper.Map<Contract>(request);
            contract.Active = true;
            contract.Creatorid = AuthUserHelper.GetUserId(user);
            contract.Createdon = TimeHelper.GetPhilippineStandardTime();

            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();

            return await ContractResponse(contract.Id);
        }
        // [HttpPatch("contract/update/{id}")]
        public async Task<ContractResponse> UpdateContract(ContractRequest request, int id, ClaimsPrincipal user)
        {
            var contract = await GetContractId(id);

            _mapper.Map(request, contract);
            contract.Creatorid = AuthUserHelper.GetUserId(user);
            contract.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            return await ContractResponse(contract.Id);
        }
        // [HttpPatch("contract/toggle-active")]
        public async Task<ContractResponse> ToggleActive(int id)
        {
            var contract = await GetContractId(id);

            contract.Active = !contract.Active;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await ContractResponse(contract.Id);
        }
        // [HttpPatch("contract/hide/{id}")]
        public async Task<ContractResponse> HideContract(int id)
        {
            var contract = await GetContractId(id);

            contract.Removed = !contract.Removed;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await ContractResponse(contract.Id);
        }
        // [HttpDelete("contract/delete/{id}")]
        public async Task<ContractResponse> DeleteContract(int id)
        {
            var contract = await GetContractId(id);

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return await ContractResponse(contract.Id);
        }
        // Helpers
        private async Task<Contract?> GetContractId(int id)
        {
            return await _contractQueries.PatchContractId(id);
        }
        private async Task<Contract?> GetContractData(int id)
        {
            return await _contractQueries.GetContractId(id);
        }
        private async Task<ContractResponse> ContractResponse(int id)
        {
            var response = await GetContractData(id);
            return _mapper.Map<ContractResponse>(response);
        }
    }
}
