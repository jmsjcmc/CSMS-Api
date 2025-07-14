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
        public IQueryable<Contract> contractsquery(string? searchTerm = null)
        {
            var query = batchgetquery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Lesseecompany.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching all contracts with optional filter for lessee company (List)
        public async Task<List<Contract>> contracstlist(string? searchTerm = null)
        {
            return await contractsquery(searchTerm).ToListAsync();
        }
        // Query for fetching specific contract for GET method
        public async Task<Contract?> getmethodcontractid(int id)
        {
            return await batchgetquery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific contract for PATCH/PUT/DELETE methods
        public async Task<Contract?> patchmethodcontractid(int id)
        {
            return await patchquery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Helpers
        private IQueryable<Contract> batchgetquery()
        {
            return _context.Contracts
                .AsNoTracking()
                .Include(c => c.Creator)
                .Include(c => c.Leasedpremises)
                .Where(c => !c.Removed)
                .OrderByDescending(c => c.Id);
        }
        private IQueryable<Contract> patchquery()
        {
            return _context.Contracts
                .Include(c => c.Creator)
                .Include(c => c.Leasedpremises)
                .Where(c => !c.Removed);
        }
    }
}
