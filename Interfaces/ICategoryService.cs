using CSMapi.Models;

namespace CSMapi.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> CategoriesList(string? searchTerm = null);
        Task<CategoryResponse> GetCategory(int id);
        Task<CategoryResponse> CreateCategory(CategoryRequest request);
        Task<CategoryResponse> UpdateCategory(CategoryRequest request, int id);
        Task<CategoryResponse> RemoveCategory(int id);
        Task<CategoryResponse> DeleteCategory(int id);
    }
}
