using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Queries
{
    public class UserQueries
    {
        private readonly AppDbContext _context;
        public UserQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all users with optional filter for last name
        public IQueryable<User?> usersquery (string? searchTerm = null)
        {
            var query = _context.Users
                    .AsNoTracking()
                    .OrderByDescending(u => u.Createdon)
                    .Where(u => !u.Removed)
                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.Lastname == searchTerm);
            }
            return query;
        }
        // Query for fetching all roles
        public async Task<List<Role>> rolesquery()
        {
            return await _context.Roles
                   .AsNoTracking()
                   .OrderByDescending(r => r.Id)
                   .Where(r => !r.Removed)
                   .ToListAsync();
        }
        // Query for fetching all users with business unit "SubZero Ice and Cold Storage Inc"
        public async Task<List<User>> lessorsquery()
        {
            return await _context.Users
                    .Where(u => u.Businessunit == "SubZero Ice and Cold Storage Inc")
                    .AsNoTracking()
                    .ToListAsync();
        }
        // Query for fetching specific user for GET method
        public async Task<User?> getmethoduserid(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific role for GET method
        public async Task<Role?> getmethodroleid(int id)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching specific user for PATCH/PUT/DELETE methods
        public async Task<User?> patchmethoduserid(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific role for PATCH/PUT/DELETE methods
        public async Task<Role?> patchmethodroleid(int id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
