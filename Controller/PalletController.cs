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
        // Fetch all occupied pallets based on product id
        [HttpGet("pallets/occupied/product-id")]
        public async Task<ActionResult<List<ProductBasedOccupiedPalletResponse>>> productbasedoccupiedpallets(int id)
        {
            try
            {
                var response = await _palletService.productbasedoccupiedpallets(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all occupied pallets
        [HttpGet("pallets/occupied")]
        public async Task<ActionResult<Pagination<OccupiedPalletResponse>>> occupiedpallets(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _palletService.occupiedpallets(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all active pallets
        [HttpGet("pallets/active")]
        public async Task<ActionResult<List<ActivePalletResponse>>> activepallets()
        {
            try
            {
                var response = await _palletService.activepallets();
                return response;

            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all Cold Storage 
        [HttpGet("cold-storages")]
        public async Task<ActionResult<List<ColdStorageResponse>>> allcoldstorages()
        {
            try
            {
                var response = await _palletService.allcoldstorages();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch pallet positions based on which cold storage 
        [HttpGet("cold-storages/positions")]
        public async Task<ActionResult<List<PalletPositionResponse>>> getfilteredpositions(int id)
        {
            try
            {
                var response = await _palletService.getfilteredpositions(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallets
        [HttpGet("pallets")]
        public async Task<ActionResult<Pagination<PalletResponse>>> allpallets(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _palletService.allpallets(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Pallet Template
        [HttpGet("pallets/template")]
        public async Task<ActionResult> pallettemplate()
        {
            try
            {
                var file = _palletExcel.generatepallettemplate();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PalletTemplate.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export pallets
        [HttpGet("pallets/export")]
        public async Task<ActionResult> exportpallets()
        {
            try
            {
                var pallets = await _context.Pallets
                    .ToListAsync();

                var file = _palletExcel.exportpallets(pallets);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pallets.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Pallet positions template
        [HttpGet("pallet-positions/template")]
        public async Task<ActionResult> positiontemplate()
        {
            try
            {
                var file = _palletExcel.generatepalletposition();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PalletPositionTemplate");
            } catch (Exception e)
            {
                return StatusCode(500, e.InnerException?.Message ?? e.Message);
            }
        }
        // Export pallet positions
        [HttpGet("pallet-positions/export")]
        public async Task<ActionResult> exportpositions()
        {
            try
            {
                var positions = await _context.Palletpositions
                    .ToListAsync();

                var file = _palletExcel.exportpositions(positions);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PalletPositions");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all pallet positions
        [HttpGet("pallet-positions")]
        public async Task<ActionResult<List<PalletPositionResponse>>> allpalletpositions()
        {
            try
            {
                var response = await _palletService.allpalletpositions();
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific cold storage
        [HttpGet("cold-storage/{id}")]
        public async Task<ActionResult<ColdStorageResponse>> getcoldstorage(int id)
        {
            try
            {
                var response = await _palletService.getcoldstorage(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific pallet position
        [HttpGet("pallet-position/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> getpositon(int id)
        {
            try
            {
                var response = await _palletService.getposition(id);
                return response;

            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific pallet 
        [HttpGet("pallet/{id}")]
        public async Task<ActionResult<PalletResponse>> getpallet(int id)
        {
            try
            {
                var response = await _palletService.getpallet(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Repalletize product
        [HttpPost("pallet/repalletization")]
        public async Task<ActionResult> repalletization([FromBody] RepalletizationRequest request)
        {
            try
            {
                await _palletService.repalletize(request, User);
                return Ok("Success.");

            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create storage
        [HttpPost("cold-storage")]
        public async Task<ActionResult<ColdStorageResponse>> addcoldstorage([FromBody] ColdStorageRequest request)
        {
            try
            {
                var response = await _palletService.addcoldstorage(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create pallet
        [HttpPost("pallet")]
        public async Task<ActionResult<PalletResponse>> addpallet([FromBody] PalletRequest request)
        {
            try
            {
                var response = await _palletService.addpallet(request, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create pallet position
        [HttpPost("pallet-position")]
        public async Task<ActionResult<PalletPositionResponse>> addposition([FromBody] PalletPositionRequest request)
        {
            try
            {
                var response = await _palletService.addposition(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import pallets
        [HttpPost("pallets/import")]
        public async Task<ActionResult<List<PalletOnlyResponse>>> importpallets(IFormFile file)
        {
            try
            {
                var pallets = _palletExcel.importpallets(file, User);
                await _context.Pallets.AddRangeAsync(pallets);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<List<PalletOnlyResponse>>(pallets);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import pallet positions
        [HttpPost("pallet-positions/import")]
        public async Task<ActionResult<List<PalletPositionResponse>>> importpositions(IFormFile file)
        {
            try
            {
                var positions = await _palletExcel.importpositions(file);
                await _context.Palletpositions.AddRangeAsync(positions);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<List<PalletPositionResponse>>(positions);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific cold storage
        [HttpPatch("cold-storage/update/{id}")]
        public async Task<ActionResult<ColdStorageResponse>> updatecoldstorage([FromBody] ColdStorageRequest request, int id)
        {
            try
            {
                var response = await _palletService.updatecoldstorage(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific pallet
        [HttpPatch("pallet/update/{id}")]
        public async Task<ActionResult<PalletResponse>> updatepallet([FromBody] PalletRequest request, int id)
        {
            try
            {
                var response = await _palletService.updatepallet(request, id, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }// Update specific pallet position

        [HttpPatch("pallet-position/update/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> updateposition([FromBody] PalletPositionRequest request, int id)
        {
            try
            {
                var response = await _palletService.updateposition(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific cold storage active status to true/false
        [HttpPatch("cold-storage/toggle-active")]
        public async Task<ActionResult<ColdStorageResponse>> cstoggleactive(int id)
        {
            try
            {
                var response = await _palletService.cstoggleactive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific pallet occupied status to true/false
        [HttpPatch("pallet/toggle-occupy")]
        public async Task<ActionResult<PalletOnlyResponse>> toggleoccupy (int id)
        {
            try
            {
                var response = await _palletService.toggleoccupy(id, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific pallet active status to true/false
        [HttpPatch("pallet/toggle-active")]
        public async Task<ActionResult<PalletOnlyResponse>> toggleactive(int id)
        {
            try
            {
                var response = await _palletService.toggleactive(id, User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific pallet without removing in Database (Soft Delete)
        [HttpPatch("pallet/hide/{id}")]
        public async Task<ActionResult<PalletResponse>> hidepallet(int id)
        {
            try
            {
                var response = await _palletService.hidepallet(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific pallet position without removing in Database (Soft Delete)
        [HttpPatch("pallet-position/hide/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> hideposition(int id)
        {
            try
            {
                var response = await _palletService.hideposition(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific cold storage in Database
        [HttpDelete("cold-storage/delete/{id}")]
        public async Task<ActionResult<ColdStorageResponse>> deletecoldstorage(int id)
        {
            try
            {
                var response = await _palletService.deletecoldstorage(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific pallet in Database
        [HttpDelete("pallet/delete/{id}")]
        public async Task<ActionResult<PalletResponse>> deletepallet(int id)
        {
            try
            {
                var response = await _palletService.deletepallet(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific pallet position in Database
        [HttpDelete("pallet-position/delete/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> deleteposition(int id)
        {
            try
            {
                var response = await _palletService.deleteposition(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
