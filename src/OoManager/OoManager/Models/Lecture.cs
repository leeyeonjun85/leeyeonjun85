using System;
using System.Collections.Generic;

namespace OoManager.Models
{
    public class Lecture
    {
        public int mid { get; set; }
        public string o2_class_date { get; set; } = string.Empty;
        public string o2_class_homework { get; set; } = string.Empty;
        public string o2_class_lecture { get; set; } = string.Empty;
        public string o2_class_memo { get; set; } = string.Empty;
        public string o2_class_time_in { get; set; } = string.Empty;
        public string o2_class_time_out { get; set; } = string.Empty;
    }
}
