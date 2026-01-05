using Microsoft.EntityFrameworkCore;

namespace WpfSQLite.Models
{
    public class ModelContext : DbContext
    {
        // DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "음료" },
                new Category { CategoryId = 2, Name = "과자" });

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "콜라", CategoryId = 1 },
                new Product { ProductId = 2, Name = "사이다", CategoryId = 1 },
                new Product { ProductId = 3, Name = "고래밥", CategoryId = 2 },
                new Product { ProductId = 4, Name = "치토스", CategoryId = 2 },
                new Product { ProductId = 5, Name = "양파링", CategoryId = 2 });
        }
    }
}
