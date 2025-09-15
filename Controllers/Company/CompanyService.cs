using AutoMapper;
using csms_backend.Models;
using csms_backend.Utils;

namespace csms_backend.Controllers
{
    public class CompanyService : BaseService, CompanyInterface
    {
        private readonly CompanyQuery _companyQuery;

        public CompanyService(Context context, IMapper mapper, CompanyQuery companyQuery)
            : base(context, mapper)
        {
            _companyQuery = companyQuery;
        }

        public async Task<Pagination<CompanyResponse>> PaginatedCompanies(
            int pageNumber,
            int pageSize,
            string? searchTerm
        )
        {
            var query = _companyQuery.PaginatedCompanies(searchTerm);

            return await PaginationHelper.PaginateAndMap<Company, CompanyResponse>(
                query,
                pageNumber,
                pageSize,
                _mapper
            );
        }

        public async Task<List<CompanyResponse>> ListedCompanies(string? searchTerm)
        {
            var companies = await _companyQuery.ListedCompanies(searchTerm);

            return _mapper.Map<List<CompanyResponse>>(companies);
        }

        public async Task<CompanyResponse> GetCompanyById(int id)
        {
            var company = await _companyQuery.GetCompanyById(id);

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<CompanyResponse> CreateCompany(CompanyRequest request)
        {
            var company = _mapper.Map<Company>(request);
            company.DateCreated = DateTimeHelper.GetPhilippineTime();
            company.Status = Status.Active;

            foreach (var rep in company.Representative)
            {
                rep.Status = Status.Active;
            }

            await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<CompanyResponse> ToggleStatus(int id)
        {
            var company = await _companyQuery.PatchCompanyById(id);

            if (company == null)
            {
                throw new Exception($"Company with id {id} not found.");
            }
            else
            {
                company.Status = company.Status == Status.Active ? Status.Inactive : Status.Active;
                company.DateUpdated = DateTimeHelper.GetPhilippineTime();

                await _context.SaveChangesAsync();
                return _mapper.Map<CompanyResponse>(company);
            }
        }

        public async Task<CompanyResponse> DeleteCompany(int id)
        {
            var company = await _companyQuery.PatchCompanyById(id);

            if (company == null)
            {
                throw new Exception($"Company with id {id} not found.");
            }
            else
            {
                _context.Company.Remove(company);
                await _context.SaveChangesAsync();

                return _mapper.Map<CompanyResponse>(company);
            }
        }
    }
}
