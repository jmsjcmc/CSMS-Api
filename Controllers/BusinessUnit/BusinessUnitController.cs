using csms_backend.Models;
using csms_backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Controllers
{
    public class BusinessUnitController : BaseController
    {
        private readonly BusinessUnitService _buService;
        public BusinessUnitController(Context context, BusinessUnitService buService) : base(context)
        {
            _buService = buService;
        }

        [HttpPost("business-unit/create")]
        public async Task<ActionResult<BusinessUnitResponse>> CreateBU(BusinessUnitRequest request)
        {
            try
            {
                var response = await _buService.CreateBU(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("business-unit/toggle-status")]
        public async Task<ActionResult<BusinessUnitResponse>> ToggleStatus(int id)
        {
            try
            {
                var response = await _buService.ToggleStatus(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("business-unit/delete/{id}")]
        public async Task<ActionResult<BusinessUnitResponse>> DeleteBU(int id)
        {
            try
            {
                var response = await _buService.DeleteBU(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("business-units/paginated")]
        public async Task<ActionResult<Pagination<BusinessUnitResponse>>> PaginatedBUs(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _buService.PaginatedBUs(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("business-units/list")]
        public async Task<ActionResult<List<BusinessUnitResponse>>> ListedBUs(string? searchTerm)
        {
            try
            {
                var response = await _buService.ListedBUs(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("business-unit/{id}")]
        public async Task<ActionResult<BusinessUnitResponse>> GetBUById(int id)
        {
            try
            {
                var response = await _buService.GetBUById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
