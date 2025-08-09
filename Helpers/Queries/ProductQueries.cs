using AutoMapper;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class ProductQueries
    {
        private readonly AppDbContext _context;
        private readonly ProductValidator _validator;
        public ProductQueries(AppDbContext context, ProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching all products with optional filter for company, using present date
        public IQueryable<Product> ProductWithCompany_AsOf(int? companyId = null, DateTime? asOf = null)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving
                .Where(r => !asOf.HasValue || r.Createdon <= asOf))
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .ThenInclude(p => p.Creator)
                .Include(p => p.Receiving
                .Where(r => !asOf.HasValue || r.Createdon <= asOf))
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .ThenInclude(p => p.Coldstorage)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (companyId.HasValue)
            {
                query = query.Where(p => p.Customer.Id == companyId);
            }

            return query;
        }
        // Query for fetching all products with dispatching with optional filter for company, as of, from date received, and to date received
        public IQueryable<Product> ProductsWithCompanyDispatching(string? company = null, DateTime? from = null, DateTime? to = null, int? productId = null)
        {
            var query = _context.Products
                .AsNoTracking()
                .Where(p => !p.Removed && p.Dispatching.Any(d => d.Dispatched))
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Dispatching)
                .ThenInclude(d => d.Dispatchingdetails)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (productId.HasValue)
            {
                query = query.Where(p => p.Id == productId);
            }

            if (!string.IsNullOrWhiteSpace(company))
            {
                query = query.Where(p => p.Customer.Companyname == company);
            }

            if (from.HasValue)
            {
                var fromDate = from.Value.Date;
                query = query.Where(p => p.Receiving.Any
                (r => r.Datereceived >= fromDate) || p.Dispatching.Any(d => d.Dispatchdate >= fromDate));
            }

            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                query = query.Where(p => p.Receiving.Any
                (d => d.Datereceived < toDate) || p.Dispatching.Any(d => d.Dispatchdate < toDate));
            }

            return query;
        }
        // Query for fetching products with optional filter for company, as of, from date received, and to date received 
        public IQueryable<Product> ProductWithCompanyQuery(string? company = null, DateTime? from = null, DateTime? to = null, int? productId = null)
        {
            var query = _context.Products
                .AsNoTracking()
                .Where(p => !p.Removed)
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Document)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Requestor)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Approver)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.DispatchingDetail)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .Include(p => p.Dispatching)
                .ThenInclude(d => d.Dispatchingdetails)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (productId.HasValue)
            {
                query = query.Where(p => p.Id == productId);
            }

            if (!string.IsNullOrWhiteSpace(company))
            {
                query = query.Where(p => p.Customer.Companyname == company);
            }

            if (from.HasValue)
            {
                var fromDate = from.Value.Date;
                query = query.Where(p => p.Receiving.Any
                (r => r.Datereceived >= fromDate) || p.Dispatching.Any(d => d.Dispatchdate >= fromDate));
            }

            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                query = query.Where(p => p.Receiving.Any
                (d => d.Datereceived < toDate) || p.Dispatching.Any(d => d.Dispatchdate < toDate));
            }

            return query;
        }
        // Query for fetching all list of products based on company id
        public async Task<List<Product>> CompanyBasedProductsList(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => !p.Removed && p.Customer.Id == id)
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Document)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Requestor)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Approver)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.DispatchingDetail)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .Include(p => p.Dispatching)
                .ThenInclude(d => d.Dispatchingdetails)
                .Include(p => p.Dispatching)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
        // Query for fetching all products with related customers
        public async Task<List<Product>?> ProductListQuery(int id)
        {
            return await _context.Products
                  .AsNoTracking()
                  .Where(p => p.Category.Id == id)
                  .Include(p => p.Category)
                  .Include(p => p.Customer)
                  .OrderByDescending(p => p.Id)
                  .ToListAsync();
        }
        // Query for fetching all products with optional filter for product code
        public IQueryable<Product> ProductsQuery(string? searchTerm = null)
        {
            var query = _context.Products
                .AsNoTracking()
                .Where(p => !p.Removed)
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Document)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Requestor)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Approver)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.DispatchingDetail)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .Include(p => p.Dispatching)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Productcode.Contains(searchTerm) || p.Productname.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching specific product by product code
        public async Task<Product?> ProductsByCode(string? productCode = null)
        {
            return await _context.Products
                   .AsNoTracking()
                   .Include(p => p.Category)
                   .Include(p => p.Customer)
                   .FirstOrDefaultAsync(p => p.Productcode == productCode);
        }
        // Query for fetching all products with related receiving details
        public async Task<List<Product>> ProductWithReceivings()
        {
            return await _context.Products
                   .AsNoTracking()
                   .Where(p => p.Receiving.Any(r => r.Received) &&
                   p.Receiving.Any(r => r.Receivingdetails.Any(r => !r.Fulldispatched)))
                   .Include(p => p.Category)
                   .Include(p => p.Receiving)
                   .ThenInclude(r => r.Receivingdetails)
                   .ToListAsync();
        }
        // Query for fetching all receiving details based on product code
        public async Task<Product?> ProductWithReceivingDetail(string productCode)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving
                .Where(r => r.Received))
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .ThenInclude(p => p.Creator)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.DispatchingDetail)
                .ThenInclude(d => d.Dispatching)
                .Include(r => r.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Outgoingrepalletization)
                .Include(r => r.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Incomingrepalletization)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .ThenInclude(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Productcode == productCode);
        }
        // Query for fetching specific product for GET method
        public async Task<Product?> GetProductId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificProduct(id);

            return await _context.Products
                  .AsNoTracking()
                  .Include(p => p.Category)
                  .Include(p => p.Customer)
                  .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific product for PATCH/PUT/DELETE methods
        public async Task<Product?> PatchProductId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificProduct(id);

            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Document)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Requestor)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Approver)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.DispatchingDetail)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .Include(p => p.Dispatching)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
