using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSMapi.Interfaces
{
    public interface IReceivingService
    {
        Task<Pagination<ReceivingResponse>> allreceivings(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null,
            string? category = null,
            string? status = null);
        Task<Pagination<ReceivingResponse>> allpendings(
            int pageNumber = 1,
            int pageSize = 10,
            string? category = null);
        Task<ReceivingResponse> getreceiving(int id);
        Task<DocumentNumberResponse> generatedocumentnumber(string category);
        Task<ReceivingResponse> addreceiving(ReceivingRequest request, IFormFile file, ClaimsPrincipal user);
        Task<ReceivingResponse> request(ClaimsPrincipal user, string status, int documentId, string? note = null);
        Task<ReceivingResponse> updatereceiving(ReceivingRequest request, IFormFile? file, int id, ClaimsPrincipal user);
        Task hidereceiving(int id);
        Task deletereceiving(int id);
    }
}
