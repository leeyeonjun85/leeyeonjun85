using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorServerSignalR.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerSignalR.Models
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



    [Table("MEMBERS")]
    public class ModelMembers
    {
        [Key]
        [Column("ID")]
        public int id { get; set; }

        [Column("NAME")]
        [MaxLength(20)]
        [Required]
        public string name { get; set; } = "김";

        [Column("GRADE")]
        public Grade grade { get; set; } = Grade.middleSchool1;

        [Column("COST")]
        public int cost { get; set; } = 150000;

        [Column("MEMBER_STATE")]
        [Required]
        public MemberState memberState { get; set; } = MemberState.Normal;

        [Column("PHONE_NUMBER")]
        public string phoneNumber { get; set; } = "010-";

        [Column("CLASS_PLAN")]
        [MaxLength(100)]
        public string classPlan { get; set; } = "월화수목금";

        [Column("MEMBER_MEMO")]
        public string memberMemo { get; set; } = "메모";
    }
}
