using Microsoft.EntityFrameworkCore;

namespace EFCore_MySQL.Models
{
    public class ModelContext : DbContext
    {
        // DbSet
        //public DbSet<Student> TestTable1 { get; set; }
        public DbSet<Student> TEST_MAUI { get; set; }

        string connString = "Server=localhost;Database=testdb;Uid=root;Pwd=0316165110;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(connString);

    }
}
