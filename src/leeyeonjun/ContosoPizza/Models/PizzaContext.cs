using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace ContosoPizza.Models
{
    public class PizzaContext(DbContextOptions<PizzaContext> options) : DbContext(options)
    {
        public DbSet<Pizza> Pizzas => Set<Pizza>();
        public DbSet<Topping> Toppings => Set<Topping>();
        public DbSet<Sauce> Sauces => Set<Sauce>();

        public DbSet<PizzaTopping> PizzaTopping { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity Setting
            modelBuilder.Entity<Pizza>()
                .HasMany(e => e.Toppings)
                .WithMany(e => e.Pizzas)
                .UsingEntity<PizzaTopping>(
                    l => l.HasOne<Topping>().WithMany().HasForeignKey(e => e.ToppingId),
                    r => r.HasOne<Pizza>().WithMany().HasForeignKey(e => e.PizzaId));
        }
    }
}
