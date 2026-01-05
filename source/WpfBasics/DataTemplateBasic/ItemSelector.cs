using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataTemplateBasic
{
    public class ItemSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Student? student = item as Student;
            FrameworkElement? frameworkElement = container as FrameworkElement;

            return (DataTemplate)frameworkElement.FindResource(
                student?.Gender == "남" ? "Man" : "Women"
                );
        }
    }
}
