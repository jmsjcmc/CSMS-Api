using AutoMapper;
using AutoMapper.QueryableExtensions;
using csms_backend.Models;
using csms_backend.Models.Entities;
using csms_backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace csms_backend.Controllers
{
    public class PalletService : BaseService, PalletInterface
    {
        private readonly PalletQuery _palletQuery;
        public PalletService(Context context, IMapper mapper, PalletQuery palletQuery) : base(context, mapper)
        {
            _palletQuery = palletQuery;
        }

        public async Task<Pagination<PalletResponse>> PaginatedPallets(
            int pageNumber,
            int pageSize,
            string? searchTerm
        )
        {
            var query = _palletQuery.PaginatedPallets(searchTerm);
            return await PaginationHelper.PaginateAndMap<Pallet, PalletResponse>(query, pageNumber, pageSize, _mapper);
            
        }

        public async Task<List<PalletResponse>> ListedPallets(string? searchTerm)
        {
            var pallets = await _palletQuery.ListedPallets(searchTerm);

            return _mapper.Map<List<PalletResponse>>(pallets);
        }

        public async Task<PalletResponse> GetPalletById(int id)
        {
            var pallet = await _palletQuery.GetPalletById(id);

            if (pallet == null)
            {
                throw new Exception($"Pallet with ID {id} not found.");
            }
            else
            {
                return _mapper.Map<PalletResponse>(pallet);
            }
        }

        public async Task<PalletResponse> CreatePallet(PalletRequest request)
        {
            var pallet = _mapper.Map<Pallet>(request);
            pallet.Status = Status.Active;
            pallet.DateCreated = DateTimeHelper.GetPhilippineTime();

            await _context.Pallet.AddAsync(pallet);
            await _context.SaveChangesAsync();

            return _mapper.Map<PalletResponse>(pallet);
        }

        public async Task<PalletResponse> UpdatePallet(PalletRequest request, int id)
        {
            var pallet = await _palletQuery.PatchPalletById(id);
            if (pallet == null)
            {
                throw new Exception($"Pallet with ID {id} not found.");
            }
            else
            {
                _mapper.Map(request, id);
                pallet.DateUpdated = DateTimeHelper.GetPhilippineTime();

                await _context.SaveChangesAsync();

                var updatedPallet = await _context.Pallet
                .Where(p => p.Id == pallet.Id)
                .ProjectTo<PalletResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

                if (updatedPallet == null)
                {
                    throw new Exception("No pallet response");
                }
                else
                {
                    return updatedPallet;
                }
            }
        }

        public async Task<PalletResponse> ToggleStatus(int id)
        {
            var pallet = await _palletQuery.PatchPalletById(id);
            if (pallet == null)
            {
                throw new Exception($"Pallet with ID {id} not found.");
            }
            else
            {
                pallet.Status = pallet.Status == Status.Active
                ? Status.Inactive
                : Status.Active;
                pallet.DateUpdated = DateTimeHelper.GetPhilippineTime();

                await _context.SaveChangesAsync();
                return _mapper.Map<PalletResponse>(pallet);
            }
        }

        public async Task<PalletResponse> DeletePallet(int id)
        {
            var pallet = await _palletQuery.PatchPalletById(id);
            if (pallet == null)
            {
                throw new Exception($"Pallet with ID {id} not found.");
            }
            else
            {
                _context.Pallet.Remove(pallet);
                await _context.SaveChangesAsync();

                return _mapper.Map<PalletResponse>(pallet);
            }
        }
    }
}
