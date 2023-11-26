using System;
using System.Collections.Generic;

namespace OoManager.Common.Models
{
    public class LessonData
    {
        public ModelMember Member { get; set; } = new();
        public LessonDataToolTip? ToolTips { get; set; }
        public ModelLessons? Lesson1 { get; set; }
        public ModelLessons? Lesson2 { get; set; }
        public ModelLessons? Lesson3 { get; set; }
        public ModelLessons? Lesson4 { get; set; }
        public ModelLessons? Lesson5 { get; set; }
        public ModelLessons? Lesson6 { get; set; }
        public ModelLessons? Lesson7 { get; set; }

        public LessonData(ModelMember member)
        {
            Member = member;
        }
    }

    public class LessonDataToolTip
    {
        public LessonDataToolTip(string xp, string bonus, string info, string lesson)
        {
            Xp = xp;
            Bonus = bonus;
            Info = info;
            Lesson = lesson;
        }

        public string Xp { get; }
        public string Bonus { get; }
        public string Info { get; }
        public string Lesson { get; }
    }
}
