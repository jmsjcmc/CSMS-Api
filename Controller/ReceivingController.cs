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
        public async Task<ActionResult<ReceivingsCount>> CountAll()
        {
            try
            {
                var count = new ReceivingsCount
                {
                    Total = await _receivingService.TotalCount(),
                    Pending = await _receivingService.PendingCount(),
                    Received = await _receivingService.ReceivedCount(),
                    Declined = await _receivingService.DeclinedCount(),
                };

                return count;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all receiving request
        [HttpGet("receivings")]
        public async Task<ActionResult<Pagination<ReceivingResponse>>> AllReceivings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? status = null)
        {
            try
            {
                var response = await _receivingService.AllReceivings(pageNumber, pageSize, searchTerm, categoryId, status);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pending receiving request
        [HttpGet("receivings/pending")]
        public async Task<ActionResult<Pagination<ReceivingResponse>>> AllPendings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? id = null)
        {
            try
            {
                var response = await _receivingService.AllPendings(pageNumber, pageSize, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallets connected to specific product through receiving details
        [HttpGet("receiving-details/product-id")]
        public async Task<ActionResult<List<ProductBasesPallet>>> ProductBasedPallets(int productId)
        {
            try
            {
                var response = await _receivingService.ProductBasedPallets(productId);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific receiving request
        [HttpGet("receiving/{id}")]
        public async Task<ActionResult<ReceivingResponse>> GetReceiving(int id)
        {
            try
            {
                var response = await _receivingService.GetReceiving(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate Document Number for receiving
        [HttpGet("receiving/generate-documentNo")]
        public async Task<ActionResult<DocumentNumberResponse>> GenerateDocumentNumber(string? category)
        {
            try
            {
                var response = await _receivingService.GenerateDocumentNumber(category);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate receivings template
        [HttpGet("receivings/template")]
        public async Task<ActionResult> ReceivingsTemplate()
        {
            try
            {
                var file = await Task.Run(() => _receivingExcel.generatereceivingtemplate());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReceivingTemplate.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export receivings
        [HttpGet("receivings/export")]
        public async Task<ActionResult> ExportReceivings()
        {
            try
            {
                var receivings = await _receivingQueries.ReceivingsList();

                var file = await _receivingExcel.exportreceivings(receivings);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Receivings.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create receiving request
        [HttpPost("receiving")]
        public async Task<ActionResult<ReceivingResponse>> AddReceiving([FromForm] ReceivingRequest request, IFormFile? file)
        {
            try
            {
                var response = await _receivingService.AddReceiving(request, file, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Approve or decline receiving request
        [HttpPatch("receiving/toggle-request")]
        public async Task<ActionResult<ReceivingResponse>> ToggleRequest(string status, int documentId, string? note = null)
        {
            try
            {
                var response = await _receivingService.Request(User ,status, documentId, note);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific receiving request
        [HttpPatch("receiving/update/{id}")]
        public async Task<ActionResult<ReceivingResponse>> UpdateReceiving([FromForm] ReceivingRequest request,IFormFile? file, int id)
        {
            try
            {
                var response = await _receivingService.UpdateReceiving(request, file, id, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific receiving request without removing in Database (Soft Delete) 
        [HttpPatch("receiving/hide/{id}")]
        public async Task<ActionResult<ReceivingResponse>> HideReceiving(int id)
        {
            try
            {
                var response = await _receivingService.HideReceiving(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific receiving request in Database
        [HttpDelete("receiving/delete/{id}")]
        public async Task<ActionResult<ReceivingResponse>> DeleteReceiving(int id)
        {
            try
            {
                var response = await _receivingService.DeleteReceiving(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
