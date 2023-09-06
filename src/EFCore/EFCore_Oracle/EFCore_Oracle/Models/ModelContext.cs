using EFCore_Oracle.Properties;
using Microsoft.EntityFrameworkCore;


namespace Oracle_EFCore.Models;

public partial class ModelContext : DbContext
{
    public DbSet<School>? Schools { get; set; }
    public DbSet<Room>? Rooms { get; set; }
    public DbSet<Student>? Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseOracle(Settings.Default.Oracle_Connection_String);
}
