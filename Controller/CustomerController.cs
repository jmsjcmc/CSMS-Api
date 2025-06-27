using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Models;
using CSMapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Controller
{
    public class CustomerController : BaseApiController
    {
        private readonly CustomerExcel _customerExcel;
        private readonly CustomerService _customerService;
        public CustomerController(AppDbContext context, IMapper mapper, CustomerService customerService, CustomerExcel customerExcel) : base (context, mapper)
        {
            _customerExcel = customerExcel;
            _customerService = customerService;
        }
        // Fetch all customers
        [HttpGet("customers")]
        public async Task<ActionResult<Pagination<CustomerResponse>>> allcustomers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _customerService.allcustomers(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all active customers
        [HttpGet("customers/active")]
        public async Task<ActionResult<List<CustomerResponse>>> allactivecustomers()
        {
            try
            {
                var response = await _customerService.allactivecustomers();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Customer Template
        [HttpGet("customers/template")]
        public async Task<ActionResult> customertemplate()
        {
            try
            {
                var file = _customerExcel.generatecustomertemplate();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerTemplate.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export customers
        [HttpGet("customers/export")]
        public async Task<ActionResult> exportcustomers()
        {
            try
            {
                var customers = await _context.Customers
                    .ToListAsync();

                var file = _customerExcel.exportcustomers(customers);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific customer
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<CustomerResponse>> getcustomer(int id)
        {
            try
            {
                var response = await _customerService.getcustomer(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import customers
        [HttpPost("customers/import")]
        public async Task<ActionResult<List<CustomerResponse>>> importcustomers(IFormFile file)
        {
            try
            {
                var customers = _customerExcel.importcustomer(file);
                await _context.Customers.AddRangeAsync(customers);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<List<CustomerResponse>>(customers);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create customer
        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResponse>> addcustomer([FromBody] CustomerRequest request)
        {
            try
            {
                var response = await _customerService.addcustomer(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific customer
        [HttpPatch("customer/update/{id}")]
        public async Task<ActionResult<CustomerResponse>> updatecustomer([FromBody] CustomerRequest request, int id)
        {
            try
            {
                var response = await _customerService.updatecustomer(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific customer active status to true/false
        [HttpPatch("customer/toggle-active")]
        public async Task<ActionResult<CustomerResponse>> toggleactive(int id)
        {
            try
            {
                var response = await _customerService.toggleactive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific customer without removing in Database (Soft Delete)
        [HttpPatch("customer/hide/{id}")]
        public async Task<ActionResult> hidecustomer(int id)
        {
            try
            {
                await _customerService.hidecustomer(id);
                return Ok("Customer removed.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific customer in Database
        [HttpDelete("customer/delete/{id}")]
        public async Task<ActionResult> deletecustomer(int id)
        {
            try
            {
                await _customerService.deletecustomer(id);
                return Ok("Customer removed permanently.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
