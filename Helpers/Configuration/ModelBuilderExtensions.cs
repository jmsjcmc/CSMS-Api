using CSMapi.Models;
using Microsoft.EntityFrameworkCore;

namespace CSMapi.Helpers.Configuration
{
    public static class ModelBuilderExtensions
    {
        public static void  Seed(this ModelBuilder modelBuilder)
        {
            // Seeding cold storage data
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
            // Seeding initial user data
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
}
