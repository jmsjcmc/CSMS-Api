using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Helpers.Queries;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CSMapi.Controller
{
    public class ReceivingController : BaseApiController
    {
        private readonly ReceivingService _receivingService;
        private readonly ReceivingExcel _receivingExcel;
        private readonly ReceivingQueries _receivingQueries;
        public ReceivingController(AppDbContext context, IMapper mapper, ReceivingService receivingService, ReceivingExcel receivingExcel, ReceivingQueries receivingQueries) : base(context, mapper)
        {
            _receivingService = receivingService;
            _receivingExcel = receivingExcel;
            _receivingQueries = receivingQueries;
        }
        // Count all receivings
        [HttpGet("receivings/count-all")]
        public async Task<ActionResult<ReceivingsCount>> countall()
        {
            try
            {
                var count = new ReceivingsCount
                {
                    Total = await _receivingService.totalcount(),
                    Pending = await _receivingService.pendingcount(),
                    Received = await _receivingService.receivedcount(),
                    Declined = await _receivingService.declinedcount(),
                };

                return count;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all receiving request
        [HttpGet("receivings")]
        public async Task<ActionResult<Pagination<ReceivingResponse>>> allreceivings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? status = null)
        {
            try
            {
                var response = await _receivingService.allreceivings(pageNumber, pageSize, searchTerm, categoryId, status);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pending receiving request
        [HttpGet("receivings/pending")]
        public async Task<ActionResult<Pagination<ReceivingResponse>>> allpendings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? id = null)
        {
            try
            {
                var response = await _receivingService.allpendings(pageNumber, pageSize, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific receiving request
        [HttpGet("receiving/{id}")]
        public async Task<ActionResult<ReceivingResponse>> getreceiving(int id)
        {
            try
            {
                var response = await _receivingService.getreceiving(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate Document Number for receiving
        [HttpGet("receiving/generate-documentNo")]
        public async Task<ActionResult<DocumentNumberResponse>> generatedocumentnumber(string? category)
        {
            try
            {
                var response = await _receivingService.generatedocumentnumber(category);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate receivings template
        [HttpGet("receivings/template")]
        public async Task<ActionResult> receivingstemplate()
        {
            try
            {
                var file = _receivingExcel.generatereceivingtemplate();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReceivingTemplate.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export receivings
        [HttpGet("receivings/export")]
        public async Task<ActionResult> exportreceivings()
        {
            try
            {
                var receivings = await _receivingQueries.receivingslist();

                var file = await _receivingExcel.exportreceivings(receivings);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Receivings.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create receiving request
        [HttpPost("receiving")]
        public async Task<ActionResult<ReceivingResponse>> addreceiving([FromForm] ReceivingRequest request, IFormFile? file)
        {
            try
            {
                var response = await _receivingService.addreceiving(request, file, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Approve or decline receiving request
        [HttpPatch("receiving/toggle-request")]
        public async Task<ActionResult<ReceivingResponse>> request(string status, int documentId, string? note = null)
        {
            try
            {
                var response = await _receivingService.request(User ,status, documentId, note);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific receiving request
        [HttpPatch("receiving/update/{id}")]
        public async Task<ActionResult<ReceivingResponse>> updatereceiving([FromForm] ReceivingRequest request,IFormFile? file, int id)
        {
            try
            {
                var response = await _receivingService.updatereceiving(request, file, id, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific receiving request without removing in Database (Soft Delete) 
        [HttpPatch("receiving/hide/{id}")]
        public async Task<ActionResult<ReceivingResponse>> hidereceiving(int id)
        {
            try
            {
                var response = await _receivingService.hidereceiving(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific receiving request in Database
        [HttpDelete("receiving/delete/{id}")]
        public async Task<ActionResult<ReceivingResponse>> deletereceiving(int id)
        {
            try
            {
                var response = await _receivingService.deletereceiving(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
