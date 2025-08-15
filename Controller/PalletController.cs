using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Controller
{
    public class PalletController : BaseApiController
    {
        private readonly PalletExcel _palletExcel;
        private readonly PalletService _palletService;
        public PalletController(AppDbContext context, IMapper mapper, PalletService palletService, PalletExcel palletExcel) : base(context, mapper)
        {
            _palletExcel = palletExcel;
            _palletService = palletService;
        }
        // Count all pallets
        [HttpGet("pallets/count-all")]
        public async Task<ActionResult<PalletsCount>> CountAll()
        {
            try
            {
                var count = new PalletsCount
                {
                    Total = await _palletService.TotalCount(),
                    Active = await _palletService.ActiveCount(),
                    Occupied = await _palletService.OccupiedCount(),
                    Repalletized = await _palletService.RepalletizedCount()
                };
                return count;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all occupied pallets based on product id
        [HttpGet("pallets/occupied/product-id")]
        public async Task<ActionResult<List<ProductBasedOccupiedPalletResponse>>> ProductBasedOccupiedPallets(int id)
        {
            try
            {
                var response = await _palletService.ProductBasedOccupiedPallets(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all occupied pallets
        [HttpGet("pallets/occupied")]
        public async Task<ActionResult<Pagination<OccupiedPalletResponse>>> OccupiedPallets(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] int status = 0)
        {
            try
            {
                var response = await _palletService.OccupiedPallets(pageNumber, pageSize, searchTerm, status);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallets cs movement in draft
        [HttpGet("pallets/cs-movement-draft")]
        public async Task<ActionResult<Pagination<CsToCsResponse>>> PaginatedCsToCsMovement(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int status = 0)
        {
            try
            {
                var response = await _palletService.PaginatedCsToCsMovement(pageNumber, pageSize, status);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallets that are repalletized in draft
        [HttpGet("pallets/repalletization-draft")]
        public async Task<ActionResult<Pagination<RepalletizationDraftResponse>>> PaginatedRepalletizationDraft(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int status = 0)
        {
            try
            {
                var response = await _palletService.PaginatedRepalletizationDraft(pageNumber, pageSize, status);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all active pallets based on pallet type
        [HttpGet("pallets/active/pallet-type")]
        public async Task<ActionResult<List<PalletTypeBasedResponse>>> PalletTypePalletsList(string searchTerm)
        {
            try
            {
                var response = await _palletService.PalletTypePalletsList(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all active pallets
        [HttpGet("pallets/active")]
        public async Task<ActionResult<List<ActivePalletResponse>>> ActivePallets()
        {
            try
            {
                var response = await _palletService.ActivePallets();
                return response;

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all Cold Storage 
        [HttpGet("cold-storages")]
        public async Task<ActionResult<List<ColdStorageResponse>>> AllColdStorages()
        {
            try
            {
                var response = await _palletService.AllColdStorages();
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch pallet positions based on which cold storage 
        [HttpGet("cold-storages/positions")]
        public async Task<ActionResult<List<PalletPositionResponse>>> GetFilteredPositions(int id)
        {
            try
            {
                var response = await _palletService.GetFilteredPositions(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallets
        [HttpGet("pallets")]
        public async Task<ActionResult<Pagination<PalletResponse>>> AllPallets(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _palletService.AllPallets(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Pallet Template
        [HttpGet("pallets/template")]
        public async Task<ActionResult> PalletTemplate()
        {
            try
            {
                var file = await Task.Run(() => _palletExcel.generatepallettemplate());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PalletTemplate.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export pallets
        [HttpGet("pallets/export")]
        public async Task<ActionResult> ExportPallets()
        {
            try
            {
                var pallets = await _context.Pallets
                    .ToListAsync();

                var file = _palletExcel.exportpallets(pallets);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pallets.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Pallet positions template
        [HttpGet("pallet-positions/template")]
        public async Task<ActionResult> PositionTemplate()
        {
            try
            {
                var file = await Task.Run(() => _palletExcel.generatepalletposition());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PalletPositionTemplate");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException?.Message ?? e.Message);
            }
        }
        // Export pallet positions
        [HttpGet("pallet-positions/export")]
        public async Task<ActionResult> ExportPositions()
        {
            try
            {
                var positions = await _context.Palletpositions
                    .ToListAsync();

                var file = _palletExcel.exportpositions(positions);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PalletPositions");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallet positions
        [HttpGet("pallet-positions")]
        public async Task<ActionResult<List<PalletPositionResponse>>> AllPalletPositions()
        {
            try
            {
                var response = await _palletService.AllPalletPositions();
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific cold storage
        [HttpGet("cold-storage/{id}")]
        public async Task<ActionResult<ColdStorageResponse>> GetColdStorage(int id)
        {
            try
            {
                var response = await _palletService.GetColdStorage(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific pallet position
        [HttpGet("pallet-position/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> GetPosition(int id)
        {
            try
            {
                var response = await _palletService.GetPosition(id);
                return response;

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific pallet 
        [HttpGet("pallet/{id}")]
        public async Task<ActionResult<PalletResponse>> GetPallet(int id)
        {
            try
            {
                var response = await _palletService.GetPallet(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Bulk Repalletization
        [HttpPost("pallets/bulk-repalletization")]
        public async Task<ActionResult<RepalletizationBulkResponse>> BulkRepalletize([FromBody] RepalletizationBulkRequest request)
        {
            try
            {
                var response = await _palletService.BulkRepalletize(request, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create storage
        [HttpPost("cold-storage")]
        public async Task<ActionResult<ColdStorageResponse>> AddColdStorage([FromBody] ColdStorageRequest request)
        {
            try
            {
                var response = await _palletService.AddColdStorage(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create pallet
        [HttpPost("pallet")]
        public async Task<ActionResult<PalletResponse>> AddPallet([FromBody] PalletRequest request)
        {
            try
            {
                var response = await _palletService.AddPallet(request, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create pallet position
        [HttpPost("pallet-position")]
        public async Task<ActionResult<PalletPositionResponse>> AddPosition([FromBody] PalletPositionRequest request)
        {
            try
            {
                var response = await _palletService.AddPosition(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import pallets
        [HttpPost("pallets/import")]
        public async Task<ActionResult<List<PalletOnlyResponse>>> ImportPallets(IFormFile file)
        {
            try
            {
                var pallets = _palletExcel.importpallets(file, User);
                await _context.Pallets.AddRangeAsync(pallets);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<List<PalletOnlyResponse>>(pallets);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import pallet positions
        [HttpPost("pallet-positions/import")]
        public async Task<ActionResult<List<PalletPositionResponse>>> ImportPositions(IFormFile file)
        {
            try
            {
                var positions = await _palletExcel.importpositions(file);
                await _context.Palletpositions.AddRangeAsync(positions);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<List<PalletPositionResponse>>(positions);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Bulk Cold Storage to Cold Storage Movement
        [HttpPatch("pallets/bulk-cs-movement")]
        public async Task<ActionResult<List<CsToCsResponse>>> BulkCsToCsMovement([FromBody] CsToCsBulkRequest request)
        {
            try
            {
                var response = await _palletService.BulkCsToCsMovement(request, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific cold storage
        [HttpPatch("cold-storage/update/{id}")]
        public async Task<ActionResult<ColdStorageResponse>> UpdateColdStorage([FromBody] ColdStorageRequest request, int id)
        {
            try
            {
                var response = await _palletService.UpdateColdStorage(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific pallet
        [HttpPatch("pallet/update/{id}")]
        public async Task<ActionResult<PalletResponse>> UpdatePallet([FromBody] PalletRequest request, int id)
        {
            try
            {
                var response = await _palletService.UpdatePallet(request, id, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific pallet position
        [HttpPatch("pallet-position/update/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> UpdatePosition([FromBody] PalletPositionRequest request, int id)
        {
            try
            {
                var response = await _palletService.UpdatePosition(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Approve cs movement
        [HttpPatch("pallets/approve-cs-movement-draft")]
        public async Task<ActionResult<CsToCsBulkResponse>> ApproveCsToCsMovement(int id)
        {
            try
            {
                var response = await _palletService.ApproveCsToCsMovement(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Approve repalletization draft
        [HttpPatch("pallets/approve-repalletization-draft")]
        public async Task<ActionResult<RepalletizationDraftResponse>> ApproveRepalletizationDraft(int id)
        {
            try
            {
                var response = await _palletService.ApproveRepalletizationDraft(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific cold storage active status to true/false
        [HttpPatch("cold-storage/toggle-active")]
        public async Task<ActionResult<ColdStorageResponse>> CsToggleActive(int id)
        {
            try
            {
                var response = await _palletService.CsToggleActive(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific pallet occupied status to true/false
        [HttpPatch("pallet/toggle-occupy")]
        public async Task<ActionResult<PalletOnlyResponse>> ToggleOccupy(int id)
        {
            try
            {
                var response = await _palletService.ToggleOccupy(id, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific pallet active status to true/false
        [HttpPatch("pallet/toggle-active")]
        public async Task<ActionResult<PalletOnlyResponse>> ToggleActive(int id)
        {
            try
            {
                var response = await _palletService.ToggleActive(id, User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific pallet without removing in Database (Soft Delete)
        [HttpPatch("pallet/hide/{id}")]
        public async Task<ActionResult<PalletResponse>> HidePallet(int id)
        {
            try
            {
                var response = await _palletService.HidePallet(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific pallet position without removing in Database (Soft Delete)
        [HttpPatch("pallet-position/hide/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> HidePosition(int id)
        {
            try
            {
                var response = await _palletService.HidePosition(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific cold storage in Database
        [HttpDelete("cold-storage/delete/{id}")]
        public async Task<ActionResult<ColdStorageResponse>> DeleteColdStorage(int id)
        {
            try
            {
                var response = await _palletService.DeleteColdStorage(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific pallet in Database
        [HttpDelete("pallet/delete/{id}")]
        public async Task<ActionResult<PalletResponse>> DeletePallet(int id)
        {
            try
            {
                var response = await _palletService.DeletePallet(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific pallet position in Database
        [HttpDelete("pallet-position/delete/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> DeletePosition(int id)
        {
            try
            {
                var response = await _palletService.DeletePosition(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific pallet repalletization in Database 
        [HttpDelete("pallet/repalletization/delete/{id}")]
        public async Task<ActionResult<RepalletizationResponse>> DeleteRepalletization(int id)
        {
            try
            {
                var response = await _palletService.DeleteRepalletization(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
