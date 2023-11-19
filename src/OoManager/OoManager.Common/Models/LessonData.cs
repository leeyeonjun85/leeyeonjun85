using System;
using System.Collections.Generic;

namespace OoManager.Common.Models
{
    public class LessonData
    {
        public ModelMember Member { get; set; } = new();
        public ModelLessons Lesson1 { get; set; } = new();
        public ModelLessons Lesson2 { get; set; } = new();
        public ModelLessons Lesson3 { get; set; } = new();
        public ModelLessons Lesson4 { get; set; } = new();
        public ModelLessons Lesson5 { get; set; } = new();
        public ModelLessons Lesson6 { get; set; } = new();
        public ModelLessons Lesson7 { get; set; } = new();
    }
}
