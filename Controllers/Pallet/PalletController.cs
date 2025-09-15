using AutoMapper;
using csms_backend.Models;
using csms_backend.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csms_backend.Controllers
{
    public class PalletController : BaseController
    {
        private readonly PalletService _palletService;

        public PalletController(Context context, IMapper mapper, PalletService palletService)
            : base(context)
        {
            _palletService = palletService;
        }

        [HttpPost("pallet/create")]
        public async Task<ActionResult<PalletResponse>> CreatePallet(
            [FromBody] PalletRequest request
        )
        {
            try
            {
                var response = await _palletService.CreatePallet(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("pallet/toggle-status")]
        public async Task<ActionResult<PalletResponse>> ToggleStatus([FromQuery] int id)
        {
            try
            {
                var response = await _palletService.ToggleStatus(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("pallet/update/{id}")]
        public async Task<ActionResult<PalletResponse>> UpdatePallet(
            [FromBody] PalletRequest request,
            [FromQuery] int id
        )
        {
            try
            {
                var response = await _palletService.UpdatePallet(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("pallet/delete/{id}")]
        public async Task<ActionResult<PalletResponse>> DeletePallet([FromQuery] int id)
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

        [HttpGet("pallets/paginated")]
        public async Task<ActionResult<Pagination<PalletResponse>>> PaginatedPallets(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null
        )
        {
            try
            {
                var response = await _palletService.PaginatedPallets(
                    pageNumber,
                    pageSize,
                    searchTerm
                );
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("pallets/list")]
        public async Task<ActionResult<List<PalletResponse>>> ListedPallets(
            [FromQuery] string? searchTerm
        )
        {
            try
            {
                var response = await _palletService.ListedPallets(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("pallet/{id}")]
        public async Task<ActionResult<PalletResponse>> GetPalletById([FromQuery] int id)
        {
            try
            {
                var response = await _palletService.GetPalletById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }

    public class PalletPositionController : BaseController
    {
        private readonly PalletPositionService _palletPositionService;

        public PalletPositionController(
            Context context,
            PalletPositionService palletPositionService
        )
            : base(context)
        {
            _palletPositionService = palletPositionService;
        }

        [HttpPost("pallet-position/create")]
        public async Task<ActionResult<PalletPositionResponse>> CreatePalletPosition(
            [FromBody] PalletPositionRequest request
        )
        {
            try
            {
                var response = await _palletPositionService.CreatePalletPosition(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("pallet-position/toggle-status")]
        public async Task<ActionResult<PalletPositionResponse>> ToggleStatus([FromQuery] int id)
        {
            try
            {
                var response = await _palletPositionService.ToggleStatus(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut("pallet-position/update/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> UpdatePalletPosition(
            [FromBody] PalletPositionRequest request,
            [FromQuery] int id
        )
        {
            try
            {
                var response = await _palletPositionService.UpdatePalletPosition(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("pallet-position/delete/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> DeletePalletPosition(
            [FromQuery] int id
        )
        {
            try
            {
                var response = await _palletPositionService.DeletePalletPosition(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("pallet-positions/paginated")]
        public async Task<
            ActionResult<Pagination<PalletPositionResponse>>
        > PaginatedPalletPositions(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null
        )
        {
            try
            {
                var response = await _palletPositionService.PaginatedPalletPositions(
                    pageNumber,
                    pageSize,
                    searchTerm
                );
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("pallet-positions/list")]
        public async Task<ActionResult<List<PalletPositionResponse>>> ListedPalletPositions(
            [FromBody] string? searchTerm
        )
        {
            try
            {
                var response = await _palletPositionService.ListedPalletPositions(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("pallet-position/{id}")]
        public async Task<ActionResult<PalletPositionResponse>> GetPalletPositionId(int id)
        {
            try
            {
                var response = await _palletPositionService.GetPalletPositionById(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
