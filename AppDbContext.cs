using CSMapi.Helpers;
using CSMapi.Helpers.Configuration;
using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        // Pallets 
        public DbSet<ColdStorage> Coldstorages { get; set; }
        public DbSet<PalletPosition> Palletpositions { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        // Contracts 
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<LeasedPresmises> Leasedpremises { get; set; }
        // Receivings
        public DbSet<Receiving> Receivings { get; set; }
        public DbSet<ReceivingDetail> Receivingdetails { get; set; }
        // Dispatching
        public DbSet<Dispatching> Dispatchings { get; set; }
        public DbSet<DispatchingDetail> Dispatchingdetails { get; set; }
        // Cs Movement
        public DbSet<CsMovement> Csmovements { get; set; }
        // Repalletization
        public DbSet<Repalletization> Repalletizations { get; set; }
        // Document
        public DbSet<Document> Documents { get; set; }
        // Customers
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        // Identity & Access
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Seed();

        }
    }
}


