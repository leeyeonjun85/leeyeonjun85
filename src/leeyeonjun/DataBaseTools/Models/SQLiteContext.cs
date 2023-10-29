using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataBaseTools.Models
{
    public class SQLiteContext : DbContext
    {
        // DbSet
        public DbSet<SQLiteModel> sqliteDB { get; set; }
        private readonly string _connectionString;

        public SQLiteContext(string connectionString)
        {
            _connectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SQLiteModel>().HasData(
                new SQLiteModel { Id = 1, Name = "이명박", Old = 12 },
                new SQLiteModel { Id = 2, Name = "박근혜", Old = 15 },
                new SQLiteModel { Id = 3, Name = "문재인", Old = 13 },
                new SQLiteModel { Id = 4, Name = "윤석렬", Old = 18 });
        }
    }

    public class SQLiteModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Old { get; set; }
    }
}
