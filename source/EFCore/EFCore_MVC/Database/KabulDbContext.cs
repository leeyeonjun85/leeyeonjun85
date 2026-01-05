using EF6Basic.Models;
using Microsoft.EntityFrameworkCore;

namespace EF6Basic.Database
{
    public class KabulDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=EFCoreMVC.db");

        // DbSet
        public DbSet<School> Schools { get; set; } = default!;
        public DbSet<Class> Classes { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
    }
}
