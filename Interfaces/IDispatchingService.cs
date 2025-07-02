using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IDispatchingService
    {
        Task<Pagination<DispatchingResponse>> allpendings(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null);
        Task<Pagination<DispatchingResponse>> alldispatched(
            int pageNumber = 1,
            int pageSize = 10,
            int? id = null);
        Task<DocumentNumberResponse> generatedocumentnumber();
        Task<DispatchingResponse> getdispatch(int id);
        Task<DispatchingResponse> addmultipledispatch(DispatchingRequest request, ClaimsPrincipal user);
        Task<DispatchingResponse> addsingledispatch(DispatchingRequest request, ClaimsPrincipal user);
        Task<DispatchingTimeStartEndResponse> addtimestartend(string timeStart, string timeEnd, int id, ClaimsPrincipal user);
        Task<DispatchingResponse> request(ClaimsPrincipal user, string status, int documentId, string? note = null);
        Task<DispatchingResponse> updatedispatch(ClaimsPrincipal user, DispatchingRequest request, int id);
        Task<DispatchingResponse> hidedispatch(int id);
        Task<DispatchingResponse> deletedispatch(int id);
    }
}
