using CSMapi.Models;

namespace CSMapi.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> categorieslist(string? searchTerm = null);
        Task<CategoryResponse> getcategory(int id);
        Task<CategoryResponse> createcategory(CategoryRequest request);
        Task<CategoryResponse> updatecategory(CategoryRequest request, int id);
        Task<CategoryResponse> removecategory(int id);
        Task<CategoryResponse> deletecategory(int id);
    }
}
