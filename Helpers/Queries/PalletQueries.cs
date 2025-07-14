using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class PalletQueries
    {
        private readonly AppDbContext _context;
        public PalletQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all filtered pallets based on product id
        public async Task<List<Pallet>> productbasedoccupiedpallets(int id)
        {
            return await _context.Pallets
                .AsNoTracking()
                .Include(p => p.DispatchDetail)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving)
                .ThenInclude(r => r.Product)
                .ThenInclude(p => p.Customer)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving)
                .ThenInclude(r => r.Product)
                .ThenInclude(r => r.Category)
                .Include(p => p.Creator)
                .Where(p => p.Occupied && p.ReceivingDetail.Any(r => r.Receiving.Product.Id == id))
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
        // Query for fetching all occupied pallets with optional filter for pallet number
        public IQueryable<Pallet> occupiedpalletsquery(string? searchTerm = null)
        {
            var query = batchpalletgetquery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Active && p.Occupied && p.Palletno.ToString().Contains(searchTerm));
            }
            return query;
        }
        // Query for fetching all occupied pallets
        public async Task<List<Pallet>> occupiedpallets()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Include(p => p.DispatchDetail)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving)
                .ThenInclude(r => r.Product)
                .ThenInclude(p => p.Customer)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving)
                .ThenInclude(r => r.Product)
                .ThenInclude(r => r.Category)
                .Include(p => p.Creator)
                .OrderByDescending(p => p.Id)
                .Where(p => p.Active && p.Occupied && !p.Removed)
                .ToListAsync();
        }
        // Query for fetching active pallets with optional filter for pallet type
        public async Task<List<Pallet>> pallettypepalletslist(string searchTerm)
        {
            return await _context.Pallets
                  .AsNoTracking()
                  .Include(p => p.DispatchDetail)
                  .Include(p => p.ReceivingDetail)
                  .ThenInclude(r => r.Receiving)
                  .ThenInclude(r => r.Product)
                  .ThenInclude(p => p.Customer)
                  .Include(p => p.ReceivingDetail)
                  .ThenInclude(r => r.Receiving)
                  .ThenInclude(r => r.Product)
                  .ThenInclude(r => r.Category)
                  .Include(p => p.Creator)
                  .OrderByDescending(p => p.Id)
                  .Where(p => p.Active && !p.Occupied && !p.Removed && p.Pallettype == searchTerm)
                  .ToListAsync();
        }
        // Query for fetching all active pallets
        public async Task<List<Pallet>> activepallets()
        {
            return await _context.Pallets
                   .AsNoTracking()
                  .Include(p => p.DispatchDetail)
                  .Include(p => p.ReceivingDetail)
                  .ThenInclude(r => r.Receiving)
                  .ThenInclude(r => r.Product)
                  .ThenInclude(p => p.Customer)
                  .Include(p => p.ReceivingDetail)
                  .ThenInclude(r => r.Receiving)
                  .ThenInclude(r => r.Product)
                  .ThenInclude(r => r.Category)
                  .Include(p => p.Creator)
                   .OrderByDescending(p => p.Createdon)
                   .Where(p => p.Active && !p.Occupied && !p.Removed)
                   .ToListAsync();
        }
        // Query for fetching all pallets with optional filter for pallet number
        public IQueryable<Pallet> palletsquery(string? searchTerm = null)
        {
            var query = batchpalletgetquery();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Pallettype.Contains(searchTerm) || p.Taggingnumber.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching all active cold storages
        public async Task<List<ColdStorage>> activecoldstorages()
        {
            return await _context.Coldstorages
                    .AsNoTracking()
                    .Where(c => c.Active)
                    .ToListAsync();
        }
        // Query for fetching specific pallet for GET method
        public async Task<Pallet?> getmethodpalletid(int id)
        {
            return await _context.Pallets
                 .AsNoTracking()
                 .Include(p => p.DispatchDetail)
                 .Include(p => p.ReceivingDetail)
                 .ThenInclude(r => r.Receiving)
                 .ThenInclude(r => r.Product)
                 .ThenInclude(p => p.Customer)
                 .Include(p => p.ReceivingDetail)
                 .ThenInclude(r => r.Receiving)
                 .ThenInclude(r => r.Product)
                 .ThenInclude(r => r.Category)
                 .Include(p => p.Creator)
                 .Include(p => p.Creator)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific cold storage for GET method
        public async Task<ColdStorage> getmethodcoldstorageid(int id)
        {
            ValidateID(id);

            var cs = await _context.Coldstorages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

            return cs ??
                throw new ArgumentException("Cold storage not found.");
            
        }
        // Query for fetching specific pallet position for GET method
        public async Task<PalletPosition> getmethodpositionid(int id)
        {
            ValidateID(id);

            var position = await _context.Palletpositions
                .AsNoTracking()
                .Include(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Id == id);

            return position ??
                throw new ArgumentException("Pallet position not found.");
        }
        // Query for fetching specific repalletization for GET method
        public async Task<Repalletization?> getmethodrepalletizationid(int id)
        {
            ValidateID(id);

            var repallet = await _context.Repalletizations
                .AsNoTracking()
                .Include(r => r.RepalletizationDetail)
                .FirstOrDefaultAsync(r => r.Id == id);

            return repallet ??
                throw new ArgumentException("Repalletization not found.");
        }
        // Query for fetching pallet positions based on cs id
        public async Task<List<PalletPosition>> getpositionsbasedoncs(int id)
        {
            ValidateID(id);

            return await _context.Palletpositions
                .AsNoTracking()
                .Include(c => c.Coldstorage)
                .Where(c => c.Csid == id && !c.Hidden && !c.Removed)
                .ToListAsync();

            
        }
        // Query for fetching all pallet positions
        public async Task<List<PalletPosition>> palletpositionsquery()
        {
            return await _context.Palletpositions
                    .AsNoTracking()
                    .Include(p => p.Coldstorage)
                    .Where(p => !p.Hidden)
                    .ToListAsync();
        }
        // Query for fetching specific pallet for PATCH/PUT/DELETE methods
        public async Task<Pallet?> patchmethodpalletid(int id)
        {
            return await _context.Pallets.
                FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific pallet position for PATCH/PUT/DELETE methods
        public async Task<PalletPosition?> patchmethodpositionid(int id)
        {
            return await _context.Palletpositions.
                FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific cold storage for PATCH/PUT/DELETE methods
        public async Task<ColdStorage?> patchmethodcoldstorageid(int id)
        {
            return await _context.Coldstorages.
                FirstOrDefaultAsync(c => c.Id == id);
        }
        // Helpers
        private void ValidateID(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID provided.");
        }
        private IQueryable<Pallet> batchpalletgetquery()
        {
            return _context.Pallets
                .AsNoTracking()
                .Include(p => p.Creator)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(p => p.Pallet)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.PalletPosition)
                .ThenInclude(p => p.Coldstorage)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.DispatchingDetail)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.RepalletizationDetail)
                .Include(p => p.DispatchDetail)
                .Where(p => !p.Removed)
                .OrderByDescending(p => p.Id);
        }
        private IQueryable<Pallet> patchpalletquery()
        {
            return _context.Pallets
                .Include(p => p.Creator)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(p => p.Pallet)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.PalletPosition)
                .ThenInclude(p => p.Coldstorage)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.DispatchingDetail)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.RepalletizationDetail)
                .Include(p => p.DispatchDetail)
                .Where(p => !p.Removed);
        }
        private IQueryable<PalletPosition> batchpositiongetquery()
        {
            return _context.Palletpositions
                .AsNoTracking()
                .Include(p => p.Coldstorage)
                .Where(p => !p.Removed)
                .OrderByDescending(p => p.Id);
        }
        private IQueryable<PalletPosition> patchpositionquery()
        {
            return _context.Palletpositions
                .Include(p => p.Coldstorage)
                .Where(p => !p.Removed);
        }
    }
}
