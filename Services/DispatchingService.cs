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
    public class DispatchingService : BaseService , IDispatchingService
    {
        private readonly DispatchingValidator _dispatchingValidator;
        private readonly DispatchingQueries _dispatchingQueries;
        private readonly DocumentHelper _documentHelper;
        public DispatchingService(AppDbContext context, IMapper mapper, DispatchingValidator dispatchingValidator, DispatchingQueries dispatchingQueries, DocumentHelper documentHelper) : base(context, mapper)
        {
            _dispatchingValidator = dispatchingValidator;
            _dispatchingQueries = dispatchingQueries;
            _documentHelper = documentHelper;
        }
        // [HttpGet("dispatchings/pending")]
        public async Task<Pagination<DispatchingResponse>> allpendings(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null)
        {
            var query = _dispatchingQueries.pendingdispatchingquery(id);
            return await PaginationHelper.paginateandmap<Dispatching, DispatchingResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("dispatchings")]
        public async Task<Pagination<DispatchingResponse>> alldispatched(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null)
        {

            var query = _dispatchingQueries.dispatchedquery(id);
            return await PaginationHelper.paginateandmap<Dispatching, DispatchingResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("dispatching/generate-documentNo")]
        public async Task<DocumentNumberResponse> generatedocumentnumber()
        {
            string prefix = "D3";

            var existingDocs = await _context.Dispatchings
                .Where(d => d.Document.Documentno.StartsWith(prefix))
                .Select(d => d.Document.Documentno)
                .ToListAsync();

            int nextNumber = 1;

            if (existingDocs.Any())
            {
                var maxNumber = existingDocs
                    .Select(doc =>
                    {
                        var parts = doc.Split('-');
                        return parts.Length == 3 && int.TryParse(parts[2], out int num) ? num : 0;
                    })
                    .Max();
                nextNumber = maxNumber + 1;
            }
            string generatedDocNo = $"{prefix}-0000-{nextNumber:D4}";
            return new DocumentNumberResponse
            {
                Documentno = generatedDocNo
            };
        }
        // [HttpGet("dispatching/{id}")]
        public async Task<DispatchingResponse> getdispatch(int id)
        {
            var dispatching = await getdispatchingdata(id);

            return _mapper.Map<DispatchingResponse>(dispatching);
        }
        // [HttpPost("dispatching/multiple")]
        public async Task<DispatchingResponse> addmultipledispatch(DispatchingRequest request, ClaimsPrincipal user)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            await _dispatchingValidator.ValidateDispatchingRequest(request);

            var document = new DocumentRequest
            {
                Documentno = request.Documentno
            };

            var requestDocument = _mapper.Map<Document>(document);
            await _context.Documents.AddAsync(requestDocument);
            await _context.SaveChangesAsync();

            var dispatch = _mapper.Map<Dispatching>(request);
            dispatch.Documentid = requestDocument.Id;
            dispatch.Pending = true;
            dispatch.Requestorid = AuthUserHelper.GetUserId(user);
            dispatch.Createdon = TimeHelper.GetPhilippineStandardTime();
            await _context.Dispatchings.AddAsync(dispatch);
            await _context.SaveChangesAsync();

            foreach (var detail in request.DispatchingDetail)
            {
                var receivingDetail = await _context.Receivingdetails
                    .Include(r => r.Pallet)
                    .FirstOrDefaultAsync(r => r.Id == detail.Receivingdetailid && r.Palletid == detail.Palletid && r.Received);
               
                var dispatchedQuantity = await _context.Dispatchingdetails
                    .Include(d => d.Pallet)
                    .Where(d => d.Receivingdetailid == detail.Receivingdetailid)
                    .SumAsync(d => (int?)d.Quantity) ?? 0;

                var totalDispatched = dispatchedQuantity + detail.Quantity;
                var isFullDispatched = totalDispatched == receivingDetail?.Quantityinapallet;

                var requestDispatchDetail = _mapper.Map<DispatchingDetail>(detail);
                requestDispatchDetail.Dispatchingid = dispatch.Id;
                requestDispatchDetail.Receivingdetailid = detail.Receivingdetailid;
                requestDispatchDetail.Partialdispatched = !isFullDispatched;
                requestDispatchDetail.Fulldispatched = isFullDispatched;

                _context.Dispatchingdetails.Add(requestDispatchDetail);

                receivingDetail.Fulldispatched = isFullDispatched;
                receivingDetail.Partialdispatched = !isFullDispatched;

                if (isFullDispatched)
                {
                    var pallet = await _context.Pallets
                        .FirstOrDefaultAsync(p => p.Id == requestDispatchDetail.Palletid);
                    pallet.Occupied = false;

                    var position = await _context.Palletpositions
                        .FirstOrDefaultAsync(p => p.Id == requestDispatchDetail.Positionid);
                    position.Hidden = false;
                }
                _context.Receivingdetails.Update(receivingDetail);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await dispatchingResponse(dispatch.Id);
        }
        // [HttpPost("dispatching")]
        public async Task<DispatchingResponse> addsingledispatch(DispatchingRequest request, ClaimsPrincipal user)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            await _dispatchingValidator.ValidateDispatchingRequest(request);

            var document = new DocumentRequest
            {
                Documentno = request.Documentno
            };

            var requestDocument = await _documentHelper.adddocument(document);

            var dispatch = _mapper.Map<Dispatching>(request);
            dispatch.Documentid = requestDocument.Id;
            dispatch.Pending = true;
            dispatch.Requestorid = AuthUserHelper.GetUserId(user);
            dispatch.Createdon = TimeHelper.GetPhilippineStandardTime();

            await _context.Dispatchings.AddAsync(dispatch);
            await _context.SaveChangesAsync();

            var groupedDetails = request.DispatchingDetail
                .GroupBy(d => d.Palletid)
                .Select(p => new
                {
                    Palletid = p.Key,
                    Quantity = p.Sum(d => d.Quantity),
                    DispatchingDetail = p.ToList()
                })
                .ToList();

            foreach (var group in groupedDetails)
            {
                var receivingDetail = await _context.Receivingdetails
                    .Include(r => r.Pallet)
                    .Where(r => r.Palletid == group.Palletid && !r.Fulldispatched && r.Received)
                    .OrderByDescending(r => r.Id)
                    .FirstOrDefaultAsync();
                
                var dispatchQuantity = await _context.Dispatchingdetails
                    .Include(d => d.Pallet)
                    .Where(d => d.Palletid == group.Palletid && d.Receivingdetailid == receivingDetail.Id)
                    .SumAsync(d => (int?)d.Quantity) ?? 0;

                var totalDispatchedQuantity = dispatchQuantity + group.Quantity;
                bool isFullDispatch = totalDispatchedQuantity == receivingDetail.Quantityinapallet;

                var requestDispatchingDetail = _mapper.Map<List<DispatchingDetail>>(group.DispatchingDetail);

                foreach (var detail in requestDispatchingDetail)
                {
                    detail.Dispatchingid = dispatch.Id;
                    detail.Receivingdetailid = receivingDetail.Id;
                    detail.Partialdispatched = !isFullDispatch;
                    detail.Fulldispatched = isFullDispatch;

                    await _context.Dispatchingdetails.AddAsync(detail);
                }
                receivingDetail.Fulldispatched = isFullDispatch;
                receivingDetail.Partialdispatched = !isFullDispatch;

                if (isFullDispatch)
                {
                    var pallet = await _context.Pallets
                        .FirstOrDefaultAsync(p => p.Id == receivingDetail.Palletid);
                    pallet.Occupied = false;

                    var palletPosition = await _context.Palletpositions
                        .FirstOrDefaultAsync(p => p.Id == receivingDetail.Positionid);
                    palletPosition.Hidden = false;
                }

                _context.Receivingdetails.Update(receivingDetail);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await dispatchingResponse(dispatch.Id);
        }
        // [HttpPatch("dispatching/update/{id}")]
        public async Task<DispatchingResponse> updatedispatch(ClaimsPrincipal user, DispatchingRequest request, int id)
        {
            var dispatching = await ValidateDispatch(id);

            _mapper.Map(request, dispatching);

            dispatching.Requestorid = AuthUserHelper.GetUserId(user);
            dispatching.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();
            return await dispatchingResponse(dispatching.Id);
        }
        // [HttpPatch("dispatching/time-start-end")]
        public async Task<DispatchingTimeStartEndResponse> addtimestartend (string timeStart, string timeEnd, int id, ClaimsPrincipal user)
        {
            var dispatching = await ValidateDispatch(id);

            dispatching.Dispatchtimestart = timeStart;
            dispatching.Dispatchtimeend = timeEnd;
            dispatching.Dispatched = true;
            dispatching.Requestorid = AuthUserHelper.GetUserId(user);
            dispatching.Updatedon = TimeHelper.GetPhilippineStandardTime();

            _context.Dispatchings.Update(dispatching);
            await _context.SaveChangesAsync();

            return _mapper.Map<DispatchingTimeStartEndResponse>(dispatching);
        }
        // [HttpPatch("dispatching/toggle-request")]
        public async Task<DispatchingResponse> request(ClaimsPrincipal user, string status, int documentId, string? note = null)
        {
            var dispatching = await _dispatchingQueries.getdispatchingbasedondocumentid(documentId);

            RequestStatusUpdate(user, dispatching, status, note);

            _context.Dispatchings.Update(dispatching);
            await _context.SaveChangesAsync();

            return await dispatchingResponse(dispatching.Id);
        }
        // [HttpPatch("dispatching/hide/{id}")]
        public async Task<DispatchingResponse> hidedispatch(int id)
        {
            var dispatching = await ValidateDispatch(id);

            dispatching.Removed = !dispatching.Removed;

            _context.Dispatchings.Update(dispatching);
            await _context.SaveChangesAsync();

            return await dispatchingResponse(dispatching.Id);
        }
        // [HttpDelete("dispatching/delete/{id}")]
        public async Task<DispatchingResponse> deletedispatch(int id)
        {
            var dispatching = await ValidateDispatch(id);

            _context.Dispatchings.Remove(dispatching);
            await _context.SaveChangesAsync();

            return await dispatchingResponse(dispatching.Id);
        }
        // Helpers
        private void RequestStatusUpdate(ClaimsPrincipal user, Dispatching dispatching, string status, string? note = null)
        {
            switch (status.ToLower())
            {
                case "approve":
                    dispatching.Dispatched = true;
                    dispatching.Approverid = AuthUserHelper.GetUserId(user);
                    dispatching.Approvedon = TimeHelper.GetPhilippineStandardTime();
                    dispatching.Pending = false;
                    break;
                case "decline":
                    dispatching.Declined = true;
                    dispatching.Approverid = AuthUserHelper.GetUserId(user);
                    dispatching.Declinedon = TimeHelper.GetPhilippineStandardTime();
                    break;
            }
        }
        private async Task<Dispatching?> getdispatchingid(int id)
        {
            return await _dispatchingQueries.patchmethoddispatchingid(id);
        }
        private async Task<Dispatching?> getdispatchingdata(int id)
        {
            return await _dispatchingQueries.getmethoddispatchingid(id);
        }
        private async Task<DispatchingResponse> dispatchingResponse(int id)
        {
            var response = await getdispatchingdata(id);
            return _mapper.Map<DispatchingResponse>(response);
        }
        // Validators
        private async Task<Dispatching> ValidateDispatch(int id)
        {
            var dispatch = await getdispatchingid(id);
            return dispatch ??
                throw new ArgumentException($"Dispatching data with id {id} not found.");
        }
    }
}
