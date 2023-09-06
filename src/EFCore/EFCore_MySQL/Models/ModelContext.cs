using Microsoft.EntityFrameworkCore;

namespace EFCore_MySQL.Models
{
    public class ModelContext : DbContext
    {
        // DbSet
        public DbSet<Student> Students { get; set; }

        string connString = "Server=localhost;Database=testdb;Uid=root;Pwd=0316165110;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(connString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotifications);

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "베트맨" },
                new Student { Id = 2, Name = "슈퍼맨" },
                new Student { Id = 3, Name = "아이언맨" },
                new Student { Id = 4, Name = "이연준" },
                new Student { Id = 5, Name = "윤석열" },
                new Student { Id = 6, Name = "홍길동" },
                new Student { Id = 7, Name = "이순신" },
                new Student { Id = 8, Name = "이연준" },
                new Student { Id = 9, Name = "차범근" },
                new Student { Id = 10, Name = "차두리" },
                new Student { Id = 11, Name = "손흥민" });
        }
    }
}
