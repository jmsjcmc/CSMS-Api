using CSMapi.Models;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IDispatchingService
    {
        Task<Pagination<DispatchingResponse>> AllPendings(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null);
        Task<Pagination<DispatchingResponse>> AllDispatched(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null,
            string? documentNumber = null);
        Task<DocumentNumberResponse> GenerateDocumentNumber();
        Task<int> TotalCount();
        Task<int> PendingCount();
        Task<int> DispatchedCount();
        Task<int> DeclinedCount();
        Task<DispatchingResponse> GetDispatch(int id);
        Task<DispatchingResponse> AddMultipleDispatch(DispatchingRequest request, ClaimsPrincipal user);
        Task<DispatchingResponse> AddSingleDispatch(DispatchingRequest request, ClaimsPrincipal user);
        Task<DispatchingTimeStartEndResponse> AddTimeStartEnd(string timeStart, string timeEnd, int id, ClaimsPrincipal user);
        Task<DispatchingResponse> Request(ClaimsPrincipal user, string status, int documentId, string? note = null);
        Task<DispatchingResponse> UpdateDispatch(ClaimsPrincipal user, DispatchingRequest request, int id);
        Task<DispatchingResponse> HideDispatch(int id);
        Task<DispatchingResponse> DeleteDispatch(int id);
    }
}
