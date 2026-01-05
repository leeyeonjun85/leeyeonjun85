using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfOxyPlotGraph.Models
{
  public class StudentWithScore
  {
    public Student Student { get; set; } = default!;
    public TestScore Score { get; set; } = default!;

    private static IEnumerable<StudentWithScore>? _joinedData;
    public static IEnumerable<StudentWithScore> GetSeedDatas()
    {
      if (_joinedData != null) return _joinedData;

      List<Student> students = new()
      {
        new Student { Id = 1, Name = "김철수" },
        new Student { Id = 2, Name = "이영희" },
        new Student { Id = 3, Name = "박민수" },
        new Student { Id = 4, Name = "정지우" },
        new Student { Id = 5, Name = "홍길동" }
      };

      Random random = new Random();
      List<TestScore> testScores = new List<TestScore>();
      foreach (Student student in students)
      {
        for (int i = 1; i <= 5; i++)
        {
          DateTime date = new DateTime(2023, i, 1);
          testScores.Add(new TestScore
          {
            Id = i,
            Date = date,
            KorScore = random.Next(0, 101),
            MathScore = random.Next(0, 101),
            EngScore = random.Next(0, 101),
            StudentId = student.Id,
          });
        }
      }
      _joinedData = from student in students
                    join score in testScores on student.Id equals score.StudentId
                    select new StudentWithScore { Student = student, Score = score };

      return _joinedData;
    }
  }
}
