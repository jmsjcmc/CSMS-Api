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

        public PalletService(Context context, IMapper mapper, PalletQuery palletQuery)
            : base(context, mapper)
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
            return await PaginationHelper.PaginateAndMap<Pallet, PalletResponse>(
                query,
                pageNumber,
                pageSize,
                _mapper
            );
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

                var updatedPallet = await _context
                    .Pallet.Where(p => p.Id == pallet.Id)
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
                pallet.Status = pallet.Status == Status.Active ? Status.Inactive : Status.Active;
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

    public class PalletPositionService : BaseService, PalletPositionInterface
    {
        private readonly PalletPositionQuery _palletPositionQuery;

        public PalletPositionService(
            Context context,
            IMapper mapper,
            PalletPositionQuery palletPositionQuery
        )
            : base(context, mapper)
        {
            _palletPositionQuery = palletPositionQuery;
        }

        public async Task<Pagination<PalletPositionResponse>> PaginatedPalletPositions(
            int pageNumber,
            int pageSize,
            string? searchTerm
        )
        {
            var query = _palletPositionQuery.PaginatedPalletPositions(searchTerm);
            return await PaginationHelper.PaginateAndMap<PalletPosition, PalletPositionResponse>(
                query,
                pageNumber,
                pageSize,
                _mapper
            );
        }

        public async Task<List<PalletPositionResponse>> ListedPalletPositions(string? searchTerm)
        {
            var positions = await _palletPositionQuery.ListedPalletPositions(searchTerm);
            return _mapper.Map<List<PalletPositionResponse>>(positions);
        }

        public async Task<PalletPositionResponse> GetPalletPositionById(int id)
        {
            var position = await _palletPositionQuery.GetPalletPositionById(id);
            if (position == null)
            {
                throw new Exception($"Pallet Position with ID {id} not found.");
            }
            else
            {
                return _mapper.Map<PalletPositionResponse>(position);
            }
        }

        public async Task<PalletPositionResponse> CreatePalletPosition(
            PalletPositionRequest request
        )
        {
            var position = _mapper.Map<PalletPosition>(request);
            position.Status = Status.Active;

            await _context.PalletPosition.AddAsync(position);
            await _context.SaveChangesAsync();

            return _mapper.Map<PalletPositionResponse>(position);
        }

        public async Task<PalletPositionResponse> UpdatePalletPosition(
            PalletPositionRequest request,
            int id
        )
        {
            var position = await _palletPositionQuery.PatchPalletPositionById(id);
            if (position == null)
            {
                throw new Exception($"Pallet Position with ID {id} not found.");
            }
            else
            {
                _mapper.Map(request, id);

                await _context.SaveChangesAsync();

                var updatedPosition = await _context
                    .PalletPosition.Where(pp => pp.Id == position.Id)
                    .ProjectTo<PalletPositionResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (updatedPosition == null)
                {
                    throw new Exception("No pallet position response");
                }
                else
                {
                    return updatedPosition;
                }
            }
        }

        public async Task<PalletPositionResponse> ToggleStatus(int id)
        {
            var pallet = await _palletPositionQuery.PatchPalletPositionById(id);

            if (pallet == null)
            {
                throw new Exception($"Pallet Position with ID {id} not found.");
            }
            else
            {
                pallet.Status = pallet.Status == Status.Active ? Status.Inactive : Status.Active;

                await _context.SaveChangesAsync();
                return _mapper.Map<PalletPositionResponse>(pallet);
            }
        }

        public async Task<PalletPositionResponse> DeletePalletPosition(int id)
        {
            var pallet = await _palletPositionQuery.PatchPalletPositionById(id);
            if (pallet == null)
            {
                throw new Exception($"Pallet Position with ID {id} not found.");
            }
            else
            {
                _context.Remove(pallet);
                await _context.SaveChangesAsync();

                return _mapper.Map<PalletPositionResponse>(pallet);
            }
        }
    }

    public class ColdStorageService : BaseService, ColdStorageInterface
    {
        private readonly ColdStorageQuery _coldStorageQuery;

        public ColdStorageService(
            Context context,
            IMapper mapper,
            ColdStorageQuery coldStorageQuery
        )
            : base(context, mapper)
        {
            _coldStorageQuery = coldStorageQuery;
        }

        public async Task<Pagination<ColdStorageResponse>> PaginatedColdStorages(
            int pageNumber,
            int pageSize,
            string? searchTerm
        )
        {
            var query = _coldStorageQuery.PaginatedColdStorages(searchTerm);
            return await PaginationHelper.PaginateAndMap<ColdStorage, ColdStorageResponse>(
                query,
                pageNumber,
                pageSize,
                _mapper
            );
        }

        public async Task<List<ColdStorageResponse>> ListedColdStorages(string? searchTerm)
        {
            var csList = await _coldStorageQuery.ListedColdStorages(searchTerm);
            return _mapper.Map<List<ColdStorageResponse>>(csList);
        }

        public async Task<ColdStorageResponse> GetColdStorageById(int id)
        {
            var cs = await _coldStorageQuery.GetColdStorageById(id);
            return _mapper.Map<ColdStorageResponse>(cs);
        }

        public async Task<ColdStorageResponse> CreateColdStorage(ColdStorageRequest request)
        {
            var cs = _mapper.Map<ColdStorage>(request);
            cs.Status = Status.Active;

            await _context.ColdStorage.AddAsync(cs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ColdStorageResponse>(cs);
        }

        public async Task<ColdStorageResponse> UpdateColdStorage(ColdStorageRequest request, int id)
        {
            var cs = await _coldStorageQuery.PatchColdStorageById(id);
            if (cs == null)
            {
                throw new Exception($"Cold Storage with ID {id} not found.");
            }
            else
            {
                _mapper.Map(request, id);

                await _context.SaveChangesAsync();

                var updatedCs = await _context
                    .ColdStorage.Where(cs => cs.Id == cs.Id)
                    .ProjectTo<ColdStorageResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (updatedCs == null)
                {
                    throw new Exception("No Cold Storage response.");
                }
                else
                {
                    return updatedCs;
                }
            }
        }

        public async Task<ColdStorageResponse> ToggleStatus(int id)
        {
            var cs = await _coldStorageQuery.PatchColdStorageById(id);
            if (cs == null)
            {
                throw new Exception($"Cold Storage with ID {id} not found.");
            }
            else
            {
                cs.Status = cs.Status == Status.Active ? Status.Inactive : Status.Active;

                await _context.SaveChangesAsync();
                return _mapper.Map<ColdStorageResponse>(cs);
            }
        }

        public async Task<ColdStorageResponse> DeleteColdStorage(int id)
        {
            var cs = await _coldStorageQuery.PatchColdStorageById(id);
            if (cs == null)
            {
                throw new Exception($"Cold Storage with ID {id} not found.");
            }
            else
            {
                _context.ColdStorage.Remove(cs);
                await _context.SaveChangesAsync();

                return _mapper.Map<ColdStorageResponse>(cs);
            }
        }
    }
}
