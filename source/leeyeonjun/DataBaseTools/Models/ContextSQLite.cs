using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBaseTools.Models
{
    public class ContextSQLite : DbContext
    {
        // DbSet
        public DbSet<ModelSQLite> sqliteDB { get; set; }
        private readonly string _connectionString;

        public ContextSQLite(string connectionString)
        {
            _connectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelSQLite>().HasData(
                new ModelSQLite { Id = 1, Name = "이명박", Old = 12 },
                new ModelSQLite { Id = 2, Name = "박근혜", Old = 15 },
                new ModelSQLite { Id = 3, Name = "문재인", Old = 13 },
                new ModelSQLite { Id = 4, Name = "윤석렬", Old = 18 });
        }
    }

    public class ModelSQLite
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Old { get; set; }
    }
}
