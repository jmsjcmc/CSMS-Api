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
        public ReceivingService(AppDbContext context, IMapper mapper, ReceivingValidator receivingValidator, ReceivingQueries receivingQueries, DocumentHelper documentHelper) : base (context, mapper)
        {
            _receivingValidator = receivingValidator;
            _receivingQueries = receivingQueries;
            _documentHelper = documentHelper;
        }
        // [HttpGet("receivings")]
        public async Task<Pagination<ReceivingResponse>> allreceivings(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? category = null,
            string? status = null)
        {
            var query = _receivingQueries.receivingsquery(searchTerm, category, status);
            var totalCount = await query.CountAsync();

            var receivings = await PaginationHelper.paginateandproject<Receiving, ReceivingResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(receivings, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("receivings/pending")]
        public async Task<Pagination<ReceivingResponse>> allpendings(
            int pageNumber = 1,
            int pageSize = 10,
            string? category = null)
        {
            var query = _receivingQueries.pendingreceivingsquery(category);
            var totalCount = await query.CountAsync();

            var receivings = await PaginationHelper.paginateandproject<Receiving, ReceivingResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(receivings, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("receiving/{id}")]
        public async Task<ReceivingResponse> getreceiving(int id)
        {
            var receiving = await _receivingQueries.getmethodreceivingid(id);

            return _mapper.Map<ReceivingResponse>(receiving);
        }
        // [HttpGet("receiving/generate-documentNo")]
        public async Task<DocumentNumberResponse> generatedocumentnumber(string category)
        {
            var prefix = _receivingValidator.GetPrefixByCategory(category);
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
        // [HttpPost("receiving")]
        public async Task<ReceivingResponse> addreceiving(ReceivingRequest request, IFormFile file, ClaimsPrincipal user)
        {
            await _receivingValidator.ValidateReceivingRequest(request);

            var transaction = await _context.Database.BeginTransactionAsync();

            string fulleName = AuthUserHelper.GetFullName(user);
            string path = await FileHelper.SaveReceivingFormAsync(file, fulleName);

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

            _context.Receivings.Add(receiving);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            var savedReceiving = await _receivingQueries.getmethodreceivingid(receiving.Id);

            return _mapper.Map<ReceivingResponse>(savedReceiving);
        }
        // [HttpPatch("receiving/toggle-request")]
        public async Task<ReceivingResponse> request(ClaimsPrincipal user, string status, int documentId, string? note = null)
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
        public async Task<ReceivingResponse> updatereceiving(ReceivingRequest request, IFormFile? file, int id, ClaimsPrincipal user)
        {
            var receiving = await getreceivingid(id);

            if (file != null)
            {
                string fulleName = AuthUserHelper.GetFullName(user);
                receiving.Receivingform = await FileHelper.SaveReceivingFormAsync(file, fulleName);
            }
            
            _mapper.Map(request, receiving);
            
            receiving.Requestorid = AuthUserHelper.GetUserId(user);
            receiving.Updatedon = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();

            var updatedReceiving = await _receivingQueries.getmethodreceivingid(receiving.Id);

            return _mapper.Map<ReceivingResponse>(updatedReceiving);
        }
        // [HttpPatch("receiving/hide/{id}")]
        public async Task hidereceiving(int id)
        {
            var receiving = await getreceivingid(id);

            receiving.Removed = true;
            _context.Receivings.Update(receiving);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("receiving/delete/{id}")]
        public async Task deletereceiving(int id)
        {
            var receiving = await getreceivingid(id);

            _context.Receivings.Remove(receiving);
            await _context.SaveChangesAsync();
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

       private async Task<Receiving?> getreceivingid(int id)
        {
            return await _receivingQueries.patchmethodreceivingid(id);
        }
    }
}
