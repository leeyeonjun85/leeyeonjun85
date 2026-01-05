using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace BlobTest
{
    public class ContextSQLite : DbContext
    {
        // DbSet
        public DbSet<ModelSQLite> blob_tbl { get; set; }
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
            modelBuilder.Entity<ModelSQLite>(entity =>
            {
                entity.Property(x => x.file).HasColumnType("blob");
            });
        }
    }

    public class ModelSQLite
    {
        [Key]
        public int id { get; set; }
        public string filename { get; set; }
        public int filesize { get; set; }
        public byte[] file { get; set; }
    }
}
