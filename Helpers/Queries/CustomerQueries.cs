using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class CustomerQueries
    {
        private readonly AppDbContext _context;
        public CustomerQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching customers with optional filter for company name
        public IQueryable<Customer?> customeronlyquery(string? searchTerm = null)
        {
            var query = _context.Customers
                   .AsNoTracking()
                   .Where(c => !c.Removed)
                   .OrderByDescending(c => c.Id)
                   .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Companyname == searchTerm);
            }
            return query;
        }
        // Query for fetcing all active customers
        public async Task<List<Customer>> activecustomersquery()
        {
            return await _context.Customers
                    .AsNoTracking()
                    .Where(c => c.Active && !c.Removed)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
        }
        // Query for fetching specific customer for GET method
        public async Task<Customer?> getmethodcustomerid(int id)
        {
            return await _context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific customer for PATCH/PUT/DELETE methods
        public async Task<Customer?> patchmethodcustomerid(int id)
        {
            return await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
