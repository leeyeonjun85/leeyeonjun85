using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace DataBaseTools.Models
{
    public class NavigationItem
    {
        public string Name { get; }
        public string Title { get; }
        public PackIconKind SelectedIcon { get; }
        public PackIconKind UnselectedIcon { get; }
        public string Source { get; }
        public bool IsEnabled { get; set; }

        public NavigationItem(string name, string title, PackIconKind selectedIcon, PackIconKind unselectedIcon, string source, bool isEnabled = true)
        {
            Name = name;
            Title = title;
            SelectedIcon = selectedIcon;
            UnselectedIcon = unselectedIcon;
            Source = source;
            IsEnabled = isEnabled;
        }
    }
}
