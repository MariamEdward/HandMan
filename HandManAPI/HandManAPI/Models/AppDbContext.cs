using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HandManAPI.Models
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext() : base("MyCs")
        {

        }
        public DbSet<Service> services { get; set; }
        public DbSet<HandMan> HandMen { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}