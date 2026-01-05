using Microsoft.EntityFrameworkCore;
using SQLServer.Models;

namespace SQLServer.Data
{
    public class ContosoPetsContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //셈플이기 때문에 여기에 connection string을 입력
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=true");
        }
    }
}
