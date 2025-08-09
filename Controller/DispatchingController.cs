using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Helpers.Queries;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Controller
{
    public class DispatchingController : BaseApiController
    {
        private readonly DispatchingService _dispatchingService;
        private readonly DispatchingQueries _dispatchingQueries;
        private readonly DispatchingExcel _dispatchingExcel;
        public DispatchingController(AppDbContext context, IMapper mapper, DispatchingService dispatchingService, DispatchingQueries dispatchingQueries, DispatchingExcel dispatchingExcel) : base(context, mapper)
        {
            _dispatchingService = dispatchingService;
            _dispatchingQueries = dispatchingQueries;
            _dispatchingExcel = dispatchingExcel;
        }
        // Count all dispatchings
        [HttpGet("dispatchings/count-all")]
        public async Task<ActionResult<DispatchingsCount>> CountAll()
        {
            try
            {
                var count = new DispatchingsCount
                {
                    Total = await _dispatchingService.TotalCount(),
                    Pending = await _dispatchingService.PendingCount(),
                    Dispatched = await _dispatchingService.DispatchedCount(),
                    Declined = await _dispatchingService.DeclinedCount()
                };
                return count;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pending dispatching request
        [HttpGet("dispatchings/pending")]
        public async Task<ActionResult<Pagination<DispatchingResponse>>> AllPendings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? id = null)
        {
            try
            {
                var response = await _dispatchingService.AllPendings(pageNumber, pageSize, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all dispatching request
        [HttpGet("dispatchings")]
        public async Task<ActionResult<Pagination<DispatchingResponse>>> AllDispatched(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? id = null,
            [FromQuery] string? documentNumber = null)
        {
            try
            {
                var response = await _dispatchingService.AllDispatched(pageNumber, pageSize, id, documentNumber);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate Document Number for dispatching
        [HttpGet("dispatching/generate-documentNo")]
        public async Task<ActionResult<DocumentNumberResponse>> GenerateDocumentNumber()
        {
            try
            {
                var response = await _dispatchingService.GenerateDocumentNumber();
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific dispatching request
        [HttpGet("dispatching/{id}")]
        public async Task<ActionResult<DispatchingResponse>> GetDispatch(int id)
        {
            try
            {
                var response = await _dispatchingService.GetDispatch(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export dispatchings
        [HttpGet("dispatchings/export")]
        public async Task<ActionResult> ExportDispatchings()
        {
            try
            {
                var dispatchings = await _dispatchingQueries.DispatchingsList();
                var file = await _dispatchingExcel.exportdispatching(dispatchings);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Dispatchings.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Dispatch multiple request 
        // Support partial and full dispatching 
        [HttpPost("dispatching/multiple")]
        public async Task<ActionResult<DispatchingResponse>> AddMultipleDispatch([FromBody] DispatchingRequest request)
        {
            try
            {
                var response = await _dispatchingService.AddMultipleDispatch(request, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Dispatch single request
        // Support partial and full dispatching
        [HttpPost("dispatching")]
        public async Task<ActionResult<DispatchingResponse>> AddSingleDispatch([FromBody] DispatchingRequest request)
        {
            try
            {
                var response = await _dispatchingService.AddSingleDispatch(request, User);
                return response;

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific dispatching request
        [HttpPatch("dispatching/update/{id}")]
        public async Task<ActionResult<DispatchingResponse>> UpdateDispatch([FromBody] DispatchingRequest request, int id)
        {
            try
            {
                var response = await _dispatchingService.UpdateDispatch(User, request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Add time start and end for specific dispatching request
        [HttpPatch("dispatching/time-start-end")]
        public async Task<ActionResult<DispatchingTimeStartEndResponse>> AddTimeStartEnd(string timeStart, string timeEnd, int id)
        {
            try
            {
                var response = await _dispatchingService.AddTimeStartEnd(timeStart, timeEnd, id, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Approve or decline specific dispatching request
        [HttpPatch("dispatching/toggle-request")]
        public async Task<ActionResult<DispatchingResponse>> ToggleRequest(string status, int documentId, string? note = null)
        {
            try
            {
                var response = await _dispatchingService.Request(User, status, documentId, note);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific dispatching request without removing in Database (Soft Delete)
        [HttpPatch("dispatching/hide/{id}")]
        public async Task<ActionResult<DispatchingResponse>> HideDispatch(int id)
        {
            try
            {
                var response = await _dispatchingService.HideDispatch(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific dispatching request in Database
        [HttpDelete("dispatching/delete/{id}")]
        public async Task<ActionResult<DispatchingResponse>> DeleteDispatch(int id)
        {
            try
            {
                var response = await _dispatchingService.DeleteDispatch(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
