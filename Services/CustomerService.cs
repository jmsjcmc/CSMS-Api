using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly CustomerValidator _customerValidator;
        private readonly CustomerQueries _customerQueries;
        public CustomerService(AppDbContext context, IMapper mapper, CustomerValidator customerValidator, CustomerQueries customerQueries)
        {
            _context = context;
            _mapper = mapper;
            _customerValidator = customerValidator;
            _customerQueries = customerQueries;
        }
        // [HttpGet("customers")]
        public async Task<Pagination<CustomerResponse>> allcustomers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _customerQueries.customeronlyquery(searchTerm);
            var totalCount = await query.CountAsync();
            var customers = await PaginationHelper.paginateandproject<Customer, CustomerResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(customers, totalCount, pageNumber, pageSize);
        }
        // [HttpGet("customers/active")]
        public async Task<List<CustomerResponse>> allactivecustomers()
        {
            var customers = await _customerQueries.activecustomersquery();

            return _mapper.Map<List<CustomerResponse>>(customers);
        }
        // [HttpGet("customer/{id}")]
        public async Task<CustomerResponse> getcustomer(int id)
        {
            var customer = await _customerQueries.getmethodcustomerid(id);

            return _mapper.Map<CustomerResponse>(customer);
        }
        // [HttpPost("customer")]
        public async Task<CustomerResponse> addcustomer(CustomerRequest request)
        {
            await _customerValidator.ValidateCustomerRequest(request);

            var customer = _mapper.Map<Customer>(request);
            customer.Active = true;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }
        // [HttpPatch("customer/update/{id}")]
        public async Task<CustomerResponse> updatecustomer(CustomerRequest request, int id)
        {
            var customer = await getcustomerid(id);

            _mapper.Map(request, customer);
            await _context.SaveChangesAsync();

            var updatedCustomer = await _customerQueries.getmethodcustomerid(customer.Id);

            return _mapper.Map<CustomerResponse>(updatedCustomer);
        }
        // [HttpPatch("customer/toggle-active")]
        public async Task<CustomerResponse> toggleactive(int id)
        {
            var customer = await getcustomerid(id);

            customer.Active = !customer.Active;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }
        // [HttpPatch("customer/hide/{id}")]
        public async Task hidecustomer(int id)
        {
            var customer = await getcustomerid(id);

            customer.Removed = true;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("customer/delete/{id}")]
        public async Task deletecustomer(int id)
        {
            var customer = await getcustomerid(id);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        // Helper
        private async Task<Customer?> getcustomerid(int id)
        {
            return await _customerQueries.patchmethodcustomerid(id);
        }
    }
}
