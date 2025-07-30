using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
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
        public async Task<Pagination<ProductCompanyInventoryAsOfResponse>> customerbasedproducts_asof(
            int pageNumber = 1,
            int pageSize = 10,
            int? companyId = null,
            DateTime? asOf = null)
        {
            if (!companyId.HasValue)
            {
                return PaginationHelper.paginatedresponse(new List<ProductCompanyInventoryAsOfResponse>(), 0, pageNumber, pageSize);
            }

            DateTime? startDate = asOf?.Date;
            DateTime? endDate = asOf?.Date;

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

            // Pre-aggregate repalletization movements
            var repalletization = await _context.Repalletizationdetails
                .Where(r => !asOf.HasValue || r.Repalletization.Createdon <= asOf)
                .Select(r => new
                {
                    r.Receivingdetailid,
                    FromPalletId = (int?)r.Repalletization.Frompalletid,
                    ToPalletId = (int?)r.Repalletization.Topalletid,
                    r.Quantitymoved,
                    r.Weightmoved
                })
                .ToListAsync();

            // Separate repalletization movements
            var repalletizedFrom = repalletization
                .GroupBy(x => new { x.Receivingdetailid, x.FromPalletId })
                .ToDictionary(
                    g => g.Key,
                    g => new {
                        QuantityMoved = g.Sum(x => x.Quantitymoved),
                        WeightMoved = g.Sum(x => x.Weightmoved)
                    });

            var repalletizedTo = repalletization
                .GroupBy(x => new { x.Receivingdetailid, x.ToPalletId })
                .ToDictionary(
                    g => g.Key,
                    g => new {
                        QuantityMoved = g.Sum(x => x.Quantitymoved),
                        WeightMoved = g.Sum(x => x.Weightmoved)
                    });

            // Query products
            var query = _productQueries.productwithcompany_asof(companyId, asOf);
            var totalCount = await query.CountAsync();

            // Early return if no products exist
            if (totalCount == 0)
            {
                return PaginationHelper.paginatedresponse(new List<ProductCompanyInventoryAsOfResponse>(), 0, pageNumber, pageSize);
            }

            var products = await PaginationHelper.paginateandproject<Product, ProductCompanyInventoryAsOfResponse>(
                query, pageNumber, pageSize, _mapper);

            // Process each product's receiving details
            var productsWithInventory = new List<ProductCompanyInventoryAsOfResponse>();

            foreach (var product in products)
            {
                var adjustedReceiving = new List<ProductCompanyInventoryReceivingResponse>();

                foreach (var rec in product.Receiving)
                {
                    var palletId = rec.ReceivingDetail.FirstOrDefault()?.Pallet?.Id;
                    if (palletId == null) continue;

                    dispatched.TryGetValue(rec.Id, out var dispAgg);

                    var fromKey = new { Receivingdetailid = rec.Id, FromPalletId = palletId };
                    repalletizedFrom.TryGetValue(fromKey, out var repFromAgg);

                    var toKey = new { Receivingdetailid = rec.Id, ToPalletId = palletId };
                    repalletizedTo.TryGetValue(toKey, out var repToAgg);

                    var remainingQty = rec.Quantityinapallet
                        - (dispAgg?.TotalQuantity ?? 0)
                        - (repFromAgg?.QuantityMoved ?? 0)
                        + (repToAgg?.QuantityMoved ?? 0);

                    if (remainingQty <= 0) continue;

                    adjustedReceiving.Add(new ProductCompanyInventoryReceivingResponse
                    {
                        Id = rec.Id,
                        Document = rec.Document,
                        Requestor = rec.Requestor,
                        Approver = rec.Approver,
                        Quantityinapallet = remainingQty,
                        Totalweight = Math.Round(rec.Totalweight
                            - (dispAgg?.TotalWeight ?? 0)
                            - (repFromAgg?.WeightMoved ?? 0)
                            + (repToAgg?.WeightMoved ?? 0), 2),
                        ReceivingDetail = rec.ReceivingDetail
                    });
                }

                // Only include products with inventory
                if (adjustedReceiving.Any())
                {
                    product.Receiving = adjustedReceiving;
                    productsWithInventory.Add(product);
                }
            }

            // Return empty if no inventory records exist
            return PaginationHelper.paginatedresponse(
                productsWithInventory,
                productsWithInventory.Count,
                pageNumber,
                pageSize
            );
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
        // [HttpGet("product/receivings")]
        public async Task<Pagination<ProductBasedReceiving>> productbasedreceivings(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.productwithcompanyquery(from: from, to: to, productId: productId);
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
                Quantityinapallet = r?.Receivingdetails.Sum(r => r.Quantityinapallet) ?? 0
            }).ToList();

            return PaginationHelper.paginatedresponse(mapped, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("product/dispatchings")]
        public async Task<Pagination<ProductBasedDispatching>> productbaseddispatchings(
            int pageNumber = 1,
            int pageSize = 10,
            int? productId = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _productQueries.productswithcompanydispatching(from: from, to: to, productId: productId);
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

            return PaginationHelper.paginatedresponse(mapped, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("products/company-based")]
        public async Task<List<BasicProductResponse>> customerbasedproductsbasic(int id)
        {
            var products = await _productQueries.companybasesproductslist(id);
            return _mapper.Map<List<BasicProductResponse>>(products);
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
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product code cannot be empty", nameof(productCode));

            var product = await _productQueries.productwithreceivingdetail(productCode);
            if (product == null)
                throw new KeyNotFoundException($"Product not found: {productCode}");

            var receivedReceivings = product.Receiving?
                .Where(r => r.Received)
                .ToList() ?? new List<Receiving>();
            // Extract valid details
            var validDetails = receivedReceivings
                .SelectMany(r => r.Receivingdetails ?? Enumerable.Empty<ReceivingDetail>())
                .Where(r => !r.Fulldispatched &&
                r.Pallet?.Occupied == true)
                .ToList();

            if (!validDetails.Any())
            {
                return new ProductWithReceivingResponse
                {
                    ReceivingDetail = new List<ProductReceivingDetailResponse>(),
                    Overallweight = Math.Round(receivedReceivings.Sum(r => r.Overallweight), 2)
                };
            }
            // Bulk preload data
            var (dispatchedSums, repalletizedFromSums, repalletizedToSums) = await PreloadDependentData(validDetails);
            // Process details
            var positionIds = validDetails.Select(r => r.Positionid).Distinct().ToList();
            var positions = await _context.Palletpositions
                .Include(p => p.Coldstorage)
                .Where(p => positionIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            var responseDetails = new List<ProductReceivingDetailResponse>();

            foreach (var detail in validDetails)
            {
                // Calculate remaining stocks
                var remaining = CalculateRemainingStocks(detail, dispatchedSums, repalletizedFromSums, repalletizedToSums);

                if (remaining.Quantity <= 0) continue;
                // Safe position access
                positions.TryGetValue(detail.Positionid, out var position);

                var detailResponse = _mapper.Map<ProductReceivingDetailResponse>(detail);
                detailResponse.Quantityinapallet = remaining.Quantity;
                detailResponse.Totalweight = remaining.Weight;

                responseDetails.Add(detailResponse);
            }
            // Build final response
            var response = _mapper.Map<ProductWithReceivingResponse>(product);
            response.ReceivingDetail = responseDetails;
            response.Overallweight = Math.Round(receivedReceivings.Sum(r => r.Overallweight), 2);

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
        // Buld data loading
        private async Task<(
            Dictionary<int, (int Quantity, double Weight)>,
            Dictionary<int, (int Quantity, double Weight)>,
            Dictionary<int, (int Quantity, double Weight)>)>
            PreloadDependentData(List<ReceivingDetail> details)
        {
            var detailIds = details.Select(d => d.Id).ToList();
            var palletIds = details.Select(d => d.Palletid).Distinct().ToList();

            var dispatchedSums = await _context.Dispatchingdetails
                .Where(d => detailIds.Contains(d.Receivingdetailid))
                .GroupBy(d => d.Receivingdetailid)
                .Select(g => new
                {
                    Id = g.Key,
                    Quantity = g.Sum(d => (int?)d.Quantity) ?? 0,
                    Weight = g.Sum(d => (double?)d.Totalweight) ?? 0
                })
                .ToDictionaryAsync(x => x.Id, x => (x.Quantity, x.Weight));

            var repalletizedFromSums = await _context.Repalletizationdetails
                .Include(r => r.Repalletization)
                .Where(r => palletIds.Contains(r.Repalletization.Frompalletid) &&
                            detailIds.Contains(r.Receivingdetailid))
                .GroupBy(r => r.Receivingdetailid)
                .Select(g => new
                {
                    Id = g.Key,
                    Quantity = g.Sum(r => (int?)r.Quantitymoved) ?? 0,
                    Weight = g.Sum(r => (double?)r.Weightmoved) ?? 0
                })
                .ToDictionaryAsync(x => x.Id, x => (x.Quantity, x.Weight));

            var repalletizedToSums = await _context.Repalletizationdetails
                .Include(r => r.Repalletization)
                .Where(r => palletIds.Contains(r.Repalletization.Topalletid))
                .GroupBy(r => r.Repalletization.Topalletid)
                .Select(g => new
                {
                    PalletId = g.Key,
                    Quantity = g.Sum(r => (int?)r.Quantitymoved) ?? 0,
                    Weight = g.Sum(r => (double?)r.Weightmoved) ?? 0
                })
                .ToDictionaryAsync(x => x.PalletId, x => (x.Quantity, x.Weight));
            return (dispatchedSums, repalletizedFromSums, repalletizedToSums);
        }
        // Stock Calculation
        private (int Quantity, double Weight) CalculateRemainingStocks(
            ReceivingDetail detail,
            Dictionary<int, (int Quantity, double Weight)> dispatchedSums,
            Dictionary<int, (int Quantity, double Weight)> repalletizedFromSums,
            Dictionary<int, (int Quantity, double Weight)> repalletizedToSums)
        {
            dispatchedSums.TryGetValue(detail.Id, out var dispatched);
            repalletizedFromSums.TryGetValue(detail.Id, out var repalletizedFrom);
            repalletizedToSums.TryGetValue(detail.Palletid, out var repalletizedTo);

            var remainingQuantity = detail.Quantityinapallet
                - dispatched.Quantity
                - repalletizedFrom.Quantity
                + repalletizedTo.Quantity;

            var remainingWeight = Math.Round(
                detail.Totalweight
                - dispatched.Weight
                - repalletizedFrom.Weight
                + repalletizedTo.Weight,
                2);
            return (remainingQuantity, remainingWeight);
        }
    }
}
