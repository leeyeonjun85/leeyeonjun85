using Microsoft.EntityFrameworkCore;

namespace GetStartedWPF
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=products.db");
            optionsBuilder.UseLazyLoadingProxies();
        }

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
