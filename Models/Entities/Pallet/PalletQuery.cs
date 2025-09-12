using Microsoft.EntityFrameworkCore;

namespace csms_backend.Models.Entities
{
    public class PalletQuery
    {
        private readonly Context _context;
        public PalletQuery(Context context)
        {
            _context = context;
        }

        public IQueryable<Pallet> PaginatedPallets(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.Pallet
                    .AsNoTracking()
                    .Where(p => p.PalletType.Contains(searchTerm)
                    || p.PalletType.Contains(searchTerm))
                    .Include(p => p.Creator)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Pallet
                    .AsNoTracking()
                    .Include(p => p.Creator)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();
                return query;
            }
        }

        public async Task<List<Pallet>> ListedPallets(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Pallet
                   .AsNoTracking()
                   .Where(p => p.PalletType.Contains(searchTerm)
                   || p.PalletType.Contains(searchTerm))
                   .Include(p => p.Creator)
                   .OrderByDescending(p => p.Id)
                   .ToListAsync();
            }
            else
            {
                return await _context.Pallet
                    .AsNoTracking()
                    .Include(p => p.Creator)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
        }

        public async Task<Pallet?> GetPalletById(int id)
        {
            return await _context.Pallet
                .AsNoTracking()
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pallet?> PatchPalletById(int id)
        {
            return await _context.Pallet
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
    public class PalletPositionQuery
    {
        private readonly Context _context;
        public PalletPositionQuery(Context context)
        {
            _context = context;
        }

        public IQueryable<PalletPosition> PaginatedPalletPositions(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.PalletPosition
                    .AsNoTracking()
                    .Where(p => p.Wing.Contains(searchTerm)
                    || p.Floor.Contains(searchTerm)
                    || p.Column.Contains(searchTerm)
                    || p.Side.Contains(searchTerm))
                    .Include(p => p.ColdStorage)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.PalletPosition
                    .AsNoTracking()
                    .Include(p => p.ColdStorage)
                    .OrderByDescending(p => p.Id)
                    .AsQueryable();

                return query;
            }
        }

        public async Task<List<PalletPosition>> ListedPalletPositions(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.PalletPosition
                    .AsNoTracking()
                    .Where(p => p.Wing.Contains(searchTerm)
                    || p.Floor.Contains(searchTerm)
                    || p.Column.Contains(searchTerm)
                    || p.Side.Contains(searchTerm))
                    .Include(p => p.ColdStorage)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.PalletPosition
                    .AsNoTracking()
                    .Include(p => p.ColdStorage)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
        }

        public async Task<PalletPosition?> GetPalletPositionById(int id)
        {
            return await _context.PalletPosition
                    .AsNoTracking()
                    .Include(p => p.ColdStorage)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PalletPosition?> PatchPalletPositionById(int id)
        {
            return await _context.PalletPosition
                    .Include(p => p.ColdStorage)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
    public class ColdStorageQuery
    {
        private readonly Context _context;
        public ColdStorageQuery(Context context)
        {
            _context = context;
        }

        public IQueryable<ColdStorage> PaginatedColdStorages(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.ColdStorage
                    .AsNoTracking()
                    .Where(c => c.Number.Contains(searchTerm))
                    .OrderByDescending(c => c.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.ColdStorage
                    .AsNoTracking()
                    .OrderByDescending(c => c.Id)
                    .AsQueryable();

                return query;
            }
        }

        public async Task<List<ColdStorage>> ListedColdStorages(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.ColdStorage
                    .AsNoTracking()
                    .Where(c => c.Number.Contains(searchTerm))
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.ColdStorage
                    .AsNoTracking()
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }

        public async Task<ColdStorage?> GetColdStorageById(int id)
        {
            return await _context.ColdStorage
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ColdStorage?> PatchColdStorageById(int id)
        {
            return await _context.ColdStorage
                    .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
