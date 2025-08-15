using CSMapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMapi.Interfaces
{
    public interface ICustomerService
    {

        Task<Pagination<CustomerResponse>> AllCustomers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<CompanyNameOnlyResponse>> AllActiveCompanyNames();
        Task<List<CustomerResponse>> AllActiveCustomers();
        Task<CustomerResponse> GetCustomer(int id);
        Task<CustomerResponse> AddCustomer(CustomerRequest request);
        Task<CustomerResponse> UpdateCustomer(CustomerRequest request, int id);
        Task<CustomerResponse> ToggleActive(int id);
        Task<CustomerResponse> HideCustomer(int id);
        Task<CustomerResponse> DeleteCustomer(int id);
        
    }
}
