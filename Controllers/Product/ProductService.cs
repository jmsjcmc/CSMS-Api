using AutoMapper;
using csms_backend.Models;
using csms_backend.Models.Entities;
using csms_backend.Utils;

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
}
