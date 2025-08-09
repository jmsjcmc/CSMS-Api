using CSMapi.Models;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IPalletService
    {
        Task<List<ProductBasedOccupiedPalletResponse>> ProductBasedOccupiedPallets(int id);
        //Task<Pagination<OccupiedPalletResponse>> occupiedpallets(
        //    int pageNumber = 1,
        //    int pageSize = 10,
        //    string? searchTerm = null);
        Task<Pagination<RepalletizationDraftResponse>> PaginatedRepalletizationDraft(
            int pageNumber = 1,
            int pageSize = 10,
            int status = 0);
        Task<List<PalletTypeBasedResponse>> PalletTypePalletsList(string searchTerm);
        Task<List<ActivePalletResponse>> ActivePallets();
        Task<List<ColdStorageResponse>> AllColdStorages();
        Task<List<PalletPositionResponse>> GetFilteredPositions(int id);
        Task<Pagination<PalletResponse>> AllPallets(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<PalletPositionResponse>> AllPalletPositions();
        Task<ColdStorageResponse> GetColdStorage(int id);
        Task<PalletPositionResponse> GetPosition(int id);
        Task<PalletResponse> GetPallet(int id);
        Task<int> TotalCount();
        Task<int> ActiveCount();
        Task<int> OccupiedCount();
        Task<int> RepalletizedCount();
       
        Task<RepalletizationBulkResponse> BulkRepalletize(RepalletizationBulkRequest request, ClaimsPrincipal user);
        Task<ColdStorageResponse> AddColdStorage(ColdStorageRequest request);
        Task<PalletResponse> AddPallet(PalletRequest request, ClaimsPrincipal user);
        Task<PalletPositionResponse> AddPosition(PalletPositionRequest request);
        Task<ColdStorageResponse> UpdateColdStorage(ColdStorageRequest request, int id);
        Task<PalletResponse> UpdatePallet(PalletRequest request, int id, ClaimsPrincipal user);
        Task<PalletPositionResponse> UpdatePosition(PalletPositionRequest request, int id);
        Task<RepalletizationDraftResponse> ApproveRepalletizationDraft(int id);
        Task<ColdStorageResponse> CsToggleActive(int id);
        Task<PalletOnlyResponse> ToggleOccupy(int id, ClaimsPrincipal user);
        Task<PalletOnlyResponse> ToggleActive(int id, ClaimsPrincipal user);
        Task<PalletResponse> HidePallet(int id);
        Task<PalletPositionResponse> HidePosition(int id);
        Task<ColdStorageResponse> DeleteColdStorage(int id);
        Task<PalletResponse> DeletePallet(int id);
        Task<PalletPositionResponse> DeletePosition(int id);
    }
}
