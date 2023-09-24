using System.Collections.Generic;
using MaterialDesignThemes.Wpf;

namespace OoManager.Models
{
    public class MemberAdd
    {
        public List<string> GradeStrings { get; set; } = new() 
        { "6살", "7살", "초1", "초2", "초3", "초4", "초5", "초6", "중1", "중2", "중3", "고1", "고2", "고3" };
        public string SelectedGrade { get; set; } = "중1";
    }
}
