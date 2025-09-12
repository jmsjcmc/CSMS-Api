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
    public class CategoryQuery
    {
        private readonly Context _context;
        public CategoryQuery(Context context)
        {
            _context = context;
        }

        public IQueryable<Category> PaginatedCategories(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.Category
                    .AsNoTracking()
                    .Where(c => c.Name.Contains(searchTerm))
                    .Include(c => c.Product)
                    .OrderByDescending(c => c.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Category
                    .AsNoTracking()
                    .Include(c => c.Product)
                    .OrderByDescending(c => c.Id)
                    .AsQueryable();

                return query;
            }
        }

        public async Task<List<Category>> ListedCategories(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Category
                    .AsNoTracking()
                    .Where(c => c.Name.Contains(searchTerm))
                    .Include(c => c.Product)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            } else
            {
                return await _context.Category
                    .AsNoTracking()
                    .Include(c => c.Product)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Category
                .AsNoTracking()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> PatchCategoryById(int id)
        {
            return await _context.Category
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
