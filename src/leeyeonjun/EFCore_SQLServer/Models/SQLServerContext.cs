using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace EFCore_SQLServer.Models
{
    public class SQLServerContext : DbContext
    {
        public DbSet<Pizza> Pizzas => Set<Pizza>();
        public DbSet<Topping> Toppings => Set<Topping>();
        public DbSet<Sauce> Sauces => Set<Sauce>();

        public DbSet<PizzaTopping> PizzaTopping { get; set; }

        private string _connectionString = string.Empty;

        public SQLServerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity Setting
            modelBuilder.Entity<Pizza>()
                .HasMany(e => e.Toppings)
                .WithMany(e => e.Pizzas)
                .UsingEntity<PizzaTopping>(
                    l => l.HasOne<Topping>().WithMany().HasForeignKey(e => e.ToppingId),
                    r => r.HasOne<Pizza>().WithMany().HasForeignKey(e => e.PizzaId));

            //modelBuilder.Entity<Pizza>().HasData(
            //    new Pizza { Id = 1, Name = "고구마피자"});
            modelBuilder.Entity<Sauce>().HasData(
                new Sauce { Id = 1, Name = "고구마소스", IsVegan = true });
            modelBuilder.Entity<Topping>().HasData(
                new Topping { Id = 1, Name = "감자", Calories = (decimal)8.32 },
                new Topping { Id = 2, Name = "고구마", Calories = (decimal)10.11 });
        }
    }
}
