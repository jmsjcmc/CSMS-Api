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
    public class PalletService : BaseService , IPalletService
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
            var pallets = await _palletQueries.productbasedoccupiedpallets(id);
            return _mapper.Map<List<ProductBasedOccupiedPalletResponse>>(pallets);
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

                    int repalletizedFromQuantity = await _context.Repalletizationdetails
                        .Include(r => r.Repalletization)
                        .Where(r => r.Repalletization.Frompalletid == pallet.Id &&
                        r.Receivingdetailid == receivingDetail.Id)
                        .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                    int repalletizedToQuantity = await _context.Repalletizationdetails
                        .Include(r => r.Repalletization)
                        .Where(r => r.Repalletization.Topalletid == pallet.Id &&
                        r.Receivingdetailid != receivingDetail.Id)
                        .SumAsync(r => (int?)r.Quantitymoved) ?? 0;

                    int remainingQuantity = receivingDetail.Quantityinapallet - dispatchedQuantity - repalletizedFromQuantity + repalletizedToQuantity ;
                    receivingDetail.Quantityinapallet = remainingQuantity;
                }
            }
            return PaginationHelper.paginatedresponse(pallets, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("pallets/active/pallet-type")]
        public async Task<List<PalletTypeBasedResponse>> pallettypepalletslist(string searchTerm)
        {
            var pallets = await _palletQueries.pallettypepalletslist(searchTerm);
            return _mapper.Map<List<PalletTypeBasedResponse>>(pallets);
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
            return await PaginationHelper.paginateandmap<Pallet, PalletResponse>(query, pageNumber, pageSize, _mapper);
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

            var cs = await getcoldstoragedata(id);

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpGet("pallet-position/{id}")]
        public async Task<PalletPositionResponse> getposition(int id)
        {
            await _palletValidator.ValidateFetchPosition(id);

            var position = await getpalletpositiondata(id);

            return _mapper.Map<PalletPositionResponse>(position);
        }
        // [HttpGet("pallet/{id}")]
        public async Task<PalletResponse> getpallet(int id)
        {
            await _palletValidator.ValidateFetchPallet(id);

            var pallet = await getpalletdata(id);

            return _mapper.Map<PalletResponse>(pallet);
        }
        // [HttpGet("pallets/count-all")] "Total"
        public async Task<int> totalcount()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed)
                .CountAsync();
        }
        // [HttpGet("pallets/count-all")] "Active"
        public async Task<int> activecount()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed && p.Active)
                .CountAsync();
        }
        // [HttpGet("pallets/count-all")] "Occupied"
        public async Task<int> occupiedcount()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed && p.Occupied)
                .CountAsync();
        }
        // [HttpGet("pallets/count-all")] "Repalletized"
        public async Task<int> repalletizedcount()
        {
            return await _context.Repalletizations
                .AsNoTracking()
                .CountAsync();
        }
        // [HttpPost("pallet/repalletization")]
        public async Task<RepalletizationResponse> repalletize(RepalletizationRequest request, ClaimsPrincipal user)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            var repalletize = _mapper.Map<Repalletization>(request);
            repalletize.Creatorid = AuthUserHelper.GetUserId(user);
            repalletize.Createdon = TimeHelper.GetPhilippineStandardTime();

            await _context.Repalletizations.AddAsync(repalletize);
            await _context.SaveChangesAsync();

            foreach (var detail in request.RepalletizationDetail)
            {
                var origin = await _context.Receivingdetails
                    .FirstOrDefaultAsync(r => r.Id == detail.Receivingdetailid && r.Palletid == request.Frompalletid);

                if (origin == null) continue;

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
            }

            await _context.SaveChangesAsync();

            var totalOriginal = await _context.Receivingdetails
                .Where(r => r.Palletid == request.Frompalletid)
                .SumAsync(r => (int?)r.Quantityinapallet) ?? 0;

            var receivingDetailIds = await _context.Receivingdetails
                .Where(r => r.Palletid == request.Frompalletid)
                .Select(r => r.Id)
                .ToListAsync();

            var totalRepalletized = await _context.Repalletizationdetails
                .Where(rd => receivingDetailIds.Contains(rd.Receivingdetailid))
                .SumAsync(rd => (int?)rd.Quantitymoved) ?? 0;

            var totalDispatched = await _context.Dispatchingdetails
                .Where(dd => receivingDetailIds.Contains(dd.Receivingdetailid))
                .SumAsync(dd => (int?)dd.Quantity) ?? 0;

            var newRemaining = totalOriginal - totalRepalletized - totalDispatched;

            if (newRemaining == 0)
            {
                var pallet = await _context.Pallets
                    .FirstOrDefaultAsync(p => p.Id == request.Frompalletid);

                if (pallet == null)
                    throw new ArgumentException("Source pallet not found.");

                pallet.Occupied = false;

                

                var positionId = await _context.Receivingdetails
                    .Where(r => r.Palletid == request.Frompalletid)
                    .Select(r => r.Positionid)
                    .FirstOrDefaultAsync();

                if (positionId != null)
                {
                    var position = await _context.Palletpositions
                        .FirstOrDefaultAsync(p => p.Id == positionId);
                    position.Hidden = false;
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await repalletizationResponse(repalletize.Id);
        }
        // [HttpPost("cold-storage")]
        public async Task<ColdStorageResponse> addcoldstorage(ColdStorageRequest request)
        {
            var cs = _mapper.Map<ColdStorage>(request);

            await _context.Coldstorages.AddAsync(cs);
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

            await _context.Pallets.AddAsync(pallet);
            await _context.SaveChangesAsync();

            var savedPallet = await getpalletdata(pallet.Id);

            return _mapper.Map<PalletResponse>(savedPallet);
        }
        // [HttpPost("pallet-position")]
        public async Task<PalletPositionResponse> addposition(PalletPositionRequest request)
        {
            _palletValidator.ValidatePalletPosition(request);

            var position = _mapper.Map<PalletPosition>(request);

            await _context.Palletpositions.AddAsync(position);
            await _context.SaveChangesAsync();

            var savedPosition = await getpalletpositiondata(position.Id);
            return _mapper.Map<PalletPositionResponse>(savedPosition);
        }
        // [HttpPatch("cold-storage/update/{id}")]
        public async Task<ColdStorageResponse> updatecoldstorage(ColdStorageRequest request, int id)
        {
            var cs = await getcoldstorageid(id);

            _mapper.Map(request, cs);

            await _context.SaveChangesAsync();

            return await coldstorageResponse(cs.Id);
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

            return await palletResponse(pallet.Id);
        }
        // [HttpPatch("pallet-position/update/{id}")]
        public async Task<PalletPositionResponse> updateposition(PalletPositionRequest request, int id)
        {
            var position = await getpalletpositionid(id);

            _mapper.Map(request, position);

            await _context.SaveChangesAsync();

            return await positionResponse(position.Id);
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
        public async Task<PalletResponse> hidepallet(int id)
        {
            var pallet = await getpalletid(id);

            pallet.Removed = !pallet.Removed;

            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();

            return await palletResponse(pallet.Id);
        }
        // [HttpPatch("pallet-position/hide/{id}")]
        public async Task<PalletPositionResponse> hideposition(int id)
        {
            var position = await getpalletpositionid(id);

            position.Removed = !position.Removed;

            _context.Palletpositions.Update(position);
            await _context.SaveChangesAsync();

            return await positionResponse(position.Id);
        }
        // [HttpDelete("cold-storage/delete/{id}")]
        public async Task<ColdStorageResponse> deletecoldstorage(int id)
        {
            var cs = await getcoldstorageid(id);

            _context.Coldstorages.Remove(cs);
            await _context.SaveChangesAsync();

            return await coldstorageResponse(cs.Id);
        }
        // [HttpDelete("pallet/delete/{id}")]
        public async Task<PalletResponse> deletepallet(int id)
        {
            var pallet = await getpalletid(id);

            _context.Pallets.Remove(pallet);
            await _context.SaveChangesAsync();

            return await palletResponse(pallet.Id);
        }
        // [HttpDelete("pallet-position/delete/{id}")]
        public async Task<PalletPositionResponse> deleteposition(int id)
        {
            var position = await getpalletpositionid(id);

            _context.Palletpositions.Remove(position);
            await _context.SaveChangesAsync();

            return await positionResponse(position.Id);
        }
        // Helpers
        private async Task<Pallet> getpalletid(int id)
        {
            return await _palletQueries.patchmethodpalletid(id) ??
                throw new ArgumentException($"Pallet with id {id} not found.");
        }
        private async Task<PalletPosition> getpalletpositionid(int id)
        {
            return await _palletQueries.patchmethodpositionid(id) ?? 
                throw new ArgumentException($"Pallet position with id {id} not found.");
        }
        private async Task<ColdStorage> getcoldstorageid(int id)
        {
            return await _palletQueries.patchmethodcoldstorageid(id) ?? 
                throw new ArgumentException($"Cold Storage with id {id} not found.");
        }
        private async Task<Pallet> getpalletdata(int id)
        {
            return await _palletQueries.getmethodpalletid(id) ??
                throw new ArgumentException($"Pallet with id {id} not found.");
        }
        private async Task<PalletPosition> getpalletpositiondata(int id)
        {
            return await _palletQueries.getmethodpositionid(id) ?? 
                throw new ArgumentException($"Pallet position with id {id} not found.");
        }
        private async Task<ColdStorage> getcoldstoragedata(int id)
        {
            return await _palletQueries.getmethodcoldstorageid(id) ?? 
                throw new ArgumentException($"Cold storage with id {id} not found.");
        }
        private async Task<Repalletization> getrepalletizationdata(int id)
        {
            return await _palletQueries.getmethodrepalletizationid(id) ??
                throw new ArgumentException($"Repalletized with id {id} not found.");
        }
        private async Task<PalletResponse> palletResponse(int id)
        {
            var response = await getpalletdata(id);
            return _mapper.Map<PalletResponse>(response);
        }
        private async Task<RepalletizationResponse> repalletizationResponse(int id)
        {
            var response = await getrepalletizationdata(id);
            return _mapper.Map<RepalletizationResponse>(response);
        }
        private async Task<PalletPositionResponse> positionResponse(int id)
        {
            var response = await getpalletpositiondata(id);
            return _mapper.Map<PalletPositionResponse>(response);
        }
        private async Task<ColdStorageResponse> coldstorageResponse(int id)
        {
            var response = await getcoldstoragedata(id);
            return _mapper.Map<ColdStorageResponse>(response);
        }
    }
}
