using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Edcore.Models
{
    public class TestSQLiteContext : DbContext
    {
        // DbSet
        public DbSet<TestSQLiteModel> yeonjunTest { get; set; }

        public TestSQLiteContext(DbContextOptions<TestSQLiteContext> options) : base(options) { }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseOracle(MyUtiles.GetJsonModel().ConnectionStrings.SeojungriOracle);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestSQLiteModel>().HasData(
                new TestSQLiteModel { Id = 1, Name = "이명박", Years = 12 },
                new TestSQLiteModel { Id = 2, Name = "윤석렬", Years = 18 });
        }
    }

    public class TestSQLiteModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Years { get; set; }
    }
}
