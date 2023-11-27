using System.Reflection.Metadata;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace OoManager.Common.Models
{
    public class OoDbContext : DbContext
    {

        // DbSet
        public DbSet<ModelMember> members { get; set; }
        public DbSet<ModelLessons> lessons { get; set; }

        public OoDbContext(DbContextOptions<OoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ModelMember>().HasData(
            //    new ModelMember
            //    { 
            //        id = 1, 
            //        grade = GradesToString.PreSchool_6,
            //        name = "이연준",
            //        old = (int)GradesToOld.PreSchool_6,
            //        classPlan = $"월화수목금토일{Environment.NewLine}설겆이(중), 화장실 청소(상)",
            //        money = 5000000,
            //        memberState = States.Attention,
            //        phoneNumber = "010-1234-5678",
            //        memberMemo = $"잘생겼음{Environment.NewLine}이채은을 사랑함",
            //        xp = 900,
            //        xpLog = "Xp 기록"
            //    },
            //    new ModelMember
            //    {
            //        id = 2,
            //        grade = GradesToString.HighSchool_3,
            //        name = "이채은",
            //        old = (int)GradesToOld.HighSchool_3,
            //        classPlan = $"월화수목금토일{Environment.NewLine}과학(상), 수학(중)",
            //        money = 5500000,
            //        memberState = States.Rest,
            //        phoneNumber = "010-5678-5128",
            //        memberMemo = $"예쁨{Environment.NewLine}이연준을 사랑함",
            //        xp = 950,
            //        xpLog = "Xp 기록"
            //    });

            //modelBuilder.Entity<ModelLessons>().HasData(
            //    new ModelLessons
            //    { 
            //        id = Convert.ToInt32($"{DateTime.Now.AddDays(-2):yyyyMMdd}1"),
            //        dateTimeStart = DateTime.Now.AddDays(-2),
            //        dateTimeEnd = DateTime.Now.AddDays(-2).AddHours(-1),
            //        lessonTopic = "삼각함수 1단원",
            //        assignment = "오투 271~272p",
            //        lessonMemo = "수업 열심히 했음",
            //        memberId = 1
            //    },
            //    new ModelLessons
            //    {
            //        id = Convert.ToInt32($"{DateTime.Now:yyyyMMdd}1"),
            //        dateTimeStart = DateTime.Now,
            //        dateTimeEnd = DateTime.Now.AddHours(-1),
            //        lessonTopic = "삼각함수 2단원",
            //        assignment = "오투 273~274p",
            //        lessonMemo = "수업시간에 딴짓했음",
            //        memberId = 1
            //    }
            //);

            //modelBuilder.Entity<ModelMember>()
            //    .HasMany(e => e.ModelLessons)
            //    .WithOne(e => e.ModelMember)
            //    .HasForeignKey(e => e.memberId)
            //    .HasPrincipalKey(e => e.id);

            modelBuilder.Entity<ModelMember>()
                .HasMany(e => e.ModelLessons)
                .WithOne(e => e.ModelMember)
                .HasForeignKey(e => e.memberId);
        }

    }
}
