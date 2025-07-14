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
        public IQueryable<Customer> customeronlyquery(string? searchTerm = null)
        {
            var query = batchgetquery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Companyname.Contains(searchTerm));
            }
            return query;
        }
        // Query for fetcing all active customers
        public async Task<List<Customer>> activecustomersquery(string? searchTerm = null)
        {
            return await customeronlyquery(searchTerm).ToListAsync();
        }
        // Query for fetching specific customer for GET method
        public async Task<Customer?> getmethodcustomerid(int id)
        {
            return await batchgetquery()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific customer for PATCH/PUT/DELETE methods
        public async Task<Customer?> patchmethodcustomerid(int id)
        {
            return await patchquery()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Helpers
        private IQueryable<Customer> batchgetquery()
        {
            return _context.Customers
                .AsNoTracking()
                .Include(c => c.Product)
                .Where(c => !c.Removed)
                .OrderByDescending(c => c.Id);
        }
        private IQueryable<Customer> patchquery()
        {
            return _context.Customers
                .Include(c => c.Product)
                .Where(c => !c.Removed);
        }
    }
}
