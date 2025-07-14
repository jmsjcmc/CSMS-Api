using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class CategoryQueries
    {
        private readonly AppDbContext _context;
        public CategoryQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all paginated categories with optional filter for category name
        public IQueryable<Category> paginatedcategory(string? searchTerm = null)
        {
            var query = batchgetquery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching all categories with optional filter for category name (List)
        public async Task<List<Category>> categorieslist(string? searchTerm = null)
        {
            return await paginatedcategory(searchTerm).ToListAsync();
        }
        // Query for fetching specific category for GET method
        public async Task<Category?> getcategoryid(int id)
        {
            return await batchgetquery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific category for PATCH/PUT/DELETE methods
        public async Task<Category?> patchcategoryid(int id)
        {
            return await patchquery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Helpers
        private IQueryable<Category> batchgetquery()
        {
            return _context.Categories
                .AsNoTracking()
                .Include(c => c.Product)
                .Where(c => !c.Removed)
                .OrderByDescending(c => c.Id);
        }
        private IQueryable<Category> patchquery()
        {
            return _context.Categories
                .Include(c => c.Product)
                .Where(c => !c.Removed);
        }
    }
}
