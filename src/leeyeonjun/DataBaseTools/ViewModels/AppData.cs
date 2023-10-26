using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseTools.Models;
using MaterialDesignThemes.Wpf;

namespace DataBaseTools.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        [ObservableProperty]
        private string _windowTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new();
        [ObservableProperty]
        private NavigationItem _selectedPage = new();

        [ObservableProperty]
        private Visibility _pageTempVisibility = Visibility.Hidden;

        [ObservableProperty]
        private string _string1 = "test_Main";

        


        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;
        [ObservableProperty]
        private bool _progressBarIsIndeterminate = false;

        [ObservableProperty]
        private string _sQLiteConnectionString = string.Empty;
        [ObservableProperty]
        private string _oracleConnectionString = string.Empty;
    }
}
