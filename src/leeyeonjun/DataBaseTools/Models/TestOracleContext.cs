using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Edcore.Models
{
    public class TestOracleContext : DbContext
    {
        // DbSet
        public DbSet<TestOracleModel> yeonjunTest { get; set; }

        public TestOracleContext(DbContextOptions<TestOracleContext> options) : base(options) { }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseOracle(MyUtiles.GetJsonModel().ConnectionStrings.SeojungriOracle);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestOracleModel>().HasData(
                new TestOracleModel { Id = 1, Name = "이명박", Years = 12 },
                new TestOracleModel { Id = 2, Name = "윤석렬", Years = 18 });
        }
    }

    public class TestOracleModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Years { get; set; }
    }
}
