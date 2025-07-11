using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;

namespace CSMapi.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly CategoryQueries _query;
        public CategoryService(CategoryQueries query, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("categories")]
        public async Task<List<CategoryResponse>> categorieslist(string? searchTerm = null)
        {
            var categories = await _query.categorieslist(searchTerm);
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
        // [HttpGet("category/{id}")]
        public async Task<CategoryResponse> getcategory(int id)
        {
            var category = await getcategoryid(id);
            return _mapper.Map<CategoryResponse>(category);
        }
        // [HttpPost("category")]
        public async Task<CategoryResponse> createcategory(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // [HttpPatch("category/update/{id}")]
        public async Task<CategoryResponse> updatecategory(CategoryRequest request, int id)
        {
            var category = await patchcategoryid(id);

            _mapper.Map(request, category);

            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // [HttpPatch("category/hide/{id}")]
        public async Task<CategoryResponse> removecategory(int id)
        {
            var category = await patchcategoryid(id);

            category.Removed = !category.Removed;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // [HttpDelete("category/delete/{id}")]
        public async Task<CategoryResponse> deletecategory(int id)
        {
            var category = await patchcategoryid(id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return await categoryResponse(category.Id);
        }
        // Helpers
        private async Task<Category?> getcategoryid(int id)
        {
            return await _query.getcategoryid(id);
        }
        private async Task<Category?> patchcategoryid(int id)
        {
            return await _query.patchcategoryid(id);
        }
        private async Task<CategoryResponse> categoryResponse(int id)
        {
            var response = await getcategoryid(id);
            return _mapper.Map<CategoryResponse>(response);
        }
    }
}
