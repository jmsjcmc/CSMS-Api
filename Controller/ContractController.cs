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
        public async Task<ActionResult<Pagination<ContractResponse>>> AllContracts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _contractService.AllContracts(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific contract
        [HttpGet("contract/{id}")]
        public async Task<ActionResult<ContractResponse>> GetContract(int id)
        {
            try
            {
                var response = await _contractService.GetContract(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Crete contract
        [HttpPost("contract")]
        public async Task<ActionResult<ContractResponse>> AddContract([FromBody] ContractRequest request)
        {
            try
            {
                var response = await _contractService.AddContract(request, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific contract
        [HttpPatch("contract/update/{id}")]
        public async Task<ActionResult<ContractResponse>> UpdateContract([FromBody] ContractRequest request, int id)
        {
            try
            {
                var response = await _contractService.UpdateContract(request, id, User);
                return response;

            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific contract active status
        [HttpPatch("contract/toggle-active")]
        public async Task<ActionResult<ContractResponse>> ToggleActive(int id)
        {
            try
            {
                var response = await _contractService.ToggleActive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific contract without removing in Database (Soft Delete)
        [HttpPatch("contract/hide/{id}")]
        public async Task<ActionResult<ContractResponse>> HideContract(int id)
        {
            try
            {
                var response = await _contractService.HideContract(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific contract in Database
        [HttpDelete("contract/delete/{id}")]
        public async Task<ActionResult<ContractResponse>> DeleteContract(int id)
        {
            try
            {
                var response = await _contractService.DeleteContract(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
