using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;

namespace CSMapi.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly CategoryQueries _query;
        private readonly CategoryValidators _validator;
        public CategoryService(CategoryQueries query, CategoryValidators validator, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
            _validator = validator;
        }
        // [HttpGet("categories")]
        public async Task<List<CategoryResponse>> CategoriesList(string? searchTerm = null)
        {
            var categories = await _query.CategoriesList(searchTerm);
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
        // [HttpGet("category/{id}")]
        public async Task<CategoryResponse> GetCategory(int id)
        {
            var category = await GetCategoryId(id);
            return _mapper.Map<CategoryResponse>(category);
        }
        // [HttpPost("category")]
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            await _validator.ValidateCategoryRequest(request);
            var category = _mapper.Map<Category>(request);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return await CategoryResponse(category.Id);
        }
        // [HttpPatch("category/update/{id}")]
        public async Task<CategoryResponse> UpdateCategory(CategoryRequest request, int id)
        {
            var category = await PatchCategoryId(id);

            _mapper.Map(request, category);

            await _context.SaveChangesAsync();

            return await CategoryResponse(category.Id);
        }
        // [HttpPatch("category/hide/{id}")]
        public async Task<CategoryResponse> RemoveCategory(int id)
        {
            var category = await PatchCategoryId(id);

            category.Removed = !category.Removed;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return await CategoryResponse(category.Id);
        }
        // [HttpDelete("category/delete/{id}")]
        public async Task<CategoryResponse> DeleteCategory(int id)
        {
            var category = await PatchCategoryId(id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return await CategoryResponse(category.Id);
        }
        // Helpers
        private async Task<Category?> GetCategoryId(int id)
        {
            return await _query.GetCategoryId(id);
        }
        private async Task<Category?> PatchCategoryId(int id)
        {
            return await _query.PatchCategoryId(id);
        }
        private async Task<CategoryResponse> CategoryResponse(int id)
        {
            var response = await GetCategoryId(id);
            return _mapper.Map<CategoryResponse>(response);
        }
        
    }
}
