using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Models
{
    public class PizzaContext : DbContext
    {
        //private readonly string _connectionString;

        //public PizzaContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(_connectionString);
        //}

        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }


        public DbSet<Pizza> Pizzas => Set<Pizza>();
        public DbSet<Topping> Toppings => Set<Topping>();
        public DbSet<Sauce> Sauces => Set<Sauce>();

    }
}
