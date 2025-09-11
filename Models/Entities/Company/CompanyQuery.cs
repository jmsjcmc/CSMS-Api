using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace csms_backend.Models
{
    public class CompanyQuery
    {
        private readonly Context _context;
        public CompanyQuery(Context context)
        {
            _context = context;
        }

        public IQueryable<Company> PaginatedCompanies(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.Company
                    .AsNoTracking()
                    .Where(c => c.Name.Contains(searchTerm))
                    .Include(c => c.Representative)
                    .OrderByDescending(c => c.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Company
                    .AsNoTracking()
                    .Include(c => c.Representative)
                    .OrderByDescending(c => c.Id)
                    .AsQueryable();

                return query;
            }
        }

        public async Task<List<Company>> ListedCompanies(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Company
                    .AsNoTracking()
                    .Where(c => c.Name.Contains(searchTerm))
                    .Include(c => c.Representative)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Company
                    .AsNoTracking()
                    .Include(c => c.Representative)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }

        public async Task<Company?> GetCompanyById(int id)
        {
            return await _context.Company
                .AsNoTracking()
                .Include(c => c.Representative)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company?> PatchCompanyById(int id)
        {
            return await _context.Company
                .Include(c => c.Representative)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
    public class RepresentativeQuery
    {
        private readonly Context _context;
        public RepresentativeQuery(Context context)
        {
            _context = context;
        }

        public async Task<List<Representative>> ListedRepresentatives(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Representative
                    .AsNoTracking()
                    .Where(r => r.FirstName.Contains(searchTerm)
                    || r.LastName.Contains(searchTerm)
                    || r.Position.Contains(searchTerm))
                    .Include(r => r.Company)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Representative
                    .AsNoTracking()
                    .Include(r => r.Company)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
        }

        public async Task<Representative?> GetRepresentativeById(int id)
        {
            return await _context.Representative
                .AsNoTracking()
                .Include(r => r.Company)
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Representative?> PatchRepresentativeById(int id)
        {
            return await _context.Representative
                .Include(r => r.Company)
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
