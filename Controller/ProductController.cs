using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Models;
using CSMapi.Services;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Controller
{
    public class ProductController : BaseApiController
    {
        private readonly ProductService _productService;
        private readonly ProductExcel _productExcel;
        public ProductController(AppDbContext context, IMapper mapper, ProductService productService, ProductExcel productExcel) : base (context, mapper)
        {
            _productExcel = productExcel;
            _productService = productService;
        }
        // Fetch all products without pagination
        [HttpGet("products/list")]
        public async Task<ActionResult<List<ProductOnlyResponse>>> ProductsList(int id)
        {
            try
            {
                var response = await _productService.ProductsList(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products with pagination
        [HttpGet("products")]
        public async Task<ActionResult<Pagination<ProductResponse>>> AllProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _productService.AllProducts(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products based on customer with optional filter as of
        [HttpGet("products/company-inventory/as-of")]
        public async Task<ActionResult<Pagination<ProductCompanyInventoryAsOfResponse>>> companybasedproducts_asof(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? companyId = null,
            [FromQuery] DateTime? asOf = null)
        {
            try
            {
                var response = await _productService.customerbasedproducts_asof(pageNumber, pageSize, companyId, asOf);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific product receivings data
        [HttpGet("product/receivings")]
        public async Task<ActionResult<Pagination<ProductBasedReceiving>>> ProductBasedReceivings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? productId = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            try
            {
                var response = await _productService.ProductBasedReceivings(pageNumber, pageSize, productId, from, to);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific product dispatching data
        [HttpGet("product/dispatchings")]
        public async Task<ActionResult<Pagination<ProductBasedDispatching>>> ProductBasedDispatchings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? productId = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            try
            {
                var response = await _productService.ProductBasedDispatchings(pageNumber, pageSize, productId, from, to);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all product based on company id
        [HttpGet("products/company-based")]
        public async Task<ActionResult<List<BasicProductResponse>>> CustomerBasedProductsBasic(int id)
        {
            try
            {
                var response = await _productService.CustomerBasedProductsBasic(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products based on customer with optional filter from and to
        [HttpGet("products/company-inventory/from-to")]
        public async Task<ActionResult<Pagination<ProductWithReceivingAndDispatchingResponse>>> CompanyBasedProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? company = null,
            [FromQuery] DateTime? from = null, 
            [FromQuery] DateTime? to = null)
        {
            try
            {
                var response = await _productService.CustomerBasedProducts_FromTo(pageNumber, pageSize, company, from, to);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Products Template
        [HttpGet("products/template")]
        public async Task<ActionResult> ProductTemplate()
        {
            try
            {
                var file = await Task.Run(() => _productExcel.generateproducttemplate());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductTemplate.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export products
        [HttpGet("products/export")]
        public async Task<ActionResult> ExportProducts()
        {
            try
            {
                var products = await _context.Products
                    .ToListAsync();

                var file = _productExcel.exportproducts(products);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific product
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            try
            {
                var response = await _productService.GetProduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products for receiving request
        [HttpGet("product/product-code")]
        public async Task<ActionResult<ProductResponse>> GetProductByCode(string productCode)
        {
            try
            {
                var response = await _productService.GetProductByCode(productCode);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch product code for dispatching 
        [HttpGet("product/product-code/dispatch")]
        public async Task<ActionResult<List<ProductCodeResponse>>> GetProductCodeForDispatch()
        {
            try
            {
                var response = await _productService.GetProductCodeForDispatch();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch receiving details through product code
        [HttpGet("product/receiving-detail/dispatch")]
        public async Task<ActionResult<ProductWithReceivingResponse>> GetProductWithReceivingDetail(string productCode)
        {
            try
            {
                var response = await _productService.GetProductWithReceivingDetail(productCode);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create product
        [HttpPost("product")]
        public async Task<ActionResult<ProductResponse>> AddProduct([FromBody] ProductRequest request)
        {
            try
            {
                var response = await _productService.AddProduct(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import products
        [HttpPost("products/import")]
        public async Task<ActionResult<List<ProductOnlyResponse>>> ImportProducts(IFormFile file)
        {
            try
            {
                var products = await _productExcel.importproducts(file);
                await _context.BulkInsertAsync(products);

                var response = _mapper.Map<List<ProductOnlyResponse>>(products);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific product
        [HttpPatch("product/update/{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] ProductRequest request, int id)
        {
            try
            {
                var response = await _productService.UpdateProduct(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific product active status to true/false
        [HttpPatch("product/toggle-active")]
        public async Task<ActionResult<ProductActiveResponse>> ToggleActive(int id)
        {
            try
            {
                var response = await _productService.ToggleActive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific product without removing in Database (Soft Delete)
        [HttpPatch("product/hide/{id}")]
        public async Task<ActionResult<ProductResponse>> HideProduct(int id)
        {
            try
            {
                var response = await _productService.HideProduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific product in Database
        [HttpDelete("product/delete/{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(int id)
        {
            try
            {
                var response = await _productService.DeleteProduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
