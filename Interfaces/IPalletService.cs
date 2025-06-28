using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IPalletService
    {
        Task<List<ProductBasedOccupiedPalletResponse>> productbasedoccupiedpallets(int id);
        Task<Pagination<OccupiedPalletResponse>> occupiedpallets(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<ActivePalletResponse>> activepallets();
        Task<List<ColdStorageResponse>> allcoldstorages();
        Task<List<PalletPositionResponse>> getfilteredpositions(int id);
        Task<Pagination<PalletResponse>> allpallets(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<PalletPositionResponse>> allpalletpositions();
        Task<ColdStorageResponse> getcoldstorage(int id);
        Task<PalletPositionResponse> getposition(int id);
        Task<PalletResponse> getpallet(int id);
        Task repalletize(RepalletizationRequest request, ClaimsPrincipal user);
        Task<ColdStorageResponse> addcoldstorage(ColdStorageRequest request);
        Task<PalletResponse> addpallet(PalletRequest request, ClaimsPrincipal user);
        Task<PalletPositionResponse> addposition(PalletPositionRequest request);
        Task<ColdStorageResponse> updatecoldstorage(ColdStorageRequest request, int id);
        Task<PalletResponse> updatepallet(PalletRequest request, int id, ClaimsPrincipal user);
        Task<PalletPositionResponse> updateposition(PalletPositionRequest request, int id);
        Task<ColdStorageResponse> cstoggleactive(int id);
        Task<PalletOnlyResponse> toggleoccupy(int id, ClaimsPrincipal user);
        Task<PalletOnlyResponse> toggleactive(int id, ClaimsPrincipal user);
        Task hidepallet(int id);
        Task hideposition(int id);
        Task deletecoldstorage(int id);
        Task deletepallet(int id);
        Task deleteposition(int id);
    }
}
