using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseTools.Models;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DataBaseTools.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        [ObservableProperty]
        private string _windowTitle = string.Empty;
        public Color ColorPrimary { get; set; } = Color.FromArgb(255, 0, 31, 158);
        public Color ColorSecondary { get; set; } = Color.FromArgb(255, 34, 158, 0);
        public ObservableCollection<NavigationItem> NavigationList { get; set; } = new();
        [ObservableProperty]
        private NavigationItem _selectedPage = new();
        [ObservableProperty]
        private int _selectedPageIndex;
        [ObservableProperty]
        private string _string1 = "test_Main";
        [ObservableProperty]
        private string _string2 = "test_home";


        // SQLite
        [ObservableProperty]
        private string _sQLiteConnectionString = string.Empty;
        [ObservableProperty]
        private SolidColorBrush _btnSQLiteBackground = new();
        [ObservableProperty]
        private bool _sQLiteIsConnected = false;
        public SQLiteContext? SQLiteContext { get; set; }
        public DbConnection? SQLiteConnection { get; set; }
        public DbCommand? SQLiteCommand { get; set; }
        public DbDataReader? SQLiteDataReader { get; set; }
        [ObservableProperty]
        private ObservableCollection<SQLiteModel> _sQLiteItemsSource = new();
        [ObservableProperty]
        private ObservableCollection<SQLiteModel> _sQLiteSelectedItems = new();
        [ObservableProperty]
        private SQLiteModel _sQLiteData = new();
        [ObservableProperty]
        private string _sQLiteAddName = string.Empty;
        [ObservableProperty]
        private int _sQLiteAddOld;
        [ObservableProperty]
        private string _sQLiteUpdateName = string.Empty;
        [ObservableProperty]
        private int _sQLiteUpdateOld;


        // WebSocket
        public WebSocketServer? WsServer { get; set; }
        public WebSocket? WebSocket { get; set; }
        public string Wsipv4 { get; set; } = string.Empty;
        public int WsMode { get; set; } = WebSocketMode.Server;
        public int WsPort { get; set; }
        [ObservableProperty]
        private bool _wsConnected = false;
        [ObservableProperty]
        private string _wsAddress = string.Empty;
        [ObservableProperty]
        private string _wsChatNickName = string.Empty;
        [ObservableProperty]
        private string _wsChatText = $"=== 채팅을 시작합니다. ==={Environment.NewLine}";
        [ObservableProperty]
        private string _wsChatSendText = string.Empty;



        // Oracle
        [ObservableProperty]
        private string _oracleConnectionString = string.Empty;


        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;
        [ObservableProperty]
        private bool _progressBarIsIndeterminate = false;



    }
}
