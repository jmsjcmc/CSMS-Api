using csms_backend.Models;
using csms_backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductService _productService;
        public ProductController(Context context, ProductService productService) : base(context)
        {
            _productService = productService;
        }

        [HttpPost("product/create")]
        public async Task<ActionResult<ProductResponse>> CreateProduct(
            [FromBody] ProductRequest request)
        {
            try
            {
                var response = await _productService.CreateProduct(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("product/toggle-status")]
        public async Task<ActionResult<ProductResponse>> ToggleStatus(
            [FromQuery] int id)
        {
            try
            {
                var response = await _productService.ToggleStatus(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("product/delete/{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(
            [FromQuery] int id)
        {
            try
            {
                var response = await _productService.DeleteProduct(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("products/paginated")]
        public async Task<ActionResult<Pagination<ProductResponse>>> PaginatedProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _productService.PaginatedProducts(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("products/list")]
        public async Task<ActionResult<List<ProductResponse>>> ListedProducts(
            [FromQuery] string? searchTerm)
        {
            try
            {
                var response = await _productService.ListedProducts(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(
            [FromQuery] int id)
        {
            try
            {
                var response = await _productService.GetProductById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
