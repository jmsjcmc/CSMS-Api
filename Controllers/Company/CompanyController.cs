using csms_backend.Models;
using csms_backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly CompanyService _companyService;
        public CompanyController(Context context, CompanyService companyService) : base(context)
        {
            _companyService = companyService;
        }

        [HttpPost("company/create")]
        public async Task<ActionResult<CompanyResponse>> CreateCompany(CompanyRequest request)
        {
            try
            {
                var response = await _companyService.CreateCompany(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("company/delete/{id}")]
        public async Task<ActionResult<CompanyResponse>> DeleteCompany(int id)
        {
            try
            {
                var response = await _companyService.DeleteCompany(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("companies/paginated")]
        public async Task<ActionResult<Pagination<CompanyResponse>>> PaginatedCompanies(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _companyService.PaginatedCompanies(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("companies/list")]
        public async Task<ActionResult<List<CompanyResponse>>> ListedCompanies(string? searchTerm)
        {
            try
            {
                var response = await _companyService.ListedCompanies(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("company/{id}")]
        public async Task<ActionResult<CompanyResponse>> GetCompanyById(int id)
        {
            try
            {
                var response = await _companyService.GetCompanyById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
