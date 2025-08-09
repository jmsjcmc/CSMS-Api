using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class ContractQueries
    {
        private readonly AppDbContext _context;
        private readonly ContractValidator _validator;
        public ContractQueries(AppDbContext context, ContractValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching all contracts with optional filter for lesseecompany
        public IQueryable<Contract> ContractsQuery(string? searchTerm = null)
        {
            var query = _context.Contracts
                .AsNoTracking()
                .Where(c => !c.Removed)
                .Include(c => c.Creator)
                .Include(c => c.Leasedpremises)
                .OrderByDescending(c => c.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Lesseecompany.Contains(searchTerm));
            }

            return query;
        }
        // Query for fetching all contracts with optional filter for lessee company (List)
        public async Task<List<Contract>> ContractsList(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Contracts
                    .AsNoTracking()
                    .Where(c => c.Lesseecompany == searchTerm && !c.Removed)
                    .Include(c => c.Creator)
                    .Include(c => c.Leasedpremises)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();

            }
            else
            {
                return await _context.Contracts
                    .AsNoTracking()
                    .Include(c => c.Creator)
                    .Include(c => c.Leasedpremises)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific contract for GET method
        public async Task<Contract?> GetContractId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificContract(id);

            return await _context.Contracts
                .AsNoTracking()
                .Include(c => c.Creator)
                .Include(c => c.Leasedpremises)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        // Query for fetching specific contract for PATCH/PUT/DELETE methods
        public async Task<Contract?> PatchContractId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificContract(id);

            return await _context.Contracts
                .Include(c => c.Creator)
                .Include(c => c.Leasedpremises)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
