using MaterialDesignThemes.Wpf;

namespace DataBaseTools.Models
{
    public class NavigationItem
    {
        public string? Title { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
        public string? Source { get; set; }
        private object? Notification { get; set; } = null;
        private bool IsAbled { get; set; } = true;
    }
}
