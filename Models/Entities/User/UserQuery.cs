using Microsoft.EntityFrameworkCore;

namespace csms_backend.Models.Entities
{
    // Queries primarily for users
    public class UserQuery
    {
        private readonly Context _context;
        public UserQuery(Context context)
        {
            _context = context;
        }
        // Query for fetching all paginated users with optional filter based on user firstname, lastname, and username
        public IQueryable<User> PaginatedUsers(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.User
                    .AsNoTracking()
                    .Where(u => u.FirstName.Contains(searchTerm)
                    || u.LastName.Contains(searchTerm)
                    || u.Username.Contains(searchTerm))
                    .Include(u => u.BusinessUnit)
                    .Include(u => u.UserRole)
                    .OrderByDescending(u => u.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.User
                    .AsNoTracking()
                    .Include(u => u.BusinessUnit)
                    .Include(u => u.UserRole)
                    .OrderByDescending(u => u.Id)
                    .AsQueryable();

                return query;
            }
        }
        // Query for fetching all listed users with optional filter based on user firstname, lastname, and username
        public async Task<List<User>> ListedUsers(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.User
                    .AsNoTracking()
                    .Where(u => u.FirstName.Contains(searchTerm)
                    || u.LastName.Contains(searchTerm)
                    || u.Username.Contains(searchTerm))
                    .Include(u => u.BusinessUnit)
                    .Include(u => u.UserRole)
                    .OrderByDescending(u => u.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.User
                    .AsNoTracking()
                    .Include(u => u.BusinessUnit)
                    .Include(u => u.UserRole)
                    .OrderByDescending(u => u.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific user for GET 
        public async Task<User?> GetUserById(int id)
        {
            return await _context.User
                .AsNoTracking()
                .Include(u => u.BusinessUnit)
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific user for PATCH/PUT/DELETE 
        public async Task<User?> PatchUserById(int id)
        {
            return await _context.User
               .Include(u => u.BusinessUnit)
               .Include(u => u.UserRole)
               .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
    // Queries primarily for Roles
    public class RoleQuery
    {
        private readonly Context _context;
        public RoleQuery(Context context)
        {
            _context = context;
        }
        // Query for fetching all paginated roles with optional filter for role name
        public IQueryable<Role> PaginatedRoles(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.Role
                    .AsNoTracking()
                    .Where(r => r.Name.Contains(searchTerm))
                    .Include(r => r.UserRole)
                    .OrderByDescending(r => r.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.Role
                    .AsNoTracking()
                    .Include(r => r.UserRole)
                    .OrderByDescending(r => r.Id)
                    .AsQueryable();

                return query;
            }
        }
        // Query for fetching all listed roles with optional filter for role name
        public async Task<List<Role>> ListedRoles(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Role
                    .AsNoTracking()
                    .Where(r => r.Name.Contains(searchTerm))
                    .Include(r => r.UserRole)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Role
                    .AsNoTracking()
                    .Include(r => r.UserRole)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
        }

        public async Task<List<Role>> ActiveListedRoles(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Role
                    .AsNoTracking()
                    .Where(r => r.Name.Contains(searchTerm) && r.Status == Status.Active)
                    .Include(r => r.UserRole)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Role
                    .AsNoTracking()
                    .Where(r => r.Status == Status.Active)
                    .Include(r => r.UserRole)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific role for GET
        public async Task<Role?> GetRoleById(int id)
        {
            return await _context.Role
                .AsNoTracking()
                .Include(r => r.UserRole)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching specific role for PATCH/PUT/DELETE
        public async Task<Role?> PatchRoleById(int id)
        {
            return await _context.Role
                .Include(r => r.UserRole)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
    // Queries primarily for Business units
    public class BusinessUnitQuery
    {
        // BU = Business Unit
        private readonly Context _context;
        public BusinessUnitQuery(Context context)
        {
            _context = context;
        }
        // Query for fetching all paginted Business units with optional filter for BU name and BU location
        public IQueryable<BusinessUnit> PaginatedBUs(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var query = _context.BusinessUnit
                    .AsNoTracking()
                    .Where(bu => bu.Name.Contains(searchTerm) || bu.Location.Contains(searchTerm))
                    .Include(bu => bu.User)
                    .OrderByDescending(bu => bu.Id)
                    .AsQueryable();

                return query;
            }
            else
            {
                var query = _context.BusinessUnit
                    .AsNoTracking()
                    .Include(bu => bu.User)
                    .OrderByDescending(bu => bu.Id)
                    .AsQueryable();

                return query;
            }
        }
        // Query for fetching all listed Business units with optional filter for BU name and BU location
        public async Task<List<BusinessUnit>> ListedBUs(string? searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.BusinessUnit
                    .AsNoTracking()
                    .Where(bu => bu.Name.Contains(searchTerm) || bu.Location.Contains(searchTerm))
                    .Include(bu => bu.User)
                    .ToListAsync();
            }
            else
            {
                return await _context.BusinessUnit
                    .AsNoTracking()
                    .Include(bu => bu.User)
                    .ToListAsync();
            }
        }
        // Query for fetching specific business unit for GET
        public async Task<BusinessUnit?> GetBUById(int id)
        {
            return await _context.BusinessUnit
                   .AsNoTracking()
                   .Include(bu => bu.User)
                   .FirstOrDefaultAsync(bu => bu.Id == id);
        }
        // Query for fetching specific business unit for PATCH/PUT/DELETE
        public async Task<BusinessUnit?> PatchBUById(int id)
        {
            return await _context.BusinessUnit
                   .Include(bu => bu.User)
                   .FirstOrDefaultAsync(bu => bu.Id == id);
        }
    }
}
