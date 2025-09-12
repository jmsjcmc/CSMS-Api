using csms_backend.Models;

namespace csms_backend.Controllers
{
    public interface PalletInterface
    {
        Task<Pagination<PalletResponse>> PaginatedPallets(
            int pageNumber,
            int pageSize,
            string? searchTerm);
        Task<List<PalletResponse>> ListedPallets(string? searchTerm);
        Task<PalletResponse> GetPalletById(int id);
        Task<PalletResponse> CreatePallet(PalletRequest request);
        Task<PalletResponse> UpdatePallet(PalletRequest request, int id);
        Task<PalletResponse> ToggleStatus(int id);
        Task<PalletResponse> DeletePallet(int id);
    }
    public interface PalletPositionInterface
    {
        Task<Pagination<PalletPositionResponse>> PaginatedPalletPositions(
            int pageNumber,
            int pageSize,
            string? searchTerm);
        Task<List<PalletPositionResponse>> ListedPalletPositions(string? searchTerm);
        Task<PalletPositionResponse> CreatePalletPosition(PalletPositionRequest request);
        Task<PalletPositionResponse> UpdatePalletPosition(PalletPositionRequest request, int id);
        Task<PalletPositionResponse> ToggleStatus(int id);
        Task<PalletPositionResponse> DeletePalletPosition(int id);
    }
    public interface ColdStorageInterface
    {
        Task<Pagination<ColdStorageResponse>> PaginatedColdStorages(
            int pageNumber,
            int pageSize,
            string? searchTerm);
        Task<List<ColdStorageResponse>> ListedColdStorages(string? searchTerm);
        Task<ColdStorageResponse> CreateColdStorages(ColdStorageRequest request);
        Task<ColdStorageResponse> UpdateColdStorage(ColdStorageRequest request, int id);
        Task<ColdStorageResponse> ToggleStatus(int id);
        Task<ColdStorageResponse> DeleteColdStorage(int id);
    }
}
