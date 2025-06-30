using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class DispatchingQueries
    {
        private readonly AppDbContext _context;
        public DispatchingQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all dispatchings in list
        public async Task<List<Dispatching>> dispatchingslist()
        {
            return await _context.Dispatchings
                .Include(d => d.Document)
                .Include(d => d.Product)
                .ThenInclude(p => p.Customer)
                .Include(d => d.Requestor)
                .Include(d => d.Approver)
                .Include(d => d.Dispatchingdetails)
                .ThenInclude(d => d.Pallet)
                .Include(d => d.Dispatchingdetails)
                .ThenInclude(d => d.PalletPosition)
                .Where(d => !d.Removed)
                .OrderByDescending(d => d.Id)
                .ToListAsync();
        }
        // Query for fetching all pending dispatching request with optional filter for category
        public IQueryable<Dispatching> pendingdispatchingquery(string? searchTerm = null)
        {
            var query = _context.Dispatchings
                   .AsNoTracking()
                   .Include(d => d.Document)
                   .Include(d => d.Product)
                   .ThenInclude(p => p.Customer)
                   .Include(d => d.Dispatchingdetails)
                   .OrderByDescending(d => d.Createdon)
                   .Where(d => d.Pending && !d.Removed)
                   .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => d.Product.Category == searchTerm);
            }
            return query;
        }
        // Query for fetching all dispatched with optional filter for category
        public IQueryable<Dispatching> dispatchedquery(string? searchTerm = null)
        {
            var query = _context.Dispatchings
                   .AsNoTracking()
                   .Include(d => d.Document)
                   .Include(d => d.Product)
                   .ThenInclude(p => p.Customer)
                   .Include(d => d.Dispatchingdetails)
                   .Where(d => !d.Removed)
                   .OrderByDescending(d => d.Createdon)
                   .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => d.Product.Category == searchTerm);
            }
            return query;
        }
        // Query for fetching specific dispatching based on document ID
        public async Task<Dispatching?> getdispatchingbasedondocumentid(int documentId)
        {
            return await _context.Dispatchings
                  .FirstOrDefaultAsync(d => d.Documentid == documentId);
        }
        // Query for fetching specific dispatching for GET methods
        public async Task<Dispatching?> getmethoddispatchingid(int id)
        {
            return await _context.Dispatchings
                  .Include(d => d.Document)
                  .Include(d => d.Dispatchingdetails)
                  .ThenInclude(d => d.Pallet)
                  .Include(d => d.Dispatchingdetails)
                  .ThenInclude(d => d.PalletPosition)
                  .ThenInclude(p => p.Coldstorage)
                  .Include(d => d.Product)
                  .ThenInclude(p => p.Customer)
                  .Include(d => d.Requestor)
                  .Include(d => d.Approver)
                  .AsNoTracking()
                  .FirstOrDefaultAsync(d => d.Id == id);
        }
        // Query for fetching specific dispatching for PATCH/PUT/DELETE methods
        public async Task<Dispatching?> patchmethoddispatchingid(int id)
        {
            return await _context.Dispatchings
                   .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
