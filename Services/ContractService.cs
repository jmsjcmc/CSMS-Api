using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSMapi.Services
{
    public class ContractService : IContractService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ContractValidator _contractValidator;
        private readonly ContractQueries _contractQueries;
        public ContractService(AppDbContext context, IMapper mapper, ContractValidator contractValidator, ContractQueries contractQueries) 
        {
            _context = context;
            _mapper = mapper;
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
            var totalCount = await query.CountAsync();

            var contracts = await PaginationHelper.paginateandproject<Contract, ContractResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(contracts, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("contract/{id}")]
        public async Task<ContractResponse> getcontract(int id)
        {
            await _contractValidator.ValidateFetchContract(id);

            var contract = _contractQueries.getmethodcontractid(id);

            return _mapper.Map<ContractResponse>(contract);
        }
        // [HttpPost("contract")]
        public async Task<ContractResponse> addcontract(ContractRequest request, ClaimsPrincipal user)
        {
            await _contractValidator.ValidateContractRequest(request);

            var contract = _mapper.Map<Contract>(request);
            contract.Creatorid = AuthUserHelper.GetUserId(user);
            contract.Createdon = TimeHelper.GetPhilippineStandardTime();

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            var savedContract = await _contractQueries.getmethodcontractid(contract.Id);

            return _mapper.Map<ContractResponse>(savedContract);
        }
        // [HttpPatch("contract/update/{id}")]
        public async Task<ContractResponse> updatecontract(ContractRequest request, int id, ClaimsPrincipal user)
        {
            var contract = await getcontractid(id);

            _mapper.Map(request, contract);
            contract.Creatorid = AuthUserHelper.GetUserId(user);
            contract.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            var updatedContract = await _contractQueries.getmethodcontractid(contract.Id);

            return _mapper.Map<ContractResponse>(updatedContract);
        }
        // [HttpPatch("contract/hide/{id}")]
        public async Task hidecontract(int id)
        {
            var contract = await getcontractid(id);

            contract.Removed = true;

            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("contract/delete/{id}")]
        public async Task deletecontract(int id)
        {
            var contract = await getcontractid(id);

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }
        // Helper
        private async Task<Contract?> getcontractid(int id)
        {
            return await _contractQueries.patchmethodcontractid(id);
        }
    }
}
