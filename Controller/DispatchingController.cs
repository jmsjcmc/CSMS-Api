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
        // Fetch all pending dispatching request
        [HttpGet("dispatchings/pending")]
        public async Task<ActionResult<Pagination<DispatchingResponse>>> allpendings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? id = null)
        {
            try
            {
                var response = await _dispatchingService.allpendings(pageNumber, pageSize, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all dispatching request
        [HttpGet("dispatchings")]
        public async Task<ActionResult<Pagination<DispatchingResponse>>> alldispatched(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? id = null)
        {
            try
            {
                var response = await _dispatchingService.alldispatched(pageNumber, pageSize, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate Document Number for dispatching
        [HttpGet("dispatching/generate-documentNo")]
        public async Task<ActionResult<DocumentNumberResponse>> generatedocumentnumber()
        {
            try
            {
                var response = await _dispatchingService.generatedocumentnumber();
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific dispatching request
        [HttpGet("dispatching/{id}")]
        public async Task<ActionResult<DispatchingResponse>> getdispatch (int id)
        {
            try
            {
                var response = await _dispatchingService.getdispatch(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export dispatchings
        [HttpGet("dispatchings/export")]
        public async Task<ActionResult> exportdispatchings()
        {
            try
            {
                var dispatchings = await _dispatchingQueries.dispatchingslist();
                var file = await _dispatchingExcel.exportdispatching(dispatchings);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Dispatchings.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Dispatch multiple request 
        // Support partial and full dispatching 
        [HttpPost("dispatching/multiple")]
        public async Task<ActionResult<DispatchingResponse>> addmultipledispatch([FromBody] DispatchingRequest request)
        {
            try
            {
                var response = await _dispatchingService.addmultipledispatch(request, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Dispatch single request
        // Support partial and full dispatching
        [HttpPost("dispatching")]
        public async Task<ActionResult<DispatchingResponse>> addsingledispatch([FromBody] DispatchingRequest request)
        {
            try
            {
                var response = await _dispatchingService.addsingledispatch(request, User);
                return response;

            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific dispatching request
        [HttpPatch("dispatching/update/{id}")]
        public async Task<ActionResult<DispatchingResponse>> updatedispatch([FromBody] DispatchingRequest request, int id)
        {
            try
            {
                var response = await _dispatchingService.updatedispatch(User, request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Add time start and end for specific dispatching request
        [HttpPatch("dispatching/time-start-end")]
        public async Task<ActionResult<DispatchingTimeStartEndResponse>> addtimestartend (string timeStart, string timeEnd, int id)
        {
            try
            {
                var response = await _dispatchingService.addtimestartend(timeStart, timeEnd, id, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Approve or decline specific dispatching request
        [HttpPatch("dispatching/toggle-request")]
        public async Task<ActionResult<DispatchingResponse>> request(string status, int documentId, string? note = null)
        {
            try
            {
                var response = await _dispatchingService.request(User, status, documentId, note);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific dispatching request without removing in Database (Soft Delete)
        [HttpPatch("dispatching/hide/{id}")]
        public async Task<ActionResult<DispatchingResponse>> hidedispatch(int id)
        {
            try
            {
                var response = await _dispatchingService.hidedispatch(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific dispatching request in Database
        [HttpDelete("dispatching/delete/{id}")]
        public async Task<ActionResult<DispatchingResponse>> deletedispatch(int id)
        {
            try
            {
                var response = await _dispatchingService.deletedispatch(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
