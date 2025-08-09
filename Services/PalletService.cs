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
        public PalletService(AppDbContext context, IMapper mapper, PalletValidator palletValidator, PalletQueries palletQueries) : base(context, mapper)
        {
            _palletValidator = palletValidator;
            _palletQueries = palletQueries;
        }
        // [HttpGet("pallets/occupied/product-id")]
        public async Task<List<ProductBasedOccupiedPalletResponse>> ProductBasedOccupiedPallets(int id)
        {
            var pallets = await _palletQueries.ProductBasedOccupiedPallets(id);
            return _mapper.Map<List<ProductBasedOccupiedPalletResponse>>(pallets);
        }
        // [HttpGet("pallets/occupied")]
        //public async Task<Pagination<OccupiedPalletResponse>> occupiedpallets(
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    string? searchTerm = null)
        //{
        //    var query = _palletQueries.occupiedpalletsquery(searchTerm);
        //    var totalCount = await query.CountAsync();

        //    var pallets = await PaginationHelper.paginateandproject<Pallet, OccupiedPalletResponse>(
        //        query, pageNumber, pageSize, _mapper);

        //    if (!pallets.Any())
        //        return PaginationHelper.paginatedresponse(pallets, totalCount, pageNumber, pageSize);

        //    // Prefetch all necessary data
        //    var palletIds = pallets.Select(p => p.Id).ToList();
        //    var rdIds = pallets.SelectMany(p => p.ReceivingDetail.Select(rd => rd.Id)).Distinct().ToList();

        //    // Dispatched quantities (grouped by receiving detail)
        //    var dispatched = await _context.Dispatchingdetails
        //        .Where(d => rdIds.Contains(d.Receivingdetailid))
        //        .GroupBy(d => d.Receivingdetailid)
        //        .Select(g => new { g.Key, Sum = g.Sum(d => (int?)d.Quantity) ?? 0 })
        //        .ToDictionaryAsync(x => x.Key, x => x.Sum);

        //    // Repalletization FROM current pallet (using ReceivingDetail.Palletid)
        //    var fromRepalletized = await _context.Repalletizationdetails
        //        .Include(rd => rd.Receivingdetail)  // Need to include ReceivingDetail
        //        .Where(r => rdIds.Contains(r.Receivingdetailid))
        //        .GroupBy(r => new
        //        {
        //            SourcePalletId = r.Receivingdetail.Palletid,  // Get source pallet from ReceivingDetail
        //            r.Receivingdetailid
        //        })
        //        .Select(g => new
        //        {
        //            g.Key.SourcePalletId,
        //            g.Key.Receivingdetailid,
        //            Sum = g.Sum(r => (int?)r.Quantitymoved) ?? 0
        //        })
        //        .ToListAsync();

        //    var fromRepalletizedDict = fromRepalletized
        //        .ToDictionary(x => new { PalletId = x.SourcePalletId, Receivingdetailid = x.Receivingdetailid }, x => x.Sum);

        //    // Repalletization TO current pallet
        //    var toRepalletized = await _context.Repalletizationdetails
        //        .Include(rd => rd.Repalletization)  // Need Repalletization for Topalletid
        //        .Where(r => rdIds.Contains(r.Receivingdetailid))
        //        .GroupBy(r => new
        //        {
        //            PalletId = r.Repalletization.Topalletid,
        //            r.Receivingdetailid
        //        })
        //        .Select(g => new
        //        {
        //            g.Key.PalletId,
        //            g.Key.Receivingdetailid,
        //            Sum = g.Sum(r => (int?)r.Quantitymoved) ?? 0
        //        })
        //        .ToListAsync();

        //    var toRepalletizedDict = toRepalletized
        //        .ToDictionary(x => new { x.PalletId, x.Receivingdetailid }, x => x.Sum);

        //    // Calculate remaining quantities
        //    foreach (var pallet in pallets)
        //    {
        //        foreach (var rd in pallet.ReceivingDetail)
        //        {
        //            int dispatchedQty = dispatched.TryGetValue(rd.Id, out var dVal) ? dVal : 0;

        //            // Use PalletId
        //            var fromKey = new { PalletId = pallet.Id, Receivingdetailid = rd.Id };
        //            int fromQty = fromRepalletizedDict.TryGetValue(fromKey, out var fVal) ? fVal : 0;

        //            var toKey = new { PalletId = pallet.Id, Receivingdetailid = rd.Id };
        //            int toQty = toRepalletizedDict.TryGetValue(toKey, out var tVal) ? tVal : 0;

        //            rd.Quantityinapallet = rd.Quantityinapallet - dispatchedQty - fromQty + toQty;
        //        }
        //    }

        //    return PaginationHelper.paginatedresponse(pallets, totalCount, pageNumber, pageSize);
        //}
        //public async Task<Pagination<OccupiedPalletResponse>> occupiedpallets(
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    string? searchTerm = null)
        //{
        //    var query = _palletQueries.occupiedpalletsquery(searchTerm);
        //    var totalCount = await query.CountAsync();

        //    var pallets = await PaginationHelper.paginateandproject<Pallet, OccupiedPalletResponse>(
        //        query, pageNumber, pageSize, _mapper);

        //    // Return without recalculating - quantities are already updated in DB
        //    return PaginationHelper.paginatedresponse(pallets, totalCount, pageNumber, pageSize);
        //}
        // [HttpGet("pallets/repalletization-draft")]
        public async Task<Pagination<RepalletizationDraftResponse>> PaginatedRepalletizationDraft(
            int pageNumber = 1,
            int pageSize = 10,
            int status = 0)
        {
            var query = _palletQueries.PaginatedRepalletizationDraftQuery(status);
            var totalCount = await query.CountAsync();
            var repalletizations = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Collect all receiving detail IDs for both source and target pallets
            var allReceivingDetailIds = repalletizations
                .Select(r => r.Fromreceivingdetailid)
                .Concat(repalletizations.Select(r => r.Toreceivingdetailtid))
                .Distinct()
                .ToList();

            // Load all receiving details with their related data
            var receivingDetails = await _context.Receivingdetails
                .Where(rd => allReceivingDetailIds.Contains(rd.Id))
                .Include(rd => rd.Pallet)
                .Include(rd => rd.PalletPosition.Coldstorage)
                .Include(rd => rd.DispatchingDetail)
                    .ThenInclude(dd => dd.Dispatching)
                .Include(rd => rd.Outgoingrepalletization)
                .Include(rd => rd.Incomingrepalletization)
                .ToDictionaryAsync(rd => rd.Id);

            // Map to response with remaining quantities
            var responseItems = new List<RepalletizationDraftResponse>();
            foreach (var rep in repalletizations)
            {
                var responseItem = _mapper.Map<RepalletizationDraftResponse>(rep);

                // Process source pallet
                if (receivingDetails.TryGetValue(rep.Fromreceivingdetailid, out var fromDetail))
                {
                    var sourcePallet = MapToProductBasesPalletWithRemaining(fromDetail);
                    responseItem.Fromreceivingdetail = sourcePallet;
                }

                // Process target pallet
                if (receivingDetails.TryGetValue(rep.Toreceivingdetailtid, out var toDetail))
                {
                    var targetPallet = MapToProductBasesPalletWithRemaining(toDetail);
                    responseItem.Toreceivingdetail = targetPallet;
                }

                responseItems.Add(responseItem);
            }

            return new Pagination<RepalletizationDraftResponse>
            {
                Items = responseItems,
                Totalcount = totalCount,
                Pagenumber = pageNumber,
                Pagesize = pageSize
            };
        }
        // [HttpGet("pallets/active/pallet-type")]
        public async Task<List<PalletTypeBasedResponse>> PalletTypePalletsList(string searchTerm)
        {
            var pallets = await _palletQueries.PalletTypePalletsList(searchTerm);
            return _mapper.Map<List<PalletTypeBasedResponse>>(pallets);
        }
        // [HttpGet("pallets/active")]
        public async Task<List<ActivePalletResponse>> ActivePallets()
        {
            var pallets = await _palletQueries.ActivePallets();

            return _mapper.Map<List<ActivePalletResponse>>(pallets);
        }
        // [HttpGet("cold-storages")]
        public async Task<List<ColdStorageResponse>> AllColdStorages()
        {
            var cs = await _palletQueries.ActiveColdStorages();

            return _mapper.Map<List<ColdStorageResponse>>(cs);
        }
        // [HttpGet("cold-storages/positions")]
        public async Task<List<PalletPositionResponse>> GetFilteredPositions(int id)
        {
            var cs = await _palletQueries.GetColdStorageId(id);

            var positions = await _palletQueries.GetPositionByCs(cs.Id);

            return _mapper.Map<List<PalletPositionResponse>>(positions);
        }
        // [HttpGet("pallets")]
        public async Task<Pagination<PalletResponse>> AllPallets(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _palletQueries.PalletsQuery(searchTerm);
            return await PaginationHelper.PaginateAndMap<Pallet, PalletResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("pallet-positions")]
        public async Task<List<PalletPositionResponse>> AllPalletPositions()
        {
            var positions = await _palletQueries.PalletPositionsQuery();

            return _mapper.Map<List<PalletPositionResponse>>(positions);
        }
        // [HttpGet("cold-storage/{id}")]
        public async Task<ColdStorageResponse> GetColdStorage(int id)
        {
            var cs = await GetCsId(id);

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpGet("pallet-position/{id}")]
        public async Task<PalletPositionResponse> GetPosition(int id)
        {
            var position = await GetPositionId(id);

            return _mapper.Map<PalletPositionResponse>(position);
        }
        // [HttpGet("pallet/{id}")]
        public async Task<PalletResponse> GetPallet(int id)
        {
            var pallet = await GetPalletId(id);

            return _mapper.Map<PalletResponse>(pallet);
        }
        // [HttpGet("pallets/count-all")] "Total"
        public async Task<int> TotalCount()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed)
                .CountAsync();
        }
        // [HttpGet("pallets/count-all")] "Active"
        public async Task<int> ActiveCount()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed && p.Active)
                .CountAsync();
        }
        // [HttpGet("pallets/count-all")] "Occupied"
        public async Task<int> OccupiedCount()
        {
            return await _context.Pallets
                .AsNoTracking()
                .Where(p => !p.Removed && p.Occupied)
                .CountAsync();
        }
        // [HttpGet("pallets/count-all")] "Repalletized"
        public async Task<int> RepalletizedCount()
        {
            return await _context.Repalletizations
                .AsNoTracking()
                .CountAsync();
        }
        // [HttpPost("pallet/bulk-repalletization")]
        public async Task<RepalletizationBulkResponse> BulkRepalletize(RepalletizationBulkRequest request, ClaimsPrincipal user)
        {
            var response = new RepalletizationBulkResponse
            {
                Repalletization = new List<RepalletizationResponse>()
            };
            var creatorId = AuthUserHelper.GetUserId(user);
            var transaction = await _context.Database.BeginTransactionAsync();

            foreach (var repallet in request.Repalletization)
            {
                var result = new RepalletizationResponse();
                try
                {
                    var fromDetail = await _context.Receivingdetails
                        .Include(r => r.Pallet)
                        .FirstOrDefaultAsync(r => r.Id == repallet.Fromreceivingdetailid);

                    var toDetail = await _context.Receivingdetails
                        .FirstOrDefaultAsync(r => r.Id == repallet.Toreceivingdetailtid);

                    if (fromDetail == null || toDetail == null)
                        throw new Exception("From or to pallet not found.");

                    if (repallet.Quantitymoved <= 0)
                        throw new Exception("Quantity moved must be greated than zero.");

                    if (fromDetail.Duquantity < repallet.Quantitymoved)
                        throw new Exception("Insufficient quantity in source pallet.");

                    var repalletization = new Repalletization
                    {
                        Fromreceivingdetailid = repallet.Fromreceivingdetailid,
                        Toreceivingdetailtid = repallet.Toreceivingdetailtid,
                        Quantitymoved = repallet.Quantitymoved,
                        Weightmoved = repallet.Weightmoved,
                        Createdon = TimeHelper.GetPhilippineStandardTime(),
                        Creatorid = creatorId,
                        Status = 1
                    };

                    await _context.Repalletizations.AddAsync(repalletization);

                    fromDetail.Duquantity -= repallet.Quantitymoved;
                    toDetail.Duweight += repallet.Quantitymoved;

                    if (fromDetail.Duquantity == 0 && fromDetail.Pallet != null)
                    {
                        fromDetail.Pallet.Occupied = false;
                    }

                    result.Id = repalletization.Id;
                    result.Fromreceivingdetailid = repalletization.Fromreceivingdetailid;
                    result.Toreceivingdetailtid = repalletization.Toreceivingdetailtid;
                    result.Quantitymoved = repalletization.Quantitymoved;
                    result.Weightmoved = repalletization.Weightmoved;
                    result.Createdon = repalletization.Createdon;
                    result.Status = repalletization.Status;

                    response.Repalletization.Add(result);
                    response.Successcount++;
                }
                catch (Exception e)
                {
                    response.Failurecount++;
                    continue;
                }
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return response;
        }
        // [HttpPost("cold-storage")]
        public async Task<ColdStorageResponse> AddColdStorage(ColdStorageRequest request)
        {
            var cs = _mapper.Map<ColdStorage>(request);

            await _context.Coldstorages.AddAsync(cs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpPost("pallet")]
        public async Task<PalletResponse> AddPallet(PalletRequest request, ClaimsPrincipal user)
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

            var savedPallet = await GetPalletId(pallet.Id);

            return _mapper.Map<PalletResponse>(savedPallet);
        }
        // [HttpPost("pallet-position")]
        public async Task<PalletPositionResponse> AddPosition(PalletPositionRequest request)
        {
            _palletValidator.ValidatePalletPosition(request);

            var position = _mapper.Map<PalletPosition>(request);

            await _context.Palletpositions.AddAsync(position);
            await _context.SaveChangesAsync();

            var savedPosition = await GetPositionId(position.Id);
            return _mapper.Map<PalletPositionResponse>(savedPosition);
        }
        // [HttpPatch("cold-storage/update/{id}")]
        public async Task<ColdStorageResponse> UpdateColdStorage(ColdStorageRequest request, int id)
        {
            var cs = await PatchCsId(id);

            _mapper.Map(request, cs);

            await _context.SaveChangesAsync();

            return await CsResponse(cs.Id);
        }
        // [HttpPatch("pallet/update/{id}")]
        public async Task<PalletResponse> UpdatePallet(PalletRequest request, int id, ClaimsPrincipal user)
        {
            await _palletValidator.ValidatePalletUpdateRequest(request, id);

            var pallet = await PatchPalletId(id);

            _mapper.Map(request, pallet);
            pallet.Updatedon = TimeHelper.GetPhilippineStandardTime();
            pallet.Creatorid = AuthUserHelper.GetUserId(user);

            await _context.SaveChangesAsync();

            return await PalletResponse(pallet.Id);
        }
        // [HttpPatch("pallet-position/update/{id}")]
        public async Task<PalletPositionResponse> UpdatePosition(PalletPositionRequest request, int id)
        {
            var position = await PatchPositionId(id);

            _mapper.Map(request, position);

            await _context.SaveChangesAsync();

            return await PositionResponse(position.Id);
        }
        // [HttpPatch("pallets/approve-repalletization-draft")]
        public async Task<RepalletizationDraftResponse> ApproveRepalletizationDraft(int id)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            var draft = await _palletQueries.PatchRepalletizationsDraftId(id);

            draft.Status = 2;
            draft.Approvedon = TimeHelper.GetPhilippineStandardTime();

            _context.Repalletizations.Update(draft);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Create response with updated data
            return _mapper.Map<RepalletizationDraftResponse>(draft);
        }
        // [HttpPatch("cold-storage/toggle-active")]
        public async Task<ColdStorageResponse> CsToggleActive(int id)
        {
            var cs = await PatchCsId(id);

            cs.Active = !cs.Active;

            _context.Coldstorages.Update(cs);
            await _context.SaveChangesAsync();

            return _mapper.Map<ColdStorageResponse>(cs);
        }
        // [HttpPatch("pallet/toggle-occupy")]
        public async Task<PalletOnlyResponse> ToggleOccupy(int id, ClaimsPrincipal user)
        {
            var pallet = await PatchPalletId(id);

            pallet.Occupied = !pallet.Occupied;
            pallet.Creatorid = AuthUserHelper.GetUserId(user);
            pallet.Updatedon = TimeHelper.GetPhilippineStandardTime();

            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();

            return _mapper.Map<PalletOnlyResponse>(pallet);
        }
        // [HttpPatch("pallet/toggle-active")]
        public async Task<PalletOnlyResponse> ToggleActive(int id, ClaimsPrincipal user)
        {
            var pallet = await PatchPalletId(id);

            pallet.Active = !pallet.Active;
            pallet.Creatorid = AuthUserHelper.GetUserId(user);
            pallet.Updatedon = TimeHelper.GetPhilippineStandardTime();
            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();

            return _mapper.Map<PalletOnlyResponse>(pallet);
        }
        // [HttpPatch("pallet/hide/{id}")]
        public async Task<PalletResponse> HidePallet(int id)
        {
            var pallet = await PatchPalletId(id);

            pallet.Removed = !pallet.Removed;

            _context.Pallets.Update(pallet);
            await _context.SaveChangesAsync();

            return await PalletResponse(pallet.Id);
        }
        // [HttpPatch("pallet-position/hide/{id}")]
        public async Task<PalletPositionResponse> HidePosition(int id)
        {
            var position = await PatchPositionId(id);

            position.Removed = !position.Removed;

            _context.Palletpositions.Update(position);
            await _context.SaveChangesAsync();

            return await PositionResponse(position.Id);
        }
        // [HttpDelete("cold-storage/delete/{id}")]
        public async Task<ColdStorageResponse> DeleteColdStorage(int id)
        {
            var cs = await PatchCsId(id);

            _context.Coldstorages.Remove(cs);
            await _context.SaveChangesAsync();

            return await CsResponse(cs.Id);
        }
        // [HttpDelete("pallet/delete/{id}")]
        public async Task<PalletResponse> DeletePallet(int id)
        {
            var pallet = await PatchPalletId(id);

            _context.Pallets.Remove(pallet);
            await _context.SaveChangesAsync();

            return await PalletResponse(pallet.Id);
        }
        // [HttpDelete("pallet-position/delete/{id}")]
        public async Task<PalletPositionResponse> DeletePosition(int id)
        {
            var position = await PatchPositionId(id);

            _context.Palletpositions.Remove(position);
            await _context.SaveChangesAsync();

            return await PositionResponse(position.Id);
        }
        // Helpers
        private async Task<Pallet?> PatchPalletId(int id)
        {
            return await _palletQueries.PatchPalletId(id);
        }
        private async Task<PalletPosition?> PatchPositionId(int id)
        {
            return await _palletQueries.PatchPositionId(id);
        }
        private async Task<ColdStorage?> PatchCsId(int id)
        {
            return await _palletQueries.PatchCsId(id);
        }
        private async Task<Pallet?> GetPalletId(int id)
        {
            return await _palletQueries.GetPalletId(id);
        }
        private async Task<PalletPosition?> GetPositionId(int id)
        {
            return await _palletQueries.GetPositionId(id);
        }
        private async Task<ColdStorage?> GetCsId(int id)
        {
            return await _palletQueries.GetColdStorageId(id);
        }
        private async Task<Repalletization?> GetRepalletizationId(int id)
        {
            return await _palletQueries.GetRepalletizationId(id);
        }
        private async Task<PalletResponse> PalletResponse(int id)
        {
            var response = await GetPalletId(id);
            return _mapper.Map<PalletResponse>(response);
        }
        private async Task<PalletPositionResponse> PositionResponse(int id)
        {
            var response = await GetPositionId(id);
            return _mapper.Map<PalletPositionResponse>(response);
        }
        private async Task<ColdStorageResponse> CsResponse(int id)
        {
            var response = await GetCsId(id);
            return _mapper.Map<ColdStorageResponse>(response);
        }
        private ProductBasesPallet MapToProductBasesPalletWithRemaining(ReceivingDetail rd)
        {
            var pallet = _mapper.Map<ProductBasesPallet>(rd);

            // Get related collections
            var outgoing = rd.Outgoingrepalletization?.ToList() ?? new List<Repalletization>();
            var incoming = rd.Incomingrepalletization?.ToList() ?? new List<Repalletization>();

            var validDispatches = rd.DispatchingDetail?
                .Where(d => d.Dispatching != null &&
                           d.Dispatching.Dispatched &&
                          !d.Dispatching.Declined &&
                          !d.Dispatching.Removed)
                .ToList() ?? new List<DispatchingDetail>();

            // Calculate remaining values
            var (remainingQty, remainingWgt) = CalculateRemainingValues(
                rd, outgoing, incoming, validDispatches);

            pallet.Remainingquantity = remainingQty;
            pallet.Remainingweight = remainingWgt;

            return pallet;
        }
        private (int RemainingQuantity, double RemainingWeight) CalculateRemainingValues(
            ReceivingDetail receivingDetail,
            List<Repalletization> outgoingRepalletizations,
            List<Repalletization> incomingRepalletizations,
            List<DispatchingDetail> validDispatches)
        {
            int originalQty = receivingDetail.Quantityinapallet;
            double originalWgt = receivingDetail.Totalweight;

            // Calculate dispatched quantities
            int dispatchedQty = validDispatches.Sum(d => d.Quantity);
            double dispatchedWgt = validDispatches.Sum(d => d.Totalweight);

            // Calculate repalletization adjustments
            int repOutQty = outgoingRepalletizations.Sum(rep => rep.Quantitymoved);
            int repInQty = incomingRepalletizations.Sum(rep => rep.Quantitymoved);

            // Calculate net remaining quantity
            int remainingQty = originalQty + repInQty - repOutQty - dispatchedQty;
            remainingQty = Math.Max(0, remainingQty);

            // Calculate weight using original unit weight consistently
            double wpu = originalQty > 0 ? originalWgt / originalQty : 0;
            double repOutWgt = repOutQty * wpu;
            double repInWgt = repInQty * wpu;

            double remainingWgt = originalWgt + repInWgt - repOutWgt - dispatchedWgt;
            remainingWgt = Math.Max(0, Math.Round(remainingWgt, 2));

            return (remainingQty, remainingWgt);
        }
    }
}
