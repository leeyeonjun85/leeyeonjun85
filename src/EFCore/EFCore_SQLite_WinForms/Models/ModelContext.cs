using Microsoft.EntityFrameworkCore;

namespace EFCore_SQLite_WinForms.Models
{
    public class ModelContext : DbContext
    {
        // DbSet
        public DbSet<School> schools { get; set; }
        public DbSet<Student> students { get; set; }

        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>().HasData(
                new School { id = 1, name = "평택초" },
                new School { id = 2, name = "아산초" });

            modelBuilder.Entity<Student>().HasData(
                new Student { id = 1, name = "베트맨", schoolId = 1 },
                new Student { id = 2, name = "슈퍼맨", schoolId = 1 },
                new Student { id = 3, name = "이연준", schoolId = 2 });
        }
    }
}
