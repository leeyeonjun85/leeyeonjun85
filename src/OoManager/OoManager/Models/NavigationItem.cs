using System.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using OoManager.ViewModels;

namespace OoManager.Models
{
    public partial class NavigationItem : ViewModelBase
    {
        public string? Title { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
        public string? Source { get; set; }

        [ObservableProperty]
        private object? _notification = null;

        [ObservableProperty]
        private bool _isAbled = true;
    }
}
