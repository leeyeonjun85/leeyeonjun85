using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Drawing;
using System;

namespace EFCore_SQLServer.Models
{
    public class SQLServerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Data Source=(localdb){Path.DirectorySeparatorChar}MSSQLLocalDB;Initial Catalog=testDB;Integrated Security=true");

            //jdbc:sqlserver://;servername=(localdb)\MSSQLLocalDB;encrypt=true;integratedSecurity=true;
        }
    }
}
