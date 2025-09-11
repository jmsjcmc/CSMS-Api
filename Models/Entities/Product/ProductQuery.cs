using Microsoft.EntityFrameworkCore;

namespace csms_backend.Models.Entities
{
    public class ProductQuery
    {
        private readonly Context _context;
        public ProductQuery(Context context)
        {
            _context = context;
        }
        public IQueryable<Product> PaginatedProducts(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.Product
                    .AsNoTracking()
                    .Where(p => p.Name.Contains(searchTerm)
                    || p.Code.Contains(searchTerm)
                    || p.Variant.Contains(searchTerm)
                    || p.Sku.Contains(searchTerm)
                    || p.Packaging.Contains(searchTerm))
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Product
                    .AsNoTracking()
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();

                return query;
            }
        }

        public async Task<List<Product>> ListedProducts(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Product
                    .AsNoTracking()
                    .Where(p => p.Name.Contains(searchTerm)
                    || p.Code.Contains(searchTerm)
                    || p.Variant.Contains(searchTerm)
                    || p.Sku.Contains(searchTerm)
                    || p.Packaging.Contains(searchTerm))
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Product
                    .AsNoTracking()
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Product
                    .AsNoTracking()
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> PatchProductById(int id)
        {
            return await _context.Product
                    .Include(p => p.Company)
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
