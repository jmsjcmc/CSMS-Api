using AutoMapper;
using csms_backend.Models;
using csms_backend.Models.Entities;
using csms_backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace csms_backend.Controllers
{
    public class BusinessUnitService : BaseService, BusinessUnitInterface
    {
        private readonly BusinessUnitQuery _buQuery;

        public BusinessUnitService(Context context, IMapper mapper, BusinessUnitQuery buQuery)
            : base(context, mapper)
        {
            _buQuery = buQuery;
        }

        // [HttpGet("business-units/paginated")]
        public async Task<Pagination<BusinessUnitResponse>> PaginatedBUs(
            int pageNumber,
            int pageSize,
            string? searchTerm
        )
        {
            var query = _buQuery.PaginatedBUs(searchTerm);

            return await PaginationHelper.PaginateAndMap<BusinessUnit, BusinessUnitResponse>(
                query,
                pageNumber,
                pageSize,
                _mapper
            );
        }

        // [HttpGet("business-units/list")]
        public async Task<List<BusinessUnitResponse>> ListedBUs(string? searchTerm)
        {
            var businessUnits = await _buQuery.ListedBUs(searchTerm);
            return _mapper.Map<List<BusinessUnitResponse>>(businessUnits);
        }

        // [HttpGet("business-unit/{id}")]
        public async Task<BusinessUnitResponse> GetBUById(int id)
        {
            var bu = await _buQuery.GetBUById(id);
            return _mapper.Map<BusinessUnitResponse>(bu);
        }

        // [HttpPost("business-unit/create")]
        public async Task<BusinessUnitResponse> CreateBU(BusinessUnitRequest request)
        {
            if (await _context.BusinessUnit.AnyAsync(bu => bu.Name == request.Name))
                throw new Exception($"Business Unit {request.Name} already added.");

            var bu = _mapper.Map<BusinessUnit>(request);
            bu.Status = Status.Active;
            bu.DateCreated = DateTimeHelper.GetPhilippineTime();

            await _context.BusinessUnit.AddAsync(bu);
            await _context.SaveChangesAsync();

            return _mapper.Map<BusinessUnitResponse>(bu);
        }

        //
        public async Task<BusinessUnitResponse> ToggleStatus(int id)
        {
            var bu = await _buQuery.PatchBUById(id);
            if (bu == null)
            {
                throw new Exception($"Business Unit with ID {id} not found.");
            }
            else
            {
                bu.Status = bu.Status == Status.Active ? Status.Inactive : Status.Active;
                bu.DateUpdated = DateTimeHelper.GetPhilippineTime();

                await _context.SaveChangesAsync();
                return _mapper.Map<BusinessUnitResponse>(bu);
            }
        }

        //
        public async Task<BusinessUnitResponse> DeleteBU(int id)
        {
            var bu = await _buQuery.PatchBUById(id);
            if (bu == null)
            {
                throw new Exception($"Business Unit with ID {id} not found.");
            }
            else
            {
                _context.BusinessUnit.Remove(bu);
                await _context.SaveChangesAsync();

                return _mapper.Map<BusinessUnitResponse>(bu);
            }
        }
    }
}
