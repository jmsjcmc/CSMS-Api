using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class CustomerQueries
    {
        private readonly AppDbContext _context;
        private readonly CustomerValidator _validator;
        public CustomerQueries(AppDbContext context, CustomerValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching customers with optional filter for company name
        public IQueryable<Customer> CustomerOnlyQuery(string? searchTerm = null)
        {
            var query = _context.Customers
                .AsNoTracking()
                .Where(c => !c.Removed)
                .Include(c => c.Product)
                .OrderByDescending(c => c.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Companyname.Contains(searchTerm));
            }
            return query;
        }
        // Query for fetcing all active customers
        public async Task<List<Customer>> ActiveCustomersQuery(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Customers
                    .AsNoTracking()
                    .Where(c => c.Companyname == searchTerm && !c.Removed)
                    .Include(c => c.Product)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Customers
                    .AsNoTracking()
                    .Where(c => !c.Removed)
                    .Include(c => c.Product)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific customer for GET method
        public async Task<Customer?> GetCustomerId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificCustomer(id);

            return await _context.Customers
                .AsNoTracking()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific customer for PATCH/PUT/DELETE methods
        public async Task<Customer?> PatchCustomerId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificCustomer(id);

            return await _context.Customers
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
