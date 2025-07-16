using AutoMapper;
using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class ProductQueries
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductQueries(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // Query for fetching all products with optional filter for company, using present date
        public IQueryable<Product> productwithcompany_asof(string? company = null)
        {
            var query = batchgetquery();

            if (!string.IsNullOrWhiteSpace(company))
            {
                query = query.Where(p => p.Receiving.Any()
                && p.Receiving.Any(r => r.Expirationdate >= TimeHelper.GetPhilippineStandardTime().Date) &&
                !p.Dispatching.Any(d => !d.Dispatched));
            }
            return query;
        }
        // Query for fetching products with optional filter for company, as of, from date received, and to date received 
        public IQueryable<Product> productwithcompanyquery( string? company = null, DateTime? from = null, DateTime? to = null, int? productId = null)
        {
            var query = batchgetquery();

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
                query = query.Where(p => p.Receiving.Any
                (r => r.Datereceived >= from.Value) && p.Dispatching.Any(d => d.Dispatchdate >= from.Value));
            }

            if (to.HasValue)
            {
                query = query.Where(p => p.Dispatching.Any
                (d => d.Dispatchdate <= to.Value) && p.Dispatching.Any(d => d.Dispatchdate <= to.Value));
            }

            return query;
        }

        public async Task<List<Product>> companybasesproductslist(int id)
        {
            return await _context.Products
                .AsNoTracking()
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
                .ThenInclude(r => r.RepalletizationDetail)
                .Include(p => p.Dispatching)
                .Where(p => !p.Removed && p.Customer.Id == id)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
        // Query for fetching all products with related customers
        public async Task<List<Product>?> productlistquery(int id)
        {
            return await _context.Products
                  .AsNoTracking()
                  .Include(p => p.Category)
                  .Include(p => p.Customer)
                  .Where(p => p.Category.Id == id)
                  .OrderByDescending(p => p.Id)
                  .ToListAsync();
        }
        // Query for fetching all products with optional filter for product code
        public IQueryable<Product> productsquery(string? searchTerm = null)
        {
            var query = batchgetquery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Productcode.Contains(searchTerm) || p.Productname.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching specific product by product code
        public async Task<Product?> productsbycode(string? productCode = null)
        {
            return await _context.Products
                   .AsNoTracking()
                   .Include(p => p.Category)
                   .Include(p => p.Customer)
                   .FirstOrDefaultAsync(p => p.Productcode == productCode);
        }
        // Query for fetching all products with related receiving details
        public async Task<List<Product>> productwithreceivings()
        {
            return await _context.Products
                   .AsNoTracking()
                   .Include(p => p.Category)
                   .Include(p => p.Receiving)
                   .ThenInclude(r => r.Receivingdetails)
                   .Where(p => p.Receiving.Any(r => r.Received) && 
                   p.Receiving.Any(r => r.Receivingdetails.Any(r => !r.Fulldispatched)))
                   .ToListAsync();
        }
        // Query for fetching all receiving details based on product code
        public async Task<Product?> productwithreceivingdetail(string productCode)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .ThenInclude(p => p.Creator)
                .Include(p => p.Receiving)
                .ThenInclude(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .ThenInclude(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Productcode == productCode);
        }
        // Query for fetching specific product for GET method
        public async Task<Product?> getmethodproductid(int id)
        {
            return await _context.Products
                  .AsNoTracking()
                  .Include(p => p.Category)
                  .Include(p => p.Customer)
                  .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific product for PATCH/PUT/DELETE methods
        public async Task<Product?> patchmethodproductid(int id)
        {
            return await patchquery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Helpers
        private IQueryable<Product> batchgetquery()
        {
            return _context.Products
                .AsNoTracking()
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
                .ThenInclude(r => r.RepalletizationDetail)
                .Include(p => p.Dispatching)
                .Where(p => !p.Removed)
                .OrderByDescending(p => p.Id);
        }
        private IQueryable<Product> patchquery()
        {
            return _context.Products
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
                .ThenInclude(r => r.RepalletizationDetail)
                .Include(p => p.Dispatching)
                .Where(p => !p.Removed);
        }
    }
}
