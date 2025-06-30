using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Interfaces
{
    public interface ICustomerService
    {
        Task<Pagination<CustomerResponse>> allcustomers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<CustomerResponse>> allactivecustomers();
        Task<CustomerResponse> getcustomer(int id);
        Task<CustomerResponse> addcustomer(CustomerRequest request);
        Task<CustomerResponse> updatecustomer(CustomerRequest request, int id);
        Task<CustomerResponse> toggleactive(int id);
        Task<CustomerResponse> hidecustomer(int id);
        Task<CustomerResponse> deletecustomer(int id);
    }
}
