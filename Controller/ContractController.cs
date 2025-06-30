using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Controller
{
    public class ContractController : BaseApiController
    {
        private readonly ContractService _contractService;
        public ContractController(AppDbContext context, IMapper mapper, ContractService contractService) : base(context, mapper)
        {
            _contractService = contractService;
        }
        // Fetch all contracts
        [HttpGet("contracts")]
        public async Task<ActionResult<Pagination<ContractResponse>>> allcontracts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _contractService.allcontracts(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific contract
        [HttpGet("contract/{id}")]
        public async Task<ActionResult<ContractResponse>> getcontract(int id)
        {
            try
            {
                var response = await _contractService.getcontract(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Crete contract
        [HttpPost("contract")]
        public async Task<ActionResult<ContractResponse>> addcontract([FromBody] ContractRequest request)
        {
            try
            {
                var response = await _contractService.addcontract(request, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific contract
        [HttpPatch("contract/update/{id}")]
        public async Task<ActionResult<ContractResponse>> updatecontract([FromBody] ContractRequest request, int id)
        {
            try
            {
                var response = await _contractService.updatecontract(request, id, User);
                return response;

            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific contract without removing in Database (Soft Delete)
        [HttpPatch("contract/hide/{id}")]
        public async Task<ActionResult<ContractResponse>> hidecontract(int id)
        {
            try
            {
                var response = await _contractService.hidecontract(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific contract in Database
        [HttpDelete("contract/delete/{id}")]
        public async Task<ActionResult<ContractResponse>> deletecontract(int id)
        {
            try
            {
                var response = await _contractService.deletecontract(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
