using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class CategoryQueries
    {
        private readonly AppDbContext _context;
        private readonly CategoryValidators _validator;
        public CategoryQueries(AppDbContext context, CategoryValidators validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching all paginated categories with optional filter for category name
        public IQueryable<Category> PaginatedCategories(string? searchTerm = null)
        {
            var query = _context.Categories
                .AsNoTracking()
                .Where(c => c.Removed)
                .Include(c => c.Product)
                .OrderByDescending(c => c.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching all categories with optional filter for category name (List)
        public async Task<List<Category>> CategoriesList(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Categories
                    .AsNoTracking()
                    .Where(c => c.Name == searchTerm && !c.Removed)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            } else
            {
                return await _context.Categories
                    .AsNoTracking()
                    .Where(c => !c.Removed)
                    .ToListAsync();
            }
        }
        // Query for fetching specific category for GET method
        public async Task<Category?> GetCategoryId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificCategory(id);

            return await _context.Categories
                .AsNoTracking()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific category for PATCH/PUT/DELETE methods
        public async Task<Category?> PatchCategoryId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificCategory(id);

            return await _context.Categories
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
