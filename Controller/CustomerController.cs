using AutoMapper;
using CSMapi.Helpers;
using CSMapi.Helpers.Excel;
using CSMapi.Models;
using CSMapi.Services;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Controller
{
    public class CustomerController : BaseApiController
    {
        private readonly CustomerExcel _customerExcel;
        private readonly CustomerService _customerService;
        private readonly ReceivingService _receivingService;
        private readonly DispatchingService _dispatchingService;
        public CustomerController(AppDbContext context, IMapper mapper, CustomerService customerService, CustomerExcel customerExcel, ReceivingService receivingService, DispatchingService dispatchingService) : base (context, mapper)
        {
            _customerExcel = customerExcel;
            _customerService = customerService;
            _receivingService = receivingService;
            _dispatchingService = dispatchingService;
        }

        // Count by date
        [HttpGet("customers/count-by-date")]
        public async Task<ActionResult<IEnumerable<DailyCountDto>>> ReceivedAndDispatchCountByDate()
        {
            if (_receivingService == null || _dispatchingService == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Services are not initialized.");

            var today = DateTime.Today;
            var startDate = today.AddDays(-30);
            var result = new List<DailyCountDto>();

            for (var date = startDate; date <= today; date = date.AddDays(1))
            {
                var received = await _receivingService.ReceivedCountByDate(date);
                var dispatched = await _dispatchingService.DispatchedCountByDate(date);

                // Skip if both are zero
                if (received == 0 && dispatched == 0)
                    continue;

                result.Add(new DailyCountDto
                {
                    Date = date,
                    Received = received,
                    Dispatched = dispatched
                });
            }

            return Ok(result);
        }


        // Fetch all customers
        [HttpGet("customers")]
        public async Task<ActionResult<Pagination<CustomerResponse>>> AllCustomers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _customerService.AllCustomers(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all active customers (name and id only)
        [HttpGet("customers/active-companies")]
        public async Task<ActionResult<List<CompanyNameOnlyResponse>>> AllActiveCompanies()
        {
            try
            {
                var response = await _customerService.AllActiveCompanyNames();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all active customers
        [HttpGet("customers/active")]
        public async Task<ActionResult<List<CustomerResponse>>> AllActiveCustomers()
        {
            try
            {
                var response = await _customerService.AllActiveCustomers();
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Customer Template
        [HttpGet("customers/template")]
        public async Task<ActionResult> CustomerTemplate()
        {
            try
            {
                var file = await Task.Run(() => _customerExcel.generatecustomertemplate());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerTemplate.xlsx");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Export customers
        [HttpGet("customers/export")]
        public async Task<ActionResult> ExportCustomers()
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
        public async Task<ActionResult<CustomerResponse>> GetCustomer(int id)
        {
            try
            {
                var response = await _customerService.GetCustomer(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import customers
        [HttpPost("customers/import")]
        public async Task<ActionResult<List<CustomerResponse>>> ImportCustomers(IFormFile file)
        {
            try
            {
                var customers = _customerExcel.importcustomer(file);
                await _context.BulkInsertAsync(customers);

                var response = _mapper.Map<List<CustomerResponse>>(customers);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create customer
        [HttpPost("customer")]
        public async Task<ActionResult<CustomerResponse>> AddCustomer([FromBody] CustomerRequest request)
        {
            try
            {
                var response = await _customerService.AddCustomer(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific customer
        [HttpPatch("customer/update/{id}")]
        public async Task<ActionResult<CustomerResponse>> UpdateCustomer([FromBody] CustomerRequest request, int id)
        {
            try
            {
                var response = await _customerService.UpdateCustomer(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle specific customer active status to true/false
        [HttpPatch("customer/toggle-active")]
        public async Task<ActionResult<CustomerResponse>> ToggleActive(int id)
        {
            try
            {
                var response = await _customerService.ToggleActive(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific customer without removing in Database (Soft Delete)
        [HttpPatch("customer/hide/{id}")]
        public async Task<ActionResult<CustomerResponse>> HideCustomer(int id)
        {
            try
            {
                var response = await _customerService.HideCustomer(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific customer in Database
        [HttpDelete("customer/delete/{id}")]
        public async Task<ActionResult<CustomerResponse>> DeleteCustomer(int id)
        {
            try
            {
                var response = await _customerService.DeleteCustomer(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
