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
    public class ReceivingService : BaseService, IReceivingService
    {
        private readonly ReceivingValidator _receivingValidator;
        private readonly ReceivingQueries _receivingQueries;
        private readonly DocumentHelper _documentHelper;
        public ReceivingService(AppDbContext context, IMapper mapper, ReceivingValidator receivingValidator, ReceivingQueries receivingQueries, DocumentHelper documentHelper) : base(context, mapper)
        {
            _receivingValidator = receivingValidator;
            _receivingQueries = receivingQueries;
            _documentHelper = documentHelper;
        }
        // [HttpGet("receivings")]
        public async Task<Pagination<ReceivingResponse>> AllReceivings(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            int? categoryId = null,
            string? status = null)
        {
            var query = _receivingQueries.ReceivingDisplayQuery(searchTerm, categoryId, status);
            return await PaginationHelper.PaginateAndMap<Receiving, ReceivingResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("receivings/pending")]
        public async Task<Pagination<ReceivingResponse>> AllPendings(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null)
        {
            var query = _receivingQueries.PendingReceivingsQuery(id);
            return await PaginationHelper.PaginateAndMap<Receiving, ReceivingResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("receiving-details/{product-id}")]
        public async Task<List<ProductBasesPallet>> ProductBasedPallets(int productId)
        {
            var receivings = await _receivingQueries.ReceivingDetailList(productId);

            var pallets = receivings.Select(r =>
            {
                // Use included collections
                var outgoing = r.Outgoingrepalletization?.ToList() ?? new List<Repalletization>();
                var incoming = r.Incomingrepalletization?.ToList() ?? new List<Repalletization>();

                // Use included dispatching details
                var validDispatches = r.DispatchingDetail?
                    .Where(d => d.Dispatching != null &&
                                d.Dispatching.Dispatched &&
                               !d.Dispatching.Declined &&
                               !d.Dispatching.Removed)
                    .ToList() ?? new List<DispatchingDetail>();

                var (remainingQty, remainingWgt) = CalculateRemainingValues(
                    r,
                    outgoing,
                    incoming,
                    validDispatches
                );

                var pallet = _mapper.Map<ProductBasesPallet>(r);
                pallet.Remainingquantity = remainingQty;
                pallet.Remainingweight = remainingWgt;
                return pallet;
            }).ToList();

            return pallets;
        }

        // [HttpGet("receiving/{id}")]
        public async Task<ReceivingResponse> GetReceiving(int id)
        {
            var receiving = await GetReceivingId(id);

            return _mapper.Map<ReceivingResponse>(receiving);
        }
        // [HttpGet("receiving/generate-documentNo")]
        public async Task<DocumentNumberResponse> GenerateDocumentNumber(string category)
        {
            var prefix = _receivingValidator.GetPrefixByCategory(category);

            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Invalid category. Cannot generate document number.");

            var documentNos = await _context.Receivings
                .AsNoTracking()
                .Include(r => r.Document)
                .Where(r => r.Document.Documentno.StartsWith(prefix))
                .Select(r => r.Document.Documentno)
                .ToListAsync();
            var nextSequence = _receivingValidator.GetNextSequence(documentNos);
            var generateDocNo = $"{prefix}-0000-{nextSequence:D4}";
            return new DocumentNumberResponse
            {
                Documentno = generateDocNo
            };
        }
        // [HttpGet("receivings/count-all")] "Total"
        public async Task<int> TotalCount()
        {
            return await _context.Receivings
                .AsNoTracking()
                .Where(r => !r.Removed)
                .CountAsync();
        }
        // [HttpGet("receivings/count-all")] "Pending"
        public async Task<int> PendingCount()
        {
            return await _context.Receivings
                .AsNoTracking()
                .Where(r => !r.Removed && r.Pending)
                .CountAsync();
        }
        // [HttpGet("receivings/count-all")] "Received"
        public async Task<int> ReceivedCount()
        {
            return await _context.Receivings
                .AsNoTracking()
                .Where(r => !r.Removed && r.Received)
                .CountAsync();
        }
        // [HttpGet("receivings/count-all")] "Declined"
        public async Task<int> DeclinedCount()
        {
            return await _context.Receivings
                .AsNoTracking()
                .Where(r => !r.Removed && r.Declined)
                .CountAsync();
        }
        // [HttpPost("receiving")]
        public async Task<ReceivingResponse> AddReceiving(ReceivingRequest request, IFormFile? file, ClaimsPrincipal user)
        {
            await _receivingValidator.ValidateReceivingRequest(request);

            var transaction = await _context.Database.BeginTransactionAsync();
            string? path = null;

            if (file != null)
            {
                string fulleName = AuthUserHelper.GetFullName(user);
                path = await FileHelper.SaveReceivingFormAsync(file, fulleName);
            }

            var details = request.ReceivingDetail;

            var positionId = request.ReceivingDetail
                .Where(r => r.Positionid > 0)
                .Select(r => r.Positionid)
                .Distinct()
                .ToList();

            var palletNo = request.ReceivingDetail
                .Where(r => r.Palletid > 0)
                .Select(r => r.Palletid)
                .Distinct()
                .ToList();

            var positions = await _context.Palletpositions
                .Where(p => positionId.Contains(p.Id))
                .ToListAsync();

            var pallets = await _context.Pallets
                .Where(p => palletNo.Contains(p.Id))
                .ToListAsync();

            var document = new DocumentRequest
            {
                Documentno = request.Documentno
            };

            var requestDocument = await _documentHelper.adddocument(document);

            var receiving = _mapper.Map<Receiving>(request);
            receiving.Documentid = requestDocument.Id;
            receiving.Requestorid = AuthUserHelper.GetUserId(user);
            receiving.Createdon = TimeHelper.GetPhilippineStandardTime();
            receiving.Receivingform = path;
            receiving.Pending = true;

            foreach (var detail in receiving.Receivingdetails)
            {
                detail.Receiving = receiving;
                detail.Received = true;
            }

            pallets.ForEach(p => p.Occupied = true);
            positions.ForEach(p => p.Hidden = true);

            await _context.Receivings.AddAsync(receiving);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var savedReceiving = await GetReceivingId(receiving.Id);

            return _mapper.Map<ReceivingResponse>(savedReceiving);
        }
        // [HttpPatch("receiving/toggle-request")]
        public async Task<ReceivingResponse> Request(ClaimsPrincipal user, string status, int documentId, string? note = null)
        {
            var receiving = await _context.Receivings
                    .Include(r => r.Document)
                    .FirstOrDefaultAsync(r => r.Documentid == documentId);

            RequestStatusUpdate(user, receiving, status, note);

            _context.Receivings.Update(receiving);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReceivingResponse>(receiving);
        }
        // [HttpPatch("receiving/update/{id}")]
        public async Task<ReceivingResponse> UpdateReceiving(ReceivingRequest request, IFormFile? file, int id, ClaimsPrincipal user)
        {
            var receiving = await PatchReceivingId(id);

            if (file != null)
            {
                string fulleName = AuthUserHelper.GetFullName(user);
                receiving.Receivingform = await FileHelper.SaveReceivingFormAsync(file, fulleName);
            }

            _mapper.Map(request, receiving);

            receiving.Requestorid = AuthUserHelper.GetUserId(user);
            receiving.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            return await ReceivingResponse(receiving.Id);
        }
        // [HttpPatch("receiving/hide/{id}")]
        public async Task<ReceivingResponse> HideReceiving(int id)
        {
            var receiving = await PatchReceivingId(id);

            receiving.Removed = !receiving.Removed;
            _context.Receivings.Update(receiving);
            await _context.SaveChangesAsync();

            return await ReceivingResponse(receiving.Id);
        }
        // [HttpDelete("receiving/delete/{id}")]
        public async Task<ReceivingResponse> DeleteReceiving(int id)
        {
            var receiving = await PatchReceivingId(id);

            _context.Receivings.Remove(receiving);
            await _context.SaveChangesAsync();

            return await ReceivingResponse(receiving.Id);
        }
        // Helpers
        private void RequestStatusUpdate(ClaimsPrincipal user, Receiving receiving, string status, string? note = null)
        {
            switch (status.ToLower())
            {
                case "approve":
                    receiving.Received = true;
                    receiving.Pending = false;
                    receiving.Declined = false;
                    receiving.Approverid = AuthUserHelper.GetUserId(user);
                    receiving.Datereceived = TimeHelper.GetPhilippineStandardTime();
                    break;
                case "decline":
                    receiving.Declined = true;
                    receiving.Pending = false;
                    receiving.Note = note;
                    receiving.Approverid = AuthUserHelper.GetUserId(user);
                    receiving.Datedeclined = TimeHelper.GetPhilippineStandardTime();
                    break;
            }
        }
        private async Task<Receiving?> PatchReceivingId(int id)
        {
            return await _receivingQueries.PatchReceivingId(id);
        }
        private async Task<Receiving?> GetReceivingId(int id)
        {
            return await _receivingQueries.GetReceivingId(id);
        }
        private async Task<ReceivingResponse> ReceivingResponse(int id)
        {
            var response = await GetReceivingId(id);
            return _mapper.Map<ReceivingResponse>(response);
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
