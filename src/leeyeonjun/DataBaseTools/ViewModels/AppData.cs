using System.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseTools.Services;

namespace DataBaseTools.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        [ObservableProperty]
        private string _windowTitle = $"DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";


        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;
        [ObservableProperty]
        private bool _progressBarIsIndeterminate = false;

        [ObservableProperty]
        private string _sQLiteConnectionString = JsonData.GetEdcoreWorksJsonData("SQLite") ?? "Data Source=SQLite.db";


    }
}
