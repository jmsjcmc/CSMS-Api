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
        public async Task<Pagination<ContractResponse>> allcontracts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _contractQueries.contractsquery(searchTerm);
            return await PaginationHelper.paginateandmap<Contract, ContractResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("contract/{id}")]
        public async Task<ContractResponse> getcontract(int id)
        {
            await _contractValidator.ValidateFetchContract(id);

            var contract = await getcontractdata(id);

            return _mapper.Map<ContractResponse>(contract);
        }
        // [HttpPost("contract")]
        public async Task<ContractResponse> addcontract(ContractRequest request, ClaimsPrincipal user)
        {
            await _contractValidator.ValidateContractRequest(request);

            var contract = _mapper.Map<Contract>(request);
            contract.Active = true;
            contract.Creatorid = AuthUserHelper.GetUserId(user);
            contract.Createdon = TimeHelper.GetPhilippineStandardTime();

            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();

            return await contractResponse(contract.Id);
        }
        // [HttpPatch("contract/update/{id}")]
        public async Task<ContractResponse> updatecontract(ContractRequest request, int id, ClaimsPrincipal user)
        {
            var contract = await getcontractid(id);

            _mapper.Map(request, contract);
            contract.Creatorid = AuthUserHelper.GetUserId(user);
            contract.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            return await contractResponse(contract.Id);
        }
        // [HttpPatch("contract/toggle-active")]
        public async Task<ContractResponse> toggleactive(int id)
        {
            var contract = await getcontractid(id);

            contract.Active = !contract.Active;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await contractResponse(contract.Id);
        }
        // [HttpPatch("contract/hide/{id}")]
        public async Task<ContractResponse> hidecontract(int id)
        {
            var contract = await getcontractid(id);

            contract.Removed = !contract.Removed;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();

            return await contractResponse(contract.Id);
        }
        // [HttpDelete("contract/delete/{id}")]
        public async Task<ContractResponse> deletecontract(int id)
        {
            var contract = await getcontractid(id);

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return await contractResponse(contract.Id);
        }
        // Helpers
        private async Task<Contract> getcontractid(int id)
        {
            return await _contractQueries.patchmethodcontractid(id) ?? 
                throw new ArgumentException($"Contract with id {id} not found.");
        }
        private async Task<Contract> getcontractdata(int id)
        {
            return await _contractQueries.getmethodcontractid(id) ??
                throw new ArgumentException($"Contract with id {id} not found.");
        }
        private async Task<ContractResponse> contractResponse(int id)
        {
            var response = await getcontractdata(id);
            return _mapper.Map<ContractResponse>(response);
        }
    }
}
