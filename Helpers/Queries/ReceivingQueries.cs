using AutoMapper;
using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CSMapi.Helpers.Queries
{
    public class ReceivingQueries
    {
        private readonly AppDbContext _context;
        public ReceivingQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all receivings in list
        public async Task<List<Receiving>> receivingslist()
        {
            return await _context.Receivings
                .Include(r => r.Document)
                .Include(r => r.Product)
                .ThenInclude(p => p.Category)
                .Include(r => r.Product)
                .ThenInclude(p => p.Customer)
                .Include(r => r.Requestor)
                .Include(r => r.Approver)
                .Include(r => r.Receivingdetails)
                .ThenInclude(r => r.Pallet)
                .Include(r => r.Receivingdetails)
                .ThenInclude(r => r.PalletPosition)
                .Where(r => !r.Removed)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }
        // Query for fetching receivings for receiving management display
        public IQueryable<Receiving> receivingdisplayquery(
            string? searchTerm = null,
            int? categoryId = null,
            string? status = null)
        {
            if (categoryId.HasValue)
            {
                var query = _context.Receivings
                    .AsNoTracking()
                    .Where(r => !r.Removed && r.Product.Category.Id == categoryId)
                    .Include(r => r.Document)
                    .Include(r => r.Product.Customer)
                    .Include(r => r.Requestor)
                    .OrderByDescending(r => r.Id)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(r => r.Document.Documentno.Contains(searchTerm) ||
                    r.Product.Customer.Companyname.Contains(searchTerm) ||
                    r.Product.Productname.Contains(searchTerm));
                }
                if (!string.IsNullOrWhiteSpace(status))
                {
                    switch (status.ToLower())
                    {
                        case "pending":
                            query = query.Where(r => r.Pending);
                            break;
                        case "received":
                            query = query.Where(r => r.Received);
                            break;
                        case "declined":
                            query = query.Where(r => r.Declined);
                            break;
                    }
                }

                return query;
            }
            else
            {
                var query = _context.Receivings
                    .AsNoTracking()
                    .Where(r => !r.Removed)
                    .Include(r => r.Document)
                    .Include(r => r.Product.Customer)
                    .Include(r => r.Requestor)
                    .OrderByDescending(r => r.Id)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(r => r.Document.Documentno.Contains(searchTerm) ||
                    r.Product.Customer.Companyname.Contains(searchTerm) ||
                    r.Product.Productname.Contains(searchTerm));
                }

                if (!string.IsNullOrWhiteSpace(status))
                {
                    switch (status.ToLower())
                    {
                        case "pending":
                            query = query.Where(r => r.Pending);
                            break;
                        case "received":
                            query = query.Where(r => r.Received);
                            break;
                        case "declined":
                            query = query.Where(r => r.Declined);
                            break;
                    }
                }

                return query;
            }
        }

        // Query for fetching all receivings with optional filter for document number, category, and status
        public IQueryable<Receiving> receivingsquery(
            string? searchTerm = null,
            int? categoryId = null,
            string? status = null)
        {
            if (categoryId.HasValue)
            {
                var query = _context.Receivings
                    .AsNoTracking()
                    .Where(r => !r.Removed && r.Product.Category.Id == categoryId)
                    .Include(r => r.Document)
                    .Include(r => r.Product.Category)
                    .Include(r => r.Product.Customer)
                    .Include(r => r.Receivingdetails)
                    .OrderByDescending(r => r.Id)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(r => r.Document.Documentno.Contains(searchTerm) || 
                    r.Product.Customer.Companyname.Contains(searchTerm) ||
                    r.Product.Productname.Contains(searchTerm));
                }
                if (!string.IsNullOrWhiteSpace(status))
                {
                    switch (status.ToLower())
                    {
                        case "pending":
                            query = query.Where(r => r.Pending);
                            break;
                        case "received":
                            query = query.Where(r => r.Received);
                            break;
                        case "declined":
                            query = query.Where(r => r.Declined);
                            break;
                    }
                }

                return query;
            }
            else
            {
                var query = _context.Receivings
                       .AsNoTracking()
                       .Where(r => !r.Removed)
                       .Include(r => r.Document)
                       .Include(r => r.Product.Category)
                       .Include(r => r.Product.Customer)
                       .Include(r => r.Receivingdetails)
                       .OrderByDescending(r => r.Createdon)
                       .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(r => r.Document.Documentno.Contains(searchTerm) ||
                    r.Product.Customer.Companyname.Contains(searchTerm) || 
                    r.Product.Productname.Contains(searchTerm));
                }

                if (!string.IsNullOrWhiteSpace(status))
                {
                    switch (status.ToLower())
                    {
                        case "pending":
                            query = query.Where(r => r.Pending);
                            break;
                        case "received":
                            query = query.Where(r => r.Received);
                            break;
                        case "declined":
                            query = query.Where(r => r.Declined);
                            break;
                    }
                }

                return query;
            }
        }
        // Query for fetching all pending receivings with optional filter for category
        public IQueryable<Receiving> pendingreceivingsquery(int? id = null)
        {
            if (id.HasValue)
            {
                var query = _context.Receivings
                    .AsNoTracking()
                    .Where(r => r.Pending && !r.Removed && r.Product.Category.Id == id)
                    .Include(r => r.Document)
                    .Include(r => r.Product.Category)
                    .Include(r => r.Product.Customer)
                    .Include(r => r.Receivingdetails)
                    .OrderByDescending(r => r.Createdon)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Receivings
                        .AsNoTracking()
                        .Where(r => r.Pending && !r.Removed)
                        .Include(r => r.Document)
                        .Include(r => r.Product.Category)
                        .Include(r => r.Product.Customer)
                        .Include(r => r.Receivingdetails)
                        .OrderByDescending(r => r.Createdon)
                        .AsQueryable();

                return query;
            }
        }
        // Query for fetching specific receiving for GET method
        public async Task<Receiving?> getmethodreceivingid(int id)
        {
            return await _context.Receivings
                    .AsNoTracking()
                    .Include(r => r.Document)
                    .Include(r => r.Product.Category)
                    .Include(r => r.Product.Customer)
                    .Include(r => r.Receivingdetails)
                    .ThenInclude(r => r.Pallet)
                    .Include(r => r.Receivingdetails)
                    .ThenInclude(r => r.PalletPosition)
                    .ThenInclude(p => p.Coldstorage)
                    .Include(r => r.Requestor)
                    .Include(r => r.Approver)
                    .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching specific receiving request for PATCH/PUT/DELETE methods
        public async Task<Receiving?> patchmethodreceivingid(int id)
        {
            return await _context.Receivings
                   .Include(r => r.Document)
                   .Include(r => r.Product.Category)
                   .Include(r => r.Product.Customer)
                   .Include(r => r.Receivingdetails)
                   .ThenInclude(r => r.Pallet)
                   .Include(r => r.Receivingdetails)
                   .ThenInclude(r => r.PalletPosition)
                   .ThenInclude(p => p.Coldstorage)
                   .Include(r => r.Requestor)
                   .Include(r => r.Approver)
                   .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching all receiving details 
        public async Task<List<ReceivingDetail>> receivingdetaillist(int? productId)
        {
            if (productId.HasValue)
            {
                return await _context.Receivingdetails
                    .AsNoTracking()
                    .Where(r => r.Receiving.Productid == productId)
                    .Include(r => r.PalletPosition.Coldstorage)
                    .Include(r => r.Pallet)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Receivingdetails
                   .AsNoTracking()
                   .Include(r => r.PalletPosition.Coldstorage)
                   .OrderByDescending(r => r.Id)
                   .ToListAsync();
            }
        }
    }
}
