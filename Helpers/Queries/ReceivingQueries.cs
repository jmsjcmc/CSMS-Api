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
        // Query for fetching all receivings with optional filter for document number, category, and status
        public async Task<IQueryable<Receiving>> receivingsquery(
            string? searchTerm = null,
            int? categoryId = null,
            string? status = null)
        {
            if (categoryId.HasValue)
            {
                var query = _context.Receivings
                    .AsNoTracking()
                   .Include(r => r.Document)
                   .Include(r => r.Product)
                   .ThenInclude(p => p.Category)
                   .Include(r => r.Product)
                   .ThenInclude(p => p.Customer)
                   .Include(r => r.Receivingdetails)
                   .Where(r => !r.Removed && r.Product.Category.Id == categoryId)
                   .OrderByDescending(r => r.Createdon)
                   .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(r => r.Document.Documentno == searchTerm);
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
                       .Include(r => r.Document)
                       .Include(r => r.Product)
                       .ThenInclude(p => p.Category)
                       .Include(r => r.Product)
                       .ThenInclude(p => p.Customer)
                       .Include(r => r.Receivingdetails)
                       .Where(r => !r.Removed)
                       .OrderByDescending(r => r.Createdon)
                       .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(r => r.Document.Documentno == searchTerm);
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
                    .Include(r => r.Document)
                    .Include(r => r.Product)
                    .ThenInclude(p => p.Category) 
                    .Include(r => r.Product)
                    .ThenInclude(p => p.Customer)
                    .Include(r => r.Receivingdetails)
                    .Where(r => r.Pending && !r.Removed && r.Product.Category.Id == id)
                    .OrderByDescending(r => r.Createdon)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Receivings
                        .AsNoTracking()
                        .Include(r => r.Document)
                        .Include(r => r.Product)
                        .ThenInclude(p => p.Category)
                        .Include(r => r.Product)
                        .ThenInclude(p => p.Customer)
                        .Include(r => r.Receivingdetails)
                        .Where(r => r.Pending && !r.Removed)
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
                    .Include(r => r.Product)
                    .ThenInclude(p => p.Category)
                    .Include(r => r.Product)
                    .ThenInclude(p => p.Customer)
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
                   .Include(r => r.Product)
                   .ThenInclude(p => p.Category)
                   .Include(r => r.Product)
                   .ThenInclude(p => p.Customer)
                   .Include(r => r.Receivingdetails)
                   .ThenInclude(r => r.Pallet)
                   .Include(r => r.Receivingdetails)
                   .ThenInclude(r => r.PalletPosition)
                   .ThenInclude(p => p.Coldstorage)
                   .Include(r => r.Requestor)
                   .Include(r => r.Approver)
                   .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
