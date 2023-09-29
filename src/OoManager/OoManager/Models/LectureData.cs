using System;
using System.Collections.Generic;
using OoManager.Common;

namespace OoManager.Models
{
    public class LectureData
    {
        public string DateString { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public Lecture Lecture { get; set; } = new();
        public Member SelectedMember { get; set; } = new();
    }
}
