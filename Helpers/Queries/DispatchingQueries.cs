using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class DispatchingQueries
    {
        private readonly AppDbContext _context;
        private readonly DispatchingValidator _validator;
        public DispatchingQueries(AppDbContext context, DispatchingValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching all dispatchings in list
        public async Task<List<Dispatching>> DispatchingsList(int? id = null)
        {
            return await _context.Dispatchings
                .AsNoTracking()
                .Where(d => !d.Removed)
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
                .OrderByDescending(d => d.Id)
                .ToListAsync();
        }
        // Query for fetching all pending dispatching request with optional filter for category
        public IQueryable<Dispatching> PendingDispatchingQuery(int? id)
        {

            var query = _context.Dispatchings
                .AsNoTracking()
                .Where(d => !d.Removed && d.Pending)
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
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(d => d.Product.Category.Id == id);
            }

            return query;
        }
        // Query for fetching all dispatched with optional filter for category
        public IQueryable<Dispatching> DispatchedQuery(int? id, string? documentNumber)
        {
            var query = _context.Dispatchings
                .AsNoTracking()
                .Where(d => !d.Removed)
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
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(d => d.Product.Category.Id == id);
            }

            if (!string.IsNullOrWhiteSpace(documentNumber))
            {
                query = query.Where(d => d.Document.Documentno == documentNumber);
            }
            return query;
        }
        // Query for fetching specific dispatching based on document ID
        public async Task<Dispatching?> GetDispatchingBasedOnDocumentId(int documentId)
        {
            return await _context.Dispatchings
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
                 .FirstOrDefaultAsync(d => d.Documentid == documentId);
        }
        // Query for fetching specific dispatching for GET methods
        public async Task<Dispatching?> GetDispatchingId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificDispatching(id);

            return await _context.Dispatchings
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
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        // Query for fetching specific dispatching for PATCH/PUT/DELETE methods
        public async Task<Dispatching?> PatchDispatchingId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificDispatching(id);

            return await _context.Dispatchings
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
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
