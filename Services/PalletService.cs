using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSMapi.Services
{
    public class PalletService : BaseService, IPalletService
    {
        private readonly PalletValidator _palletValidator;
        private readonly PalletQueries _palletQueries;
        public PalletService(AppDbContext context, IMapper mapper, PalletValidator palletValidator, PalletQueries palletQueries) : base (context, mapper)
        {
            _palletValidator = palletValidator;
            _palletQueries = palletQueries;
        }
        // [HttpGet("pallets/occupied/product-id")]
        public async Task<List<ProductBasedOccupiedPalletResponse>> productbasedoccupiedpallets(int id)
        {
            var product = await _palletQueries.productbasedoccupiedpallets(id);
            return _mapper.Map<List<ProductBasedOccupiedPalletResponse>>(product);
        }
        // [HttpGet("pallets/occupied")]
        public async Task<Pagination<OccupiedPalletResponse>> occupiedpallets(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _palletQueries.occupiedpalletsquery(searchTerm);
            var totalCount = await query.CountAsync();

            var pallets = await PaginationHelper.paginateandproject<Pallet, OccupiedPalletResponse>(
                query, pageNumber, pageSize, _mapper);
          
            foreach (var pallet in pallets)
            {
                var receivingDetails = pallet.ReceivingDetail;

                foreach (var receivingDetail in receivingDetails)
                {
                    int dispatchedQuantity = await _context.Dispatchingdetails
                        .Where(d => d.Receivingdetailid == receivingDetail.Id)
                        .SumAsync(d => (int?)d.Quantity) ?? 0;

                    int repalletizedQuantity = await _context.Repalletizationdetails
                        .Include(r => r.Repalletization)
                        .Where(r => r.Repalletization.Topalletid == pallet.Id &&
                        r.Receivingdetailid != receivingDetail.Id)
                        .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                    int remainingQuantity = receivingDetail.Quantityinapallet - dispatchedQuantity + repalletizedQuantity;
                    receivingDetail.Quantityinapallet = remainingQuantity;
                }
            }

            return PaginationHelper.paginatedresponse(pallets, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("pallets/active")]
        public async Task<List<ActivePalletResponse>> activepallets()
        {
            var pallets = await _palletQueries.activepallets();

            return _mapper.Map<List<ActivePalletResponse>>(pallets);
        }
        // [HttpGet("cold-storages")]
        public async Task<List<ColdStorageResponse>> allcoldstorages()
        {
            var cs = await _palletQueries.activecoldstorages();

            return _mapper.Map<List<ColdStorageResponse>>(cs);
        }
        // [HttpGet("cold-storages/positions")]
        public async Task<List<PalletPositionResponse>> getfilteredpositions(int id)
        {
            var cs = await _palletQueries.getmethodcoldstorageid(id);

            var positions = await _palletQueries.getpositionsbasedoncs(cs.Id);

            return _mapper.Map<List<PalletPositionResponse>>(positions);
        }
        // [HttpGet("pallets")]
        public async Task<Pagination<PalletResponse>> allpallets(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _palletQueries.palletsquery(searchTerm);
            var totalCount = await query.CountAsync();

            var pallets = await PaginationHelper.paginateandproject<Pallet, PalletResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(pallets, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("pallet-positions")]
        public async Task<List<PalletPositionResponse>> allpalletpositions()
        {
            var positions = await _palletQueries.palletpositionsquery();

            return _mapper.Map<List<PalletPositionResponse>>(positions);
        }
        // [HttpGet("cold-storage/{id}")]
        public async Task<ColdStorageResponse> getcoldstorage(int id)
        {
            await _palletValidator.ValidateFetchColdStorage(id);

            var cs = await _context.Coldstorages
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpGet("pallet-position/{id}")]
        public async Task<PalletPositionResponse> getposition(int id)
        {
            await _palletValidator.ValidateFetchPosition(id);

            var position = await _context.Palletpositions
                .Include(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<PalletPositionResponse>(position);
        }
        // [HttpGet("pallet/{id}")]
        public async Task<PalletResponse> getpallet(int id)
        {
            await _palletValidator.ValidateFetchPallet(id);

            var pallet = await _palletQueries.getmethodpalletid(id);

            return _mapper.Map<PalletResponse>(pallet);
        }
        // [HttpPost("pallet/repalletization")]
        public async Task repalletize(RepalletizationRequest request, ClaimsPrincipal user)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            var repalletize = _mapper.Map<Repalletization>(request);
            repalletize.Creatorid = AuthUserHelper.GetUserId(user);
            repalletize.Createdon = TimeHelper.GetPhilippineStandardTime();

            _context.Repalletizations.Add(repalletize);
            await _context.SaveChangesAsync();

            var fromPalletDetail = await _context.Receivingdetails
                .Where(r => r.Palletid == request.Frompalletid)
                .ToListAsync();

            int fromPalletRemainingQuantity = 0;
            foreach (var detail in fromPalletDetail)
            {
                var totalRepalletized = await _context.Repalletizationdetails
                    .Where(r => r.Receivingdetailid == detail.Id)
                    .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                var totalDispatched = await _context.Dispatchingdetails
                    .Where(d => d.Receivingdetailid == detail.Id)
                    .SumAsync(d => (int?)d.Quantity) ?? 0;

                fromPalletRemainingQuantity += detail.Quantityinapallet - totalRepalletized - totalDispatched;
            }
            int totalQuantityMoved = 0;
            foreach (var detail in request.RepalletizationDetail)
            {
                var origin = await _context.Receivingdetails
                    .Include(r => r.Receiving)
                    .Include(r => r.Pallet)
                    .Include(r => r.RepalletizationDetail)
                    .FirstOrDefaultAsync(r => r.Id == detail.Receivingdetailid && r.Palletid == request.Frompalletid);

                var totalRepalletizedQuantity = await _context.Repalletizationdetails
                    .Where(r => r.Receivingdetailid == origin.Id)
                    .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                var totalOriginDispatchedQuantity = await _context.Dispatchingdetails
                    .Where(d => d.Receivingdetailid == origin.Id)
                    .SumAsync(d => (int?)d.Quantity) ?? 0;

                var remainingOriginQuantity = origin.Quantityinapallet - totalOriginDispatchedQuantity - totalRepalletizedQuantity;

                var unitWeight = origin.Quantityinapallet > 0
                    ? origin.Totalweight / origin.Quantityinapallet
                    : 0;

                var weightToMove = Math.Round(unitWeight * detail.Quantitymoved, 2);

                _context.Repalletizationdetails.Add(new RepalletizationDetail
                {
                    Repalletizationid = repalletize.Id,
                    Receivingdetailid = origin.Id,
                    Quantitymoved = detail.Quantitymoved,
                    Weightmoved = weightToMove
                });

                totalQuantityMoved += fromPalletRemainingQuantity;
            }

            if (totalQuantityMoved == fromPalletRemainingQuantity)
            {
                var pallet = await _context.Pallets
                    .FirstOrDefaultAsync(p => p.Id == request.Frompalletid);
                var firstDetail = fromPalletDetail.FirstOrDefault();

                var positionId = firstDetail.Positionid;

                var position = await _context.Palletpositions
                    .FirstOrDefaultAsync(p => p.Id == positionId);

                position.Hidden = false;
                pallet.Occupied = false;

            }
            
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        // [HttpPost("cold-storage")]
        public async Task<ColdStorageResponse> addcoldstorage(ColdStorageRequest request)
        {
            var cs = _mapper.Map<ColdStorage>(request);

            _context.Coldstorages.Add(cs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpPost("pallet")]
        public async Task<PalletResponse> addpallet(PalletRequest request, ClaimsPrincipal user)
        {
            await _palletValidator.ValidatePalletRequest(request);

            var pallet = _mapper.Map<Pallet>(request);

            pallet.Occupied = false;
            pallet.Active = true;
            pallet.Removed = false;
            pallet.Createdon = TimeHelper.GetPhilippineStandardTime();
            pallet.Creatorid = AuthUserHelper.GetUserId(user);

            _context.Pallets.Add(pallet);
            await _context.SaveChangesAsync();

            var savedPallet = await _palletQueries.getmethodpalletid(pallet.Id);

            return _mapper.Map<PalletResponse>(savedPallet);
        }
        // [HttpPost("pallet-position")]
        public async Task<PalletPositionResponse> addposition(PalletPositionRequest request)
        {
            await _palletValidator.ValidatePalletPosition(request);

            var position = _mapper.Map<PalletPosition>(request);

            _context.Palletpositions.Add(position);
            await _context.SaveChangesAsync();

            var savedPosition = await _context.Palletpositions
                .AsNoTracking()
                .Include(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Id == position.Id);

            return _mapper.Map<PalletPositionResponse>(savedPosition);
        }
        // [HttpPatch("cold-storage/update/{id}")]
        public async Task<ColdStorageResponse> updatecoldstorage(ColdStorageRequest request, int id)
        {
            var cs = await getcoldstorageid(id);

            _mapper.Map(request, cs);

            await _context.SaveChangesAsync();

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpPatch("pallet/update/{id}")]
        public async Task<PalletResponse> updatepallet(PalletRequest request, int id, ClaimsPrincipal user)
        {
            await _palletValidator.ValidatePalletUpdateRequest(request, id);

            var pallet = await getpalletid(id);

            _mapper.Map(request, pallet);
            pallet.Updatedon = TimeHelper.GetPhilippineStandardTime();
            pallet.Creatorid = AuthUserHelper.GetUserId(user);

            await _context.SaveChangesAsync();

            var updatedPallet = await _context.Pallets
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == pallet.Id);

            return _mapper.Map<PalletResponse>(updatedPallet);
        }
        // [HttpPatch("pallet-position/update/{id}")]
        public async Task<PalletPositionResponse> updateposition(PalletPositionRequest request, int id)
        {
            var position = await getpalletpositionid(id);

            _mapper.Map(request, position);

            await _context.SaveChangesAsync();

            var updatedPosition = await _context.Palletpositions
                .AsNoTracking()
                .Include(p => p.Coldstorage)
                .FirstOrDefaultAsync(p => p.Id == position.Id);

            return _mapper.Map<PalletPositionResponse>(updatedPosition);
        }
        // [HttpPatch("cold-storage/toggle-active")]
        public async Task<ColdStorageResponse> cstoggleactive(int id)
        {
            var cs = await getcoldstorageid(id);

            cs.Active = !cs.Active;

            _context.Coldstorages.Update(cs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpPatch("pallet/toggle-occupy")]
        public async Task<PalletOnlyResponse> toggleoccupy(int id, ClaimsPrincipal user)
        {
            var pallet = await getpalletid(id);

            pallet.Occupied = !pallet.Occupied;
            pallet.Creatorid = AuthUserHelper.GetUserId(user);
            pallet.Updatedon = TimeHelper.GetPhilippineStandardTime();

            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();

            return _mapper.Map<PalletOnlyResponse>(pallet);
        }
        // [HttpPatch("pallet/toggle-active")]
        public async Task<PalletOnlyResponse> toggleactive(int id, ClaimsPrincipal user)
        {
            var pallet = await getpalletid(id);

            pallet.Active = !pallet.Active;
            pallet.Creatorid = AuthUserHelper.GetUserId(user);
            pallet.Updatedon = TimeHelper.GetPhilippineStandardTime();
            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();

            return _mapper.Map<PalletOnlyResponse>(pallet);
        }
        // [HttpPatch("pallet/hide/{id}")]
        public async Task hidepallet(int id)
        {
            var pallet = await getpalletid(id);

            pallet.Removed = true;

            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();
        }
        // [HttpPatch("pallet-position/hide/{id}")]
        public async Task hideposition(int id)
        {
            var position = await getpalletpositionid(id);

            position.Removed = true;

            _context.Palletpositions.Update(position);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("cold-storage/delete/{id}")]
        public async Task deletecoldstorage(int id)
        {
            var cs = await getcoldstorageid(id);

            _context.Coldstorages.Remove(cs);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("pallet/delete/{id}")]
        public async Task deletepallet(int id)
        {
            var pallet = await getpalletid(id);

            _context.Pallets.Remove(pallet);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("pallet-position/delete/{id}")]
        public async Task deleteposition(int id)
        {
            var position = await getpalletpositionid(id);

            _context.Palletpositions.Remove(position);
            await _context.SaveChangesAsync();
        }
        // Helpers
        private async Task<Pallet?> getpalletid(int id)
        {
            return await _palletQueries.patchmethodpalletid(id);
        }

        private async Task<PalletPosition?> getpalletpositionid(int id)
        {
            return await _palletQueries.patchmethodpositionid(id);
        }

        private async Task<ColdStorage?> getcoldstorageid(int id)
        {
            return await _palletQueries.patchmethodcoldstorageid(id);
        }
    }
}
