using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Services
{
    public class ProductService : BaseService , IProductService
    {
        private readonly ProductValidator _productValidator;
        private readonly ProductQueries _productQueries;
        public ProductService(AppDbContext context, IMapper mapper, ProductValidator productValidator, ProductQueries productQueries) : base (context, mapper)
        {
            _productValidator = productValidator;
            _productQueries = productQueries;
        }
        // [HttpGet("products/list")]
        public async Task<List<ProductOnlyResponse>> productslist(int id)
        {
            var products = await _productQueries.productlistquery(id);

            return _mapper.Map<List<ProductOnlyResponse>>(products);
        }
        // [HttpGet("products")]
        public async Task<Pagination<ProductResponse>> allproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _productQueries.productsquery(searchTerm);
            return await PaginationHelper.paginateandmap<Product, ProductResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("products/company-inventory/as-of")]
        public async Task<Pagination<ProductWithReceivingAndDispatchingResponse>> customerbasedproducts_asof(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null)
        {
            var query = _productQueries.productwithcompany_asof(company);
            var totalCount = await query.CountAsync();

            var products = await PaginationHelper.paginateandproject<Product, ProductWithReceivingAndDispatchingResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(products, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("products/company-inventory/from-to")]
        public async Task<Pagination<ProductWithReceivingAndDispatchingResponse>> customerbasedproducts_fromto(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.productwithcompanyquery(company, from, to);
            var totalCount = await query.CountAsync();

            var products = await PaginationHelper.paginateandproject<Product, ProductWithReceivingAndDispatchingResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(products, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("product/receivings-dispatchings")]
        public async Task<Pagination<ProductBasedReceivingDispatchingResponse>> productbasedreceivingdispatching_summary(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null)
        {
            var query = _productQueries.productwithcompanyquery( productId : productId);
            var totalCount = await query.CountAsync();

            var products = await PaginationHelper.paginateandproject<Product, ProductBasedReceivingDispatchingResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(products, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("products/company-based")]
        public async Task<List<BasicProductResponse>> customerbasedproductsbasic(int id)
        {
            var products = await _productQueries.companybasesproductslist(id);
            return _mapper.Map<List<BasicProductResponse>>(products);
        }
        // [HttpGet("products/company-inventory/summary")]
        public async Task<Pagination<ProductSummary>> customerbasedproducts_summary(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.productwithcompanyquery(company, from, to);
            var totalCount = await query.CountAsync();

            var products = await PaginationHelper.paginateandproject<Product, ProductSummary>(
                query, pageNumber, pageSize, _mapper);
            
            return PaginationHelper.paginatedresponse(products, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("product/{id}")]
        public async Task<ProductResponse> getproduct(int id)
        {
            var product = await getproductdata(id);

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpGet("product/product-code")]
        public async Task<ProductResponse> getproductbycode(string productCode)
        {
            var product = await _productQueries.productsbycode(productCode);

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpGet("product/product-code/dispatch")]
        public async Task<List<ProductCodeResponse>> getproductcodefordispatch()
        {
            var products = await _productQueries.productwithreceivings();

            return _mapper.Map<List<ProductCodeResponse>>(products);
        }
        //[HttpGet("product/receiving-detail/dispatch")]
        public async Task<ProductWithReceivingResponse> getproductwithreceivingdetail(string productCode)
        {
            var product = await _productQueries.productwithreceivingdetail(productCode);

            var detail = product.Receiving
                .Where(r => r.Received)
                .SelectMany(r => r.Receivingdetails)
                .Where(r => !r.Fulldispatched && r.Pallet.Occupied)
                .ToList();
            var received = product.Receiving
                .FirstOrDefault(r => r.Productid == product.Id);
            var receivingDetailsResponse = new List<ProductReceivingDetailResponse>();
            var positionIds = detail.Select(p => p.Positionid).Distinct().ToList();
            var positions = await _context.Palletpositions
                .Include(p => p.Coldstorage)
                .Where(p => positionIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (var receivingDetail in detail)
            {
                var totalDispatchedQuantity = await _context.Dispatchingdetails
                    .Where(d => d.Receivingdetailid == receivingDetail.Id)
                    .SumAsync(d => (int?)d.Quantity) ?? 0;

                var totalDispatchedWeight = await _context.Dispatchingdetails
                    .Where(d => d.Receivingdetailid == receivingDetail.Id)
                    .SumAsync(d => (double?)d.Totalweight) ?? 0;

                var totalRepalletizedFromQuantity = await _context.Repalletizationdetails
                    .Include(r => r.Repalletization)
                    .Where(r => r.Repalletization.Frompalletid == receivingDetail.Palletid &&
                    r.Receivingdetailid == receivingDetail.Id)
                    .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                var totalRepalletizedToQuantity = await _context.Repalletizationdetails
                    .Include(r => r.Repalletization)
                    .Where(r => r.Repalletization.Topalletid == receivingDetail.Palletid &&
                    r.Receivingdetailid != receivingDetail.Id)
                    .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                var totalRepalletizedFromWeight = await _context.Repalletizationdetails
                    .Include(r => r.Repalletization)
                    .Where(r => r.Repalletization.Frompalletid == receivingDetail.Palletid &&
                    r.Receivingdetailid == receivingDetail.Id)
                    .SumAsync(r => (double?)r.Weightmoved) ?? 0;

                var totalRepalletizedToWeight = await _context.Repalletizationdetails
                    .Include(r => r.Repalletization)
                    .Where(r => r.Repalletization.Topalletid == receivingDetail.Palletid &&
                    r.Receivingdetailid != receivingDetail.Id)
                    .SumAsync(r => (double?)r.Weightmoved) ?? 0;

                var remainingQuantity = receivingDetail.Quantityinapallet - totalDispatchedQuantity - totalRepalletizedFromQuantity + totalRepalletizedToQuantity;
                var remainingWeight = Math.Round(receivingDetail.Totalweight - totalRepalletizedFromWeight + totalRepalletizedToWeight, 2);

                if (remainingQuantity <= 0) continue;

                positions.TryGetValue(receivingDetail.Positionid, out var position);

                var detailResponse = _mapper.Map<ProductReceivingDetailResponse>(receivingDetail);
                detailResponse.Quantityinapallet = remainingQuantity;
                detailResponse.Totalweight = Math.Round(remainingWeight, 2);

                receivingDetailsResponse.Add(detailResponse);
            }

            var overallWeight = product.Receiving
                .Where(r => r.Received)
                .Sum(r => r.Overallweight);

            var response = _mapper.Map<ProductWithReceivingResponse>(product);
            response.ReceivingDetail = receivingDetailsResponse;
            response.Overallweight = Math.Round(overallWeight, 2);
            return response;
        }
        // [HttpPost("product")]
        public async Task<ProductResponse> addproduct(ProductRequest request)
        {
            _productValidator.ValidateProductRequest(request);

            var product = _mapper.Map<Product>(request);
            product.Active = true;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpPatch("product/update/{id}")]
        public async Task<ProductResponse> updateproduct(ProductRequest request, int id)
        {
            var product = await getproductid(id);

            _mapper.Map(request, product);

            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // [HttpPatch("product/toggle-active")]
        public async Task<ProductActiveResponse> toggleactive (int id)
        {
            var product = await getproductid(id);

            product.Active = !product.Active;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductActiveResponse>(product);
        }
        // [HttpPatch("product/hide/{id}")]
        public async Task<ProductResponse> hideproduct(int id)
        {
            var product = await getproductid(id);

            product.Removed = !product.Removed;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // [HttpDelete("product/delete/{id}")]
        public async Task<ProductResponse> deleteproduct(int id)
        {
            var product = await getproductid(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        // Helpers
        private async Task<Product?> getproductid(int id)
        {
            return await _productQueries.patchmethodproductid(id);
        }
        private async Task<Product?> getproductdata(int id)
        {
            return await _productQueries.getmethodproductid(id);
        }
        private async Task<ProductResponse> productResponse(int id)
        {
            var response = await getproductdata(id);
            return _mapper.Map<ProductResponse>(response);
        }
    }
}
