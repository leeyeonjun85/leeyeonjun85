using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Utiles;

namespace Edcore.Models
{
    public class SeojungriOracleContext : DbContext
    {
        // DbSet
        public DbSet<YeonjunTest> yeonjunTest { get; set; }

        public SeojungriOracleContext(DbContextOptions<SeojungriOracleContext> options) : base(options) { }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseOracle(MyUtiles.GetJsonModel().ConnectionStrings.SeojungriOracle);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YeonjunTest>().HasData(
                new YeonjunTest { Id = 1, Name = "이명박", Years = 12 },
                new YeonjunTest { Id = 2, Name = "윤석렬", Years = 18 });
        }
    }

    public class YeonjunTest
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Years { get; set; }
    }
}
