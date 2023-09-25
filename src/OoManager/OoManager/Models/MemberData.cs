using System;
using System.Collections.Generic;
using OoManager.Common;

namespace OoManager.Models
{
    public class MemberData
    {
        public string Key { get; set; } = string.Empty;
        public Member Member { get; set; } = new();

        public List<string> GradeStrings { get; set; } = new()
            { "6살", "7살", "초1", "초2", "초3", "초4", "초5", "초6", "중1", "중2", "중3", "고1", "고2", "고3" };
        public string SelectedGrade { get; set; } = "중1";

        public List<string> StateStrings { get; set; } = new()
            { "재원", "휴원", "보류" };
        public string SelectedState { get; set; } = "재원";
    }
}
