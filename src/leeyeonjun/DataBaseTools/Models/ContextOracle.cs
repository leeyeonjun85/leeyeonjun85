using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBaseTools.Models
{
    public class ContextOracle : DbContext
    {
        // DbSet
        public DbSet<ModelOracle> LeeyeonjunTestTable1 { get; set; }
        private readonly string _connectionString;

        public ContextOracle(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public ContextOracle(DbContextOptions<ContextOracle> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseOracle(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelOracle>().HasData(
                new ModelOracle { Id = 1, Name = "이명박", Old = 12 },
                new ModelOracle { Id = 2, Name = "박근혜", Old = 15 },
                new ModelOracle { Id = 3, Name = "문재인", Old = 13 },
                new ModelOracle { Id = 4, Name = "윤석렬", Old = 18 });
        }
    }

    public class ModelOracle
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Old { get; set; }
    }
}
