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
        // Query for fetching all categories with optional filter for category name (List)
        public async Task<List<Category>> categorieslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Categories
                    .AsNoTracking()
                    .Where(c => c.Name == searchTerm && !c.Removed)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Categories
                    .AsNoTracking()
                    .Where(c => !c.Removed)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific category for GET method
        public async Task<Category?> getcategoryid(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific category for PATCH/PUT/DELETE methods
        public async Task<Category?> patchcategoryid(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
