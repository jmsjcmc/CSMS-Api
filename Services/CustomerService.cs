using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Queries;
using CSMapi.Interfaces;
using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Services
{
    public class CustomerService : BaseService , ICustomerService
    {
        private readonly CustomerValidator _customerValidator;
        private readonly CustomerQueries _customerQueries;
        public CustomerService(AppDbContext context, IMapper mapper, CustomerValidator customerValidator, CustomerQueries customerQueries) : base (context, mapper)
        {
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
            return await PaginationHelper.paginateandmap<Customer, CustomerResponse>(query, pageNumber, pageSize, _mapper);
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
            var customer = await getcustomerdata(id);

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

            return await customerResponse(customer.Id);
        }
        // [HttpPatch("customer/update/{id}")]
        public async Task<CustomerResponse> updatecustomer(CustomerRequest request, int id)
        {
            var customer = await getcustomerid(id);

            _mapper.Map(request, customer);
            await _context.SaveChangesAsync();

            return await customerResponse(customer.Id);
        }
        // [HttpPatch("customer/toggle-active")]
        public async Task<CustomerResponse> toggleactive(int id)
        {
            var customer = await getcustomerid(id);

            customer.Active = !customer.Active;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return await customerResponse(customer.Id);
        }
        // [HttpPatch("customer/hide/{id}")]
        public async Task<CustomerResponse> hidecustomer(int id)
        {
            var customer = await getcustomerid(id);

            customer.Removed = true;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return await customerResponse(customer.Id);
        }
        // [HttpDelete("customer/delete/{id}")]
        public async Task<CustomerResponse> deletecustomer(int id)
        {
            var customer = await getcustomerid(id);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return await customerResponse(customer.Id);
        }
        // Helper
        private async Task<Customer?> getcustomerid(int id)
        {
            return await _customerQueries.patchmethodcustomerid(id);
        }
        private async Task<Customer?> getcustomerdata(int id)
        {
            return await _customerQueries.getmethodcustomerid(id);
        }
        private async Task<CustomerResponse> customerResponse(int id)
        {
            var response = await getcustomerdata(id);
            return _mapper.Map<CustomerResponse>(response);
        }
    }
}
