using CSMapi.Helpers;
using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ColdStorage> Coldstorages { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dispatching> Dispatchings { get; set; }
        public DbSet<DispatchingDetail> Dispatchingdetails { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<LeasedPresmises> Leasedpremises { get; set; }
        public DbSet<Receiving> Receivings { get; set; }
        public DbSet<ReceivingDetail> Receivingdetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Repalletization> Repalletizations { get; set; }
        public DbSet<RepalletizationDetail> Repalletizationdetails { get; set; }
        public DbSet<PalletPosition> Palletpositions { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Receiving>(d =>
            {
                d.HasOne(r => r.Document)
                .WithMany(r => r.Receiving)
                .HasForeignKey(r => r.Documentid)
                .OnDelete(DeleteBehavior.Restrict);

                d.HasOne(r => r.Product)
                .WithMany(r => r.Receiving)
                .HasForeignKey(r => r.Productid)
                .OnDelete(DeleteBehavior.Restrict);

                d.HasMany(r => r.Receivingdetails)
                .WithOne(r => r.Receiving)
                .HasForeignKey(r => r.Receivingid)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ReceivingDetail>(d =>
            {
                d.HasOne(r => r.Pallet)
                .WithMany(r => r.ReceivingDetail)
                .HasForeignKey(r => r.Palletid)
                .OnDelete(DeleteBehavior.Restrict);

                d.HasOne(r => r.PalletPosition)
                .WithMany(r => r.ReceivingDetail)
                .HasForeignKey(r => r.Positionid)
                .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Dispatching>(d =>
            {
                d.HasOne(d => d.Product)
                .WithMany(d => d.Dispatching)
                .HasForeignKey(d => d.Productid);

                d.HasOne(d => d.Document)
                .WithMany(d => d.Dispatching)
                .HasForeignKey(d => d.Documentid);
            });

            modelBuilder.Entity<DispatchingDetail>(d =>
            {
                d.HasOne(d => d.Pallet)
                .WithMany(d => d.DispatchDetail)
                .HasForeignKey(d => d.Palletid)
                .OnDelete(DeleteBehavior.NoAction);

                d.HasOne(d => d.Dispatching)
                .WithMany(d => d.Dispatchingdetails)
                .HasForeignKey(d => d.Dispatchingid)
                .OnDelete(DeleteBehavior.Restrict); 

                d.HasOne(d => d.Receivingdetail)
                .WithMany(d => d.DispatchingDetail)
                .HasForeignKey(d => d.Receivingdetailid)
                .OnDelete(DeleteBehavior.NoAction);

                d.HasOne(d => d.PalletPosition)
                .WithMany(d => d.DispatchingDetail)
                .HasForeignKey(d => d.Positionid)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasMany(c => c.Leasedpremises)
                .WithOne(l => l.Contract)
                .HasForeignKey(l => l.Contractid)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Customer)
                .WithMany(p => p.Product)
                .HasForeignKey(p => p.Customerid);
            });

            modelBuilder.Entity<PalletPosition>(d =>
            {
                d.HasOne(p => p.Coldstorage)
                .WithMany(p => p.Palletposition)
                .HasForeignKey(p => p.Csid);
            });

            modelBuilder.Entity<RepalletizationDetail>(d =>
            {
                d.HasOne(p => p.Repalletization)
                .WithMany(p => p.RepalletizationDetail)
                .HasForeignKey(p => p.Repalletizationid)
                .OnDelete(DeleteBehavior.Restrict);

                d.HasOne(p => p.Receivingdetail)
                .WithOne(p => p.RepalletizationDetail)
                .HasForeignKey<RepalletizationDetail>(p => p.Receivingdetailid);
            });

            modelBuilder.Entity<ColdStorage>().HasData(
                new ColdStorage
                {
                    Id = 1,
                    Csnumber = "1",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 2,
                    Csnumber = "2",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 3,
                    Csnumber = "3",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 4,
                    Csnumber = "4",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 5,
                    Csnumber = "5",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 6,
                    Csnumber = "6",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 7,
                    Csnumber = "7",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 8,
                    Csnumber = "8",
                    Active = true
                },
                new ColdStorage
                {
                    Id = 9,
                    Csnumber = "9",
                    Active = true
                });
            // Seeding initial role data
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Rolename = "Admin",
                },
                new Role
                {
                    Id = 2,
                    Rolename = "User"
                },
                new Role
                {
                    Id = 3,
                    Rolename = "Approver"
                });
            // Seeding inital user data 
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Firstname = "James Jecemeco",
                    Lastname = "Tabilog",
                    Username = "211072",
                    Password = BCrypt.Net.BCrypt.HashPassword("@temp123"),
                    Businessunit = "ABFI Central Office",
                    Businessunitlocation = "Binugao, Toril, Davao City",
                    Department = "Cisdevo",
                    Position = "Software Developer",
                    Role = "Admin, User, Approver",
                    Createdon = TimeHelper.GetPhilippineStandardTime()
                },
                new User
                {
                    Id = 2,
                    Firstname = "Shiela",
                    Lastname = "Hernando",
                    Username = "211073",
                    Password = BCrypt.Net.BCrypt.HashPassword("@temp123"),
                    Businessunit = "SubZero Ice and Cold Storage Inc",
                    Businessunitlocation = "Binugao, Toril, Davao City",
                    Department = "Executive",
                    Position = "Senior Operations Manager",
                    Role = "Approver",
                    Createdon = TimeHelper.GetPhilippineStandardTime()
                },
                new User
                {
                    Id = 3,
                    Firstname = "Jerecho",
                    Lastname = "Asilum",
                    Username = "211028",
                    Password = BCrypt.Net.BCrypt.HashPassword("@temp123"),
                    Businessunit = "ABFI Central Office",
                    Businessunitlocation = "Binugao, Toril, Davao City",
                    Department = "Cisdevo",
                    Position = "Software Developer",
                    Role = "Admin, User, Approver",
                    Createdon = TimeHelper.GetPhilippineStandardTime()
                });
            }
        }
    }


