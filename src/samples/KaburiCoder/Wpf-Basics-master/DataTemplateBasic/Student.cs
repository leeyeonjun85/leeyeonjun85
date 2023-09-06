using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplateBasic
{
  public class Student
  {
    public string Name { get; set; } = "";
    public string Gender { get; set; } = "";
    public string Birthday { get; set; } = "";
    public string School { get; set; } = "";

    public override string ToString()
    {
      return $"Name: {Name}, Gender: {Gender}, Birthday: {Birthday}, School: {School}";
    }
    public static List<Student> Students => new()
    {
      new Student{Name="김성현", Gender="남", Birthday="19800201", School = "서울대"},
      new Student{Name="나민성", Gender="남", Birthday="19851225", School = "고려대"},
      new Student{Name="정민지", Gender="여", Birthday="19900101", School = "연세대"},
      new Student{Name="김둘리", Gender="남", Birthday="19870723", School = "전남대"},
      new Student{Name="김수", Gender="여", Birthday="20010331", School = "충남대"},
    };

  }
}
