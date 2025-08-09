using CSMapi.Models;
using CSMapi.Validators;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class UserQueries
    {
        private readonly AppDbContext _context;
        private readonly UserValidator _validator;
        public UserQueries(AppDbContext context, UserValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        // Query for fetching all users with optional filter for last name
        public IQueryable<User> UsersQuery(string? searchTerm = null)
        {
            var query = _context.Users
                    .AsNoTracking()
                    .Where(u => !u.Removed)
                    .OrderByDescending(u => u.Createdon)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.Lastname == searchTerm);
            }
            return query;
        }
        // Query for fetching all roles
        public async Task<List<Role>> RolesQuery()
        {
            return await _context.Roles
                   .AsNoTracking()
                   .Where(r => !r.Removed)
                   .OrderByDescending(r => r.Id)
                   .ToListAsync();
        }
        // Query for fetching all users with business unit "SubZero Ice and Cold Storage Inc"
        public async Task<List<User>> LessorsQuery()
        {
            return await _context.Users
                    .Where(u => u.Businessunit == "SubZero Ice and Cold Storage Inc")
                    .AsNoTracking()
                    .ToListAsync();
        }
        // Query for fetching specific user for GET method
        public async Task<User?> GetUserId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificId(id);

            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific role for GET method
        public async Task<Role?> GetRoleId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificId(id);

            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching specific user for PATCH/PUT/DELETE methods
        public async Task<User?> PatchUserId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificId(id);

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific role for PATCH/PUT/DELETE methods
        public async Task<Role?> PatchRoleId(int id)
        {
            // Validate id if it exist
            await _validator.ValidateSpecificId(id);

            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
