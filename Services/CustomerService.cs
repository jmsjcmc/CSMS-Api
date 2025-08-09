using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;


namespace CSMapi.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly CustomerValidator _customerValidator;
        private readonly CustomerQueries _customerQueries;
        public CustomerService(AppDbContext context, IMapper mapper, CustomerValidator customerValidator, CustomerQueries customerQueries) : base(context, mapper)
        {
            _customerValidator = customerValidator;
            _customerQueries = customerQueries;
        }
        // [HttpGet("customers")]
        public async Task<Pagination<CustomerResponse>> AllCustomers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _customerQueries.CustomerOnlyQuery(searchTerm);
            return await PaginationHelper.PaginateAndMap<Customer, CustomerResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("customers/active-companies")]
        public async Task<List<CompanyNameOnlyResponse>> AllActiveCompanyNames()
        {
            var companies = await _customerQueries.ActiveCustomersQuery();

            return _mapper.Map<List<CompanyNameOnlyResponse>>(companies);
        }
        // [HttpGet("customers/active")]
        public async Task<List<CustomerResponse>> AllActiveCustomers()
        {
            var customers = await _customerQueries.ActiveCustomersQuery();

            return _mapper.Map<List<CustomerResponse>>(customers);
        }
        // [HttpGet("customer/{id}")]
        public async Task<CustomerResponse> GetCustomer(int id)
        {
            var customer = await GetCustomerData(id);

            return _mapper.Map<CustomerResponse>(customer);
        }
        // [HttpPost("customer")]
        public async Task<CustomerResponse> AddCustomer(CustomerRequest request)
        {
            _customerValidator.ValidateCustomerRequest(request);

            var customer = _mapper.Map<Customer>(request);
            customer.Active = true;

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return await CustomerResponse(customer.Id);
        }
        // [HttpPatch("customer/update/{id}")]
        public async Task<CustomerResponse> UpdateCustomer(CustomerRequest request, int id)
        {
            var customer = await GetCustomerId(id);

            _mapper.Map(request, customer);
            await _context.SaveChangesAsync();

            return await CustomerResponse(customer.Id);
        }
        // [HttpPatch("customer/toggle-active")]
        public async Task<CustomerResponse> ToggleActive(int id)
        {
            var customer = await GetCustomerId(id);

            customer.Active = !customer.Active;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return await CustomerResponse(customer.Id);
        }
        // [HttpPatch("customer/hide/{id}")]
        public async Task<CustomerResponse> HideCustomer(int id)
        {
            var customer = await GetCustomerId(id);

            customer.Removed = !customer.Removed;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return await CustomerResponse(customer.Id);
        }
        // [HttpDelete("customer/delete/{id}")]
        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await GetCustomerId(id);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return await CustomerResponse(customer.Id);
        }
        // Helpers
        private async Task<Customer?> GetCustomerId(int id)
        {
            return await _customerQueries.PatchCustomerId(id);
        }
        private async Task<Customer?> GetCustomerData(int id)
        {
            return await _customerQueries.GetCustomerId(id);
        }
        private async Task<CustomerResponse> CustomerResponse(int id)
        {
            var response = await GetCustomerData(id);
            return _mapper.Map<CustomerResponse>(response);
        }
    }
}
