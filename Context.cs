using csms_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace csms_backend
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option) { }
        public DbSet<User> User { get; set; }
        public DbSet<BusinessUnit> BusinessUnit { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRoleRelation> UserRole { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}
