using csms_backend.Models;

namespace csms_backend.Controllers
{
    public interface ProductInterface
    {
        Task<Pagination<ProductResponse>> PaginatedProducts(
            int pageNumber,
            int pageSize,
            string? searchTerm);
        Task<List<ProductResponse>> ListedProducts(string? searchTerm);
        Task<ProductResponse> GetProductById(int id);
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<ProductResponse> ToggleStatus(int id);
        Task<ProductResponse> DeleteProduct(int id);
    }
    public interface CategoryInterface
    {
        Task<List<CategoryResponse>> ListedCategories(string? searchTerm);
        Task<CategoryResponse> GetCategoryById(int id);
        Task<CategoryResponse> CreateCategory(CategoryRequest request);
        Task<CategoryResponse> UpdateCategory(CategoryRequest request, int id);
        Task<CategoryResponse> ToggleStatus(int id);
        Task<CategoryResponse> DeleteCategory(int id);
    }
}
