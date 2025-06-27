using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class ContractQueries
    {
        private readonly AppDbContext _context;
        public ContractQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all contracts with optional filter for lesseecompany
        public IQueryable<Contract?> contractsquery(string? searchTerm = null)
        {
            var query = _context.Contracts
                  .AsNoTracking()
                  .Include(c => c.Leasedpremises)
                  .Where(c => !c.Removed)
                  .OrderByDescending(c => c.Createdon)
                  .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Lesseecompany == searchTerm);
            }
            return query;
        }
        // Query for fetching specific contract for GET method
        public async Task<Contract?> getmethodcontractid(int id)
        {
            return await _context.Contracts
                .AsNoTracking()
                .Include(c => c.Leasedpremises)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific contract for PATCH/PUT/DELETE methods
        public async Task<Contract?> patchmethodcontractid(int id)
        {
            return await _context.Contracts
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
