using System.Configuration;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DataBaseTools.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        [ObservableProperty]
        private string _windowTitle = string.Empty;


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
