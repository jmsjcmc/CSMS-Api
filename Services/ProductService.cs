using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ProductValidator _productValidator;
        private readonly ProductQueries _productQueries;
        public ProductService(AppDbContext context, IMapper mapper, ProductValidator productValidator, ProductQueries productQueries) : base(context, mapper)
        {
            _productValidator = productValidator;
            _productQueries = productQueries;
        }
        // [HttpGet("products/list")]
        public async Task<List<ProductOnlyResponse>> ProductsList(int id)
        {
            var products = await _productQueries.ProductListQuery(id);

            return _mapper.Map<List<ProductOnlyResponse>>(products);
        }
        // [HttpGet("products")]
        public async Task<Pagination<ProductResponse>> AllProducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _productQueries.ProductsQuery(searchTerm);
            return await PaginationHelper.PaginateAndMap<Product, ProductResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("products/company-inventory/as-of")]
        public async Task<Pagination<ProductCompanyInventoryAsOfResponse>> customerbasedproducts_asof(
            int pageNumber = 1,
            int pageSize = 10,
            int? companyId = null,
            DateTime? asOf = null)
        {
            if (!companyId.HasValue)
            {
                return PaginationHelper.PaginatedResponse(new List<ProductCompanyInventoryAsOfResponse>(), 0, pageNumber, pageSize);
            }

            // Pre-aggregate dispatched quantities
            var dispatched = await _context.Dispatchingdetails
                .Where(d => !asOf.HasValue || d.Dispatching.Dispatchdate <= asOf)
                .GroupBy(d => d.Receivingdetailid)
                .Select(g => new
                {
                    ReceivingDetailId = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalWeight = g.Sum(x => x.Totalweight)
                })
                .ToDictionaryAsync(x => x.ReceivingDetailId);

            // Pre-aggregate repalletization movements (OUT and IN separately)
            var repalletizationOut = await _context.Repalletizations
                .Where(r => !asOf.HasValue || r.Createdon <= asOf)
                .GroupBy(r => r.Fromreceivingdetailid)
                .Select(g => new
                {
                    ReceivingDetailId = g.Key,
                    QuantityMoved = g.Sum(r => r.Quantitymoved),
                    WeightMoved = g.Sum(r => r.Weightmoved)
                })
                .ToDictionaryAsync(x => x.ReceivingDetailId);

            var repalletizationIn = await _context.Repalletizations
                .Where(r => !asOf.HasValue || r.Createdon <= asOf)
                .GroupBy(r => r.Toreceivingdetailtid)
                .Select(g => new
                {
                    ReceivingDetailId = g.Key,
                    QuantityMoved = g.Sum(r => r.Quantitymoved),
                    WeightMoved = g.Sum(r => r.Weightmoved)
                })
                .ToDictionaryAsync(x => x.ReceivingDetailId);

            // Query products
            var query = _productQueries.ProductWithCompany_AsOf(companyId, asOf);
            var totalCount = await query.CountAsync();

            if (totalCount == 0)
            {
                return PaginationHelper.PaginatedResponse(new List<ProductCompanyInventoryAsOfResponse>(), 0, pageNumber, pageSize);
            }

            var products = await PaginationHelper.PaginatedAndProject<Product, ProductCompanyInventoryAsOfResponse>(
                query, pageNumber, pageSize, _mapper);

            var productsWithInventory = new List<ProductCompanyInventoryAsOfResponse>();

            foreach (var product in products)
            {
                var adjustedReceiving = new List<ProductCompanyInventoryReceivingResponse>();

                foreach (var rec in product.Receiving)
                {
                    // Get aggregated values
                    dispatched.TryGetValue(rec.Id, out var dispAgg);
                    repalletizationOut.TryGetValue(rec.Id, out var repOutAgg);
                    repalletizationIn.TryGetValue(rec.Id, out var repInAgg);

                    // Calculate remaining inventory
                    var remainingQty = rec.Quantityinapallet
                        - (dispAgg?.TotalQuantity ?? 0)
                        - (repOutAgg?.QuantityMoved ?? 0)
                        + (repInAgg?.QuantityMoved ?? 0);

                    // Skip if no remaining inventory
                    if (remainingQty <= 0) continue;

                    var remainingWeight = rec.Totalweight
                        - (dispAgg?.TotalWeight ?? 0)
                        - (repOutAgg?.WeightMoved ?? 0)
                        + (repInAgg?.WeightMoved ?? 0);

                    adjustedReceiving.Add(new ProductCompanyInventoryReceivingResponse
                    {
                        Id = rec.Id,
                        Document = rec.Document,
                        Requestor = rec.Requestor,
                        Approver = rec.Approver,
                        Datereceived = rec.Datereceived,
                        Quantityinapallet = remainingQty,
                        Totalweight = Math.Round(remainingWeight, 2),
                        ReceivingDetail = rec.ReceivingDetail
                    });
                }

                if (adjustedReceiving.Any())
                {
                    productsWithInventory.Add(new ProductCompanyInventoryAsOfResponse
                    {
                        Id = product.Id,
                        Category = product.Category,
                        Productcode = product.Productcode,
                        Productname = product.Productname,
                        Receiving = adjustedReceiving
                    });
                }
            }

            return new Pagination<ProductCompanyInventoryAsOfResponse>
            {
                Items = productsWithInventory,
                Totalcount = totalCount,
                Pagenumber = pageNumber,
                Pagesize = pageSize
            };
        }
        // [HttpGet("products/company-inventory/from-to")]
        public async Task<Pagination<ProductWithReceivingAndDispatchingResponse>> CustomerBasedProducts_FromTo(
            int pageNumber = 1,
            int pageSize = 10,
            string? company = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.ProductWithCompanyQuery(company, from, to);
            var totalCount = await query.CountAsync();

            var products = await PaginationHelper.PaginatedAndProject<Product, ProductWithReceivingAndDispatchingResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.PaginatedResponse(products, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("product/receivings")]
        public async Task<Pagination<ProductBasedReceiving>> ProductBasedReceivings(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.ProductWithCompanyQuery(from: from, to: to, productId: productId);
            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = products.SelectMany(p =>
            from r in p.Receiving.Where(r => r.Received).DefaultIfEmpty()
            select new ProductBasedReceiving
            {
                Id = p.Id,
                Productname = p.Productname,
                Productpackaging = p.Productpackaging,
                Deliveryunit = p.Deliveryunit,
                Datereceived = r?.Datereceived,
                Quantityinapallet = r?.Receivingdetails.Sum(r => r.Duquantity) ?? 0
            }).ToList();

            return PaginationHelper.PaginatedResponse(mapped, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("product/dispatchings")]
        public async Task<Pagination<ProductBasedDispatching>> ProductBasedDispatchings(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.ProductsWithCompanyDispatching(from: from, to: to, productId: productId);
            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = products.SelectMany(p =>
                p.Dispatching
                .Where(d => d.Dispatched)
                .Select(d => new ProductBasedDispatching
                {
                    Id = p.Id,
                    Productname = p.Productname,
                    Productpackaging = p.Productpackaging,
                    Deliveryunit = p.Deliveryunit,
                    Dispatchdate = d.Dispatchdate,
                    Quantity = d.Dispatchingdetails.Sum(d => d.Quantity)
                })).ToList();

            return PaginationHelper.PaginatedResponse(mapped, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("products/company-based")]
        public async Task<List<BasicProductResponse>> CustomerBasedProductsBasic(int id)
        {
            var products = await _productQueries.CompanyBasedProductsList(id);
            return _mapper.Map<List<BasicProductResponse>>(products);
        }
        // [HttpGet("product/{id}")]
        public async Task<ProductResponse> GetProduct(int id)
        {
            var product = await GetProductData(id);

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpGet("product/product-code")]
        public async Task<ProductResponse> GetProductByCode(string productCode)
        {
            var product = await _productQueries.ProductsByCode(productCode);

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpGet("product/product-code/dispatch")]
        public async Task<List<ProductCodeResponse>> GetProductCodeForDispatch()
        {
            var products = await _productQueries.ProductWithReceivings();

            return _mapper.Map<List<ProductCodeResponse>>(products);
        }
        //[HttpGet("product/receiving-detail/dispatch")]
        public async Task<ProductWithReceivingResponse> GetProductWithReceivingDetail(string productCode)
        {
            var product = await _productQueries.ProductWithReceivingDetail(productCode);
            var response = _mapper.Map<ProductWithReceivingResponse>(product);

            if (response.ReceivingDetail?.Any() == true)
            {
                var allReceivingDetails = product.Receiving
                    .SelectMany(r => r.Receivingdetails)
                    .ToDictionary(r => r.Id);

                foreach (var detail in response.ReceivingDetail)
                {
                    if (allReceivingDetails.TryGetValue(detail.Id, out var originalDetail))
                    {
                        var (quantity, weight) = CalculateRemainingValues(originalDetail);
                        detail.Remainingquantity = quantity;
                        detail.Remainingweight = weight;
                    }
                    else
                    {
                        detail.Remainingquantity = detail.Quantityinapallet;
                        detail.Remainingweight = detail.Totalweight;
                    }
                }
            }
            return response;
        }
        // [HttpPost("product")]
        public async Task<ProductResponse> AddProduct(ProductRequest request)
        {
            _productValidator.ValidateProductRequest(request);

            var product = _mapper.Map<Product>(request);
            product.Active = true;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpPatch("product/update/{id}")]
        public async Task<ProductResponse> UpdateProduct(ProductRequest request, int id)
        {
            var product = await GetProductId(id);

            _mapper.Map(request, product);

            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // [HttpPatch("product/toggle-active")]
        public async Task<ProductActiveResponse> ToggleActive(int id)
        {
            var product = await GetProductId(id);

            product.Active = !product.Active;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductActiveResponse>(product);
        }
        // [HttpPatch("product/hide/{id}")]
        public async Task<ProductResponse> HideProduct(int id)
        {
            var product = await GetProductId(id);

            product.Removed = !product.Removed;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // [HttpDelete("product/delete/{id}")]
        public async Task<ProductResponse> DeleteProduct(int id)
        {
            var product = await GetProductId(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // Helpers
        private async Task<Product?> GetProductId(int id)
        {
            return await _productQueries.PatchProductId(id);
        }
        private async Task<Product?> GetProductData(int id)
        {
            return await _productQueries.GetProductId(id);
        }
        private async Task<ProductResponse> ProductResponse(int id)
        {
            var response = await GetProductData(id);
            return _mapper.Map<ProductResponse>(response);
        }
        private (int remainingQuantity, double remainingWeight) CalculateRemainingValues(ReceivingDetail receivingDetail)
        {
            int originalQty = receivingDetail.Quantityinapallet;
            double originalWgt = receivingDetail.Totalweight;

            // Calculate dispatched quantities
            var validDispatches = receivingDetail.DispatchingDetail?
                .Where(d => d.Dispatching != null &&
                           d.Dispatching.Dispatched &&
                          !d.Dispatching.Declined &&
                          !d.Dispatching.Removed)
                .ToList() ?? new List<DispatchingDetail>();

            int dispatchedQty = validDispatches.Sum(d => d.Quantity);
            double dispatchedWgt = validDispatches.Sum(d => d.Totalweight);

            // Calculate repalletization adjustments
            var outgoingRepallets = receivingDetail.Outgoingrepalletization?.ToList() ?? new List<Repalletization>();
            var incomingRepallets = receivingDetail.Incomingrepalletization?.ToList() ?? new List<Repalletization>();

            int outgoingQty = outgoingRepallets.Sum(r => r.Quantitymoved);
            int incomingQty = incomingRepallets.Sum(r => r.Quantitymoved);

            // Calculate net remaining quantity
            int netQtyChange = incomingQty - outgoingQty;
            int remainingQty = Math.Max(0, originalQty - dispatchedQty + netQtyChange);

            // Calculate weight using original unit weight
            double wpu = originalQty > 0 ? originalWgt / originalQty : 0;
            double outgoingWgt = outgoingQty * wpu;
            double incomingWgt = incomingQty * wpu;

            double remainingWgt = originalWgt - dispatchedWgt - outgoingWgt + incomingWgt;
            remainingWgt = Math.Max(0, Math.Round(remainingWgt, 2));

            return (remainingQty, remainingWgt);
        }
    }
}
