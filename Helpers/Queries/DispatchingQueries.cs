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
        public async Task<List<Dispatching>> dispatchingslist(int? id = null)
        {
            return await dispatchedquery(id).ToListAsync();
        }
        // Query for fetching all pending dispatching request with optional filter for category
        public IQueryable<Dispatching> pendingdispatchingquery(int? id)
        {

            var query = batchgetquery();

            if (id.HasValue)
            {
                query = query.Where(d => d.Pending && d.Product.Category.Id == id);
            }

            return query;
        }
        // Query for fetching all dispatched with optional filter for category
        public IQueryable<Dispatching> dispatchedquery(int? id)
        {
            var query = batchgetquery();

            if (id.HasValue)
            {
                query = query.Where(d => d.Product.Category.Id == id);
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
            return await batchgetquery()
                  .FirstOrDefaultAsync(d => d.Id == id);
        }
        // Query for fetching specific dispatching for PATCH/PUT/DELETE methods
        public async Task<Dispatching?> patchmethoddispatchingid(int id)
        {
            return await patchquery()
                   .FirstOrDefaultAsync(d => d.Id == id);
        }
        // Helpers
        private IQueryable<Dispatching> batchgetquery()
        {
            return _context.Dispatchings
                .AsNoTracking()
                .Include(d => d.Product)
                .ThenInclude(p => p.Category)
                .Include(d => d.Product)
                .ThenInclude(p => p.Customer)
                .Include(d => d.Document)
                .Include(d => d.Requestor)
                .Include(d => d.Approver)
                .Include(d => d.Dispatchingdetails)
                .ThenInclude(d => d.Pallet)
                .Include(d => d.Dispatchingdetails)
                .ThenInclude(d => d.PalletPosition)
                .ThenInclude(p => p.Coldstorage)
                .Where(d => !d.Removed)
                .OrderByDescending(d => d.Id);
        }
        private IQueryable<Dispatching> patchquery()
        {
            return _context.Dispatchings
               .Include(d => d.Product)
               .Include(d => d.Document)
               .Include(d => d.Requestor)
               .Include(d => d.Approver)
               .Include(d => d.Dispatchingdetails)
               .Where(d => !d.Removed);
        }
    }
}
