using System;

namespace OoManager.Models
{
    public class DateLectures
    {
        public string DateString { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public Lecture Lecture { get; set; } = new();
        public MemberData SelectedMember { get; set; } = new();
    }
}
