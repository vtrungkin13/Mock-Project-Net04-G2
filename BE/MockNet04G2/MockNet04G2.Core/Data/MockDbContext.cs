﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Core.Data
{
    public class MockDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Donate> Donates { get; set; }
        public DbSet<Cooperate> Cooperates { get; set; }
        public DbSet<Organization> Organizations { get; set; }


        public MockDbContext() { }

        public MockDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string nonUserPassword = BCrypt.Net.BCrypt.HashPassword("G7t@f9Xq");
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "NonUser", Email = "NonUser@gmail.com", Password = nonUserPassword, Phone = "000000000", Dob = DateTime.MinValue, Role = Common.Enums.RoleEnum.User }
            );
            modelBuilder.Entity<User>().HasData(
                new User{ Id = 2 ,Name = "Admin", Email = "Admin@gmail.com", Password = hashedPassword, Phone = "0375769058", Dob = new DateTime(2002,01,11), Role = Common.Enums.RoleEnum.Admin }
            );


            modelBuilder.Entity<Organization>().HasData(
               new Organization
               {
                   Id = 1,
                   Name = "Organization A",
                   Phone = "123-456-7890",
                   Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRENF9uv9UWIWWbExsgj7XyX58xMFAOZTzUSQ&s"
               },
               new Organization
               {
                   Id = 2,
                   Name = "Organization B",
                   Phone = "098-765-4321",
                   Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRENF9uv9UWIWWbExsgj7XyX58xMFAOZTzUSQ&s"
               },
               new Organization
               {
                   Id = 3,
                   Name = "Organization C",
                   Phone = "555-555-5555",
                   Logo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRENF9uv9UWIWWbExsgj7XyX58xMFAOZTzUSQ&s"
               }
             );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Build configuration manually
                var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                // Get the connection string from the configuration
                var connectionString = config.GetConnectionString("DefaultConnection");

                // Use the connection string
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


    }
}
