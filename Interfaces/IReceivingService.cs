using CSMapi.Models;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IReceivingService
    {
        Task<Pagination<ReceivingResponse>> AllReceivings(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            int? categoryId = null,
            string? status = null);
        Task<Pagination<ReceivingResponse>> AllPendings(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null);
        Task<List<ProductBasesPallet>> ProductBasedPallets(int productId);
        Task<ReceivingResponse> GetReceiving(int id);
        Task<DocumentNumberResponse> GenerateDocumentNumber(string category);
        Task<int> TotalCount();
        Task<int> PendingCount();
        Task<int> ReceivedCount();
        Task<int> DeclinedCount();
        Task<ReceivingResponse> AddReceiving(ReceivingRequest request, IFormFile file, ClaimsPrincipal user);
        Task<ReceivingResponse> Request(ClaimsPrincipal user, string status, int documentId, string? note = null);
        Task<ReceivingResponse> UpdateReceiving(ReceivingRequest request, IFormFile? file, int id, ClaimsPrincipal user);
        Task<ReceivingResponse> HideReceiving(int id);
        Task<ReceivingResponse> DeleteReceiving(int id);
    }
}
