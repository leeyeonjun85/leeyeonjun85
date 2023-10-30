using Microsoft.EntityFrameworkCore;

namespace OoManager.Models
{
    public class OoDbContext : DbContext
    {

        // DbSet
        public DbSet<ModelMembers> members { get; set; }
        public DbSet<ModelLessons> lessons { get; set; }

        public OoDbContext(DbContextOptions<OoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelMembers>().HasData(
                new ModelMembers { id = 1, name = "이연준", phoneNumber = "010-1234-5678" }
                );
        }

    }
}
