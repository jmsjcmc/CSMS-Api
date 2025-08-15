using CSMapi.Models;
using CSMapi.Validators;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CSMapi.Helpers.Queries
{
    public class PalletQueries
    {
        private readonly AppDbContext _context;
        private readonly PalletValidator _validator;
        public PalletQueries(AppDbContext context, PalletValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching repalletization draft display
        public IQueryable<Repalletization> PaginatedRepalletizationDraftQuery(int status)
        {
            var query = _context.Repalletizations
                .AsNoTracking()
                .Where(r => r.Status == status)
                .Include(r => r.Fromreceivingdetail.Pallet)
                .Include(r => r.Fromreceivingdetail.Receiving.Product)
                .Include(r => r.Fromreceivingdetail.PalletPosition.Coldstorage)
                .Include(r => r.Fromreceivingdetail.DispatchingDetail)
                .ThenInclude(d => d.Dispatching)
                .Include(r => r.Toreceivingdetail.Pallet)
                .Include(r => r.Toreceivingdetail.Receiving.Product)
                .Include(r => r.Toreceivingdetail.PalletPosition.Coldstorage)
                .Include(r => r.Toreceivingdetail.DispatchingDetail)
                .ThenInclude(d => d.Dispatching)
                .OrderByDescending(r => r.Id)
                .AsQueryable();

            return query;
        }
        // Query for fetching all filtered pallets based on product id
        public async Task<List<Pallet>> ProductBasedOccupiedPallets(int id)
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => p.Occupied && p.ReceivingDetail.Any(r => r.Receiving.Product.Id == id))
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
                .ToListAsync();
        }
        // Query for fetching all occupied pallets with optional filter for pallet number
        public IQueryable<Pallet> OccupiedPalletsQuery(string? searchTerm = null)
        {
            var query = _context.Pallets
                .AsNoTracking()
                .Where(p => p.Occupied)
                .Include(p => p.ReceivingDetail)
                .ThenInclude(r => r.Receiving.Product)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Active && p.Occupied && p.Palletno.ToString().Contains(searchTerm));
            }
            return query;
        }
        // Query for fetching all occupied pallets
        public async Task<List<Pallet>> OccupiedPallets()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => p.Active && p.Occupied && !p.Removed)
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
                .ToListAsync();
        }

        // Query for fetching active pallets with optional filter for pallet type
        public async Task<List<Pallet>> PalletTypePalletsList(string searchTerm)
        {
            return await _context.Pallets
                  .AsNoTracking()
                  .Where(p => p.Active && !p.Occupied && !p.Removed && p.Pallettype == searchTerm)
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
                  .ToListAsync();
        }
        // Query for fetching all active pallets
        public async Task<List<Pallet>> ActivePallets()
        {
            return await _context.Pallets
                   .AsNoTracking()
                   .Where(p => p.Active && !p.Occupied && !p.Removed)
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
                  .ToListAsync();
        }
        // Query for fetching all pallets with optional filter for pallet number
        public IQueryable<Pallet> PalletsQuery(string? searchTerm = null)
        {
            var query = _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed)
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
                .Include(p => p.DispatchDetail)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Pallettype.Contains(searchTerm) || p.Taggingnumber.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching all active cold storages
        public async Task<List<ColdStorage>> ActiveColdStorages()
        {
            return await _context.Coldstorages
                    .AsNoTracking()
                    .Where(c => c.Active)
                    .ToListAsync();
        }
        // Query for fetching specific pallet for GET method
        public async Task<Pallet?> GetPalletId(int id)
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
        public async Task<ColdStorage?> GetColdStorageId(int id)
        {
            // Validate id if it exist
            if (!await _context.Coldstorages.AnyAsync(c => c.Id == id))
            {
                throw new ArgumentException($"Cold Storage ID {id} not found.");
            }

            return await _context.Coldstorages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific pallet position for GET method
        public async Task<PalletPosition?> GetPositionId(int id)
        {
            // Validate id if it exist
            if (!await _context.Palletpositions.AnyAsync(p => p.Id == id))
            {
                throw new ArgumentException($"Position ID {id} not found.");
            }

            return await _context.Palletpositions
                .AsNoTracking()
                .Include(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific repalletization for GET method
        public async Task<Repalletization?> GetRepalletizationId(int id)
        {
            // Validate id if it exist
            if (!await _context.Repalletizations.AnyAsync(r => r.Id == id))
            {
                throw new ArgumentException($"Repalletization ID {id} not found.");
            }

            return await _context.Repalletizations
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching pallet positions based on cs id
        public async Task<List<PalletPosition>> GetPositionByCs(int id)
        {
            // Validate id if it exist
            if (!await _context.Palletpositions.AnyAsync(p => p.Id == id))
            {
                throw new ArgumentException($"Position ID {id} not found.");
            }

            return await _context.Palletpositions
                .AsNoTracking()
                .Include(c => c.Coldstorage)
                .Where(c => c.Csid == id && !c.Hidden && !c.Removed)
                .ToListAsync();
        }
        // Query for fetching all pallet positions
        public async Task<List<PalletPosition>> PalletPositionsQuery()
        {
            return await _context.Palletpositions
                    .AsNoTracking()
                    .Include(p => p.Coldstorage)
                    .Where(p => !p.Hidden)
                    .ToListAsync();
        }
        // Query for fetching specific pallet for PATCH/PUT/DELETE methods
        public async Task<Pallet?> PatchPalletId(int id)
        {
            // Validate id if it exist
            if (!await _context.Pallets.AnyAsync(p => p.Id == id))
            {
                throw new ArgumentException($"Pallet ID {id} not foud.");
            }

            return await _context.Pallets
                .Include(p => p.Creator)
                .Include(p => p.ReceivingDetail)
                .Include(p => p.DispatchDetail)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific pallet position for PATCH/PUT/DELETE methods
        public async Task<PalletPosition?> PatchPositionId(int id)
        {
            // Validate id if it exist
            if (!await _context.Palletpositions.AnyAsync(p => p.Id == id))
            {
                throw new ArgumentException($"Position ID {id} not found.");
            }

            return await _context.Palletpositions
                .Include(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific cold storage for PATCH/PUT/DELETE methods
        public async Task<ColdStorage?> PatchCsId(int id)
        {
            // Validate id if it exist
            if (!await _context.Coldstorages.AnyAsync(c => c.Id == id))
            {
                throw new ArgumentException($"Cold Storage ID {id} not found.");
            }

            return await _context.Coldstorages.
                FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific repalletizaiton draft for PATCH/PUT/DELETE methods 
        public async Task<Repalletization?> PatchRepalletizationsDraftId(int id)
        {
            // Validate id if it exist
            if (!await _context.Repalletizations.AnyAsync(r => r.Id == id))
            {
                throw new ArgumentException($"Repalletization ID {id} not found.");
            }

            return await _context.Repalletizations
                .Include(r => r.Fromreceivingdetail.Pallet)
                .Include(r => r.Fromreceivingdetail.PalletPosition.Coldstorage)
                .Include(r => r.Fromreceivingdetail.DispatchingDetail)
                .ThenInclude(d => d.Dispatching)
                .Include(r => r.Toreceivingdetail.Pallet)
                .Include(r => r.Toreceivingdetail.PalletPosition.Coldstorage)
                .Include(r => r.Toreceivingdetail.DispatchingDetail)
                .ThenInclude(d => d.Dispatching)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // 
        //public async Task<Repalletization?> GetRepalletizationId(int id)
        //{
        //    if (!await _context.Repalletizations.AnyAsync(r => r.Id == id))
        //        throw new ArgumentException($"Repalletization ID {id} not found.");

        //    return await _context.Repalletizations
        //        .AsNoTracking()
        //        .Include(r => r.Fromreceivingdetail.Pallet)
        //        .Include(r => r.Fromreceivingdetail.PalletPosition.Coldstorage)
        //        .Include(r => r.Fromreceivingdetail.DispatchingDetail)
        //        .ThenInclude(d => d.Dispatching)
        //        .Include(r => r.Toreceivingdetail.Pallet)
        //        .Include(r => r.Toreceivingdetail.PalletPosition.Coldstorage)
        //        .Include(r => r.Toreceivingdetail.DispatchingDetail)
        //        .ThenInclude(d => d.Dispatching)
        //        .FirstOrDefaultAsync(r => r.Id == id);
        //}
    }
}
