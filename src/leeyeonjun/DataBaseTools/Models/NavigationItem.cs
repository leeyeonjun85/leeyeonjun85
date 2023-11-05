using System.Windows;
using MaterialDesignThemes.Wpf;

namespace DataBaseTools.Models
{
    public class NavigationItem
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
        public string Source { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
        public Visibility IsVisibility { get; set; } = Visibility.Hidden;
    }
}
