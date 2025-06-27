using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Models;
using CSMapi.Services;
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
        public async Task<ActionResult<List<ProductOnlyResponse>>> productslist(string category)
        {
            try
            {
                var response = await _productService.productslist(category);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products with pagination
        [HttpGet("products")]
        public async Task<ActionResult<Pagination<ProductResponse>>> allproducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _productService.allproducts(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products based on customer with optional filter as of
        [HttpGet("products/company-inventory/as-of")]
        public async Task<ActionResult<Pagination<ProductWithReceivingAndDispatchingResponse>>> companybasedproducts_asof(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? company = null)
        {
            try
            {
                var response = await _productService.customerbasedproducts_asof(pageNumber, pageSize, company);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products based on customer with optional filter from and to
        [HttpGet("products/company-inventory/from-to")]
        public async Task<ActionResult<Pagination<ProductWithReceivingAndDispatchingResponse>>> companybasedproducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? company = null,
            [FromQuery] DateTime? from = null, 
            [FromQuery] DateTime? to = null)
        {
            try
            {
                var response = await _productService.customerbasedproducts_fromto(pageNumber, pageSize, company, from, to);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products based on customer (summary)
        [HttpGet("products/company-inventory/summary")]
        public async Task<ActionResult<Pagination<ProductSummary>>> companybasedproducts_summary(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? company = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            try
            {
                var response = await _productService.customerbasedproducts_summary(pageNumber, pageSize, company, from, to);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Products Template
        [HttpGet("products/template")]
        public async Task<ActionResult> producttemplate()
        {
            try
            {
                var file = _productExcel.generateproducttemplate();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductTemplate.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export products
        [HttpGet("products/export")]
        public async Task<ActionResult> exportproducts()
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
        public async Task<ActionResult<ProductResponse>> getproduct(int id)
        {
            try
            {
                var response = await _productService.getproduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products for receiving request
        [HttpGet("product/product-code")]
        public async Task<ActionResult<ProductResponse>> getproductbycode(string productCode)
        {
            try
            {
                var response = await _productService.getproductbycode(productCode);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch product code for dispatching 
        [HttpGet("product/product-code/dispatch")]
        public async Task<ActionResult<List<ProductCodeResponse>>> getproductcodefordispatch()
        {
            try
            {
                var response = await _productService.getproductcodefordispatch();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch receiving details through product code
        [HttpGet("product/receiving-detail/dispatch")]
        public async Task<ActionResult<ProductWithReceivingResponse>> getproductwithreceivingdetail(string productCode)
        {
            try
            {
                var response = await _productService.getproductwithreceivingdetail(productCode);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create product
        [HttpPost("product")]
        public async Task<ActionResult<ProductResponse>> addproduct([FromBody] ProductRequest request)
        {
            try
            {
                var response = await _productService.addproduct(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import products
        [HttpPost("products/import")]
        public async Task<ActionResult<List<ProductOnlyResponse>>> importproducts(IFormFile file)
        {
            try
            {
                var products = await _productExcel.importproducts(file);
                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<List<ProductOnlyResponse>>(products);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific product
        [HttpPatch("product/update/{id}")]
        public async Task<ActionResult<ProductResponse>> updateproduct([FromBody] ProductRequest request, int id)
        {
            try
            {
                var response = await _productService.updateproduct(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific product active status to true/false
        [HttpPatch("product/toggle-active")]
        public async Task<ActionResult<ProductActiveResponse>> toggleactive(int id)
        {
            try
            {
                var response = await _productService.toggleactive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific product without removing in Database (Soft Delete)
        [HttpPatch("product/hide/{id}")]
        public async Task<ActionResult> hideproduct(int id)
        {
            try
            {
                await _productService.hideproduct(id);
                return Ok("Success.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific product in Database
        [HttpDelete("product/delete/{id}")]
        public async Task<ActionResult> deleteproduct(int id)
        {
            try
            {
                await _productService.deleteproduct(id);
                return Ok("Success,");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
