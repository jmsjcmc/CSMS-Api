using AutoMapper;
using AutoMapper.QueryableExtensions;
using csms_backend.Models;
using csms_backend.Models.Entities;
using csms_backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace csms_backend.Controllers
{
    public class ProductService : BaseService, ProductInterface
    {
        private readonly ProductQuery _productQuery;
        public ProductService(Context context, IMapper mapper, ProductQuery productQuery) : base(context, mapper)
        {
            _productQuery = productQuery;
        }

        public async Task<Pagination<ProductResponse>> PaginatedProducts(
            int pageNumber,
            int pageSize,
            string? searchTerm)
        {
            var query = _productQuery.PaginatedProducts(searchTerm);
            return await PaginationHelper.PaginateAndMap<Product, ProductResponse>(query, pageNumber, pageSize, _mapper);
        }

        public async Task<List<ProductResponse>> ListedProducts(string? searchTerm)
        {
            var products = await _productQuery.ListedProducts(searchTerm);
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetProductById(int id)
        {
            var product = await _productQuery.GetProductById(id);
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            product.Status = Status.Active;

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> ToggleStatus(int id)
        {
            var product = await _productQuery.PatchProductById(id);

            if (product == null)
            {
                throw new Exception($"Product with Id {id} not found.");
            }
            else
            {
                product.Status = product.Status == Status.Active
                    ? Status.Inactive
                    : Status.Active;

                await _context.SaveChangesAsync();
                return _mapper.Map<ProductResponse>(product);
            }
        }

        public async Task<ProductResponse> DeleteProduct(int id)
        {
            var product = await _productQuery.PatchProductById(id);
            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }
            else
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();

                return _mapper.Map<ProductResponse>(product);
            }
        }
    }
    public class CategoryService : BaseService, CategoryInterface
    {
        private readonly CategoryQuery _categoryQuery;
        public CategoryService(Context context, IMapper mapper, CategoryQuery categoryQuery) : base (context, mapper)
        {
            _categoryQuery = categoryQuery;
        }
        // [HttpGet("categories/list")]
        public async Task<List<CategoryResponse>> ListedCategories(string? searchTerm)
        {
            var categories = await _categoryQuery.ListedCategories(searchTerm);

            return _mapper.Map<List<CategoryResponse>>(categories);
        }
        // [HttpGet("category/{id}")]
        public async Task<CategoryResponse> GetCategoryById(int id)
        {
            var category = await _categoryQuery.GetCategoryById(id);
            return _mapper.Map<CategoryResponse>(category);
        }
        // [HttpPost("category/create")]
        public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);
            category.Status = Status.Active;

            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(category);
        }
        // [HttpPut("category/update/{id}")]
        public async Task<CategoryResponse> UpdateCategory(CategoryRequest request, int id)
        {
            var category = await _categoryQuery.PatchCategoryById(id);
            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            } else
            {
                _mapper.Map(request, id);

                await _context.SaveChangesAsync();

                var updatedCategory = await _context.Category
                    .Where(c => c.Id == category.Id)
                    .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (updatedCategory == null)
                {
                    throw new Exception("No user response.");
                } else
                {
                    return updatedCategory;
                }
            }
        }
        // [HttpPut("category/toggle-status")]
        public async Task<CategoryResponse> ToggleStatus(int id)
        {
            var category = await _categoryQuery.PatchCategoryById(id);
            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }
            else
            {
                category.Status = category.Status == Status.Active
                    ? Status.Inactive
                    : Status.Active;

                await _context.SaveChangesAsync();
                return _mapper.Map<CategoryResponse>(category);
            }
        }
        // [HttpDelete("category/delete/{id}")]
        public async Task<CategoryResponse> DeleteCategory(int id)
        {
            var category = await _categoryQuery.PatchCategoryById(id);
            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            } else
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();

                return _mapper.Map<CategoryResponse>(category);
            }
        }
    }
}
