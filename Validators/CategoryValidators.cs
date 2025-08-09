using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Validators
{
    public class CategoryValidators
    {
        private readonly AppDbContext _context;
        public CategoryValidators(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateCategoryRequest(CategoryRequest request)
        {
            if (await _context.Categories.AnyAsync(c => c.Name == request.Name))
            {
                throw new ArgumentException("Category name used.");
            }
        }
        public async Task ValidateSpecificCategory(int id)
        {
            if (!await _context.Categories.AnyAsync(c => c.Id == id))
            {
                throw new ArgumentException("Category Id not found.");
            }
        }
    }
}
