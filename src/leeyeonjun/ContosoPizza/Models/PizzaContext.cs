using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

        public DbSet<PizzaTopping> PizzaTopping { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Pizza>()
            //    .HasMany(e => e.Toppings)
            //    .WithMany(e => e.Pizzas)
            //    .UsingEntity<PizzaTopping>();

            modelBuilder.Entity<Pizza>()
                .HasMany(e => e.Toppings)
                .WithMany(e => e.Pizzas)
                .UsingEntity<PizzaTopping>(
                    l => l.HasOne<Topping>().WithMany().HasForeignKey(e => e.ToppingId),
                    r => r.HasOne<Pizza>().WithMany().HasForeignKey(e => e.PizzaId));

            //modelBuilder.Entity<PizzaTopping>()
            //    .HasKey(d => new { d.PizzasId, d.ToppingsId });
        }
    }
}
