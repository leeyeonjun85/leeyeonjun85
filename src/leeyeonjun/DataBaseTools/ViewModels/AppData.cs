using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseTools.Models;
using DataBaseTools.Services;
using MaterialDesignThemes.Wpf;
using Oracle.ManagedDataAccess.Client;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DataBaseTools.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        public Color ColorPrimary { get; } = Colors.MidnightBlue;
        public Color ColorSecondary { get; } = Colors.LimeGreen;

        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new()
        {
            new(
                name : "Home",
                title : "Data Base Tools by Lee Yeon-jun",
                selectedIcon : PackIconKind.Home,
                unselectedIcon : PackIconKind.HomeOutline,
                source : "/Views/PageHome.xaml",
                isEnabled : true
            ),
            new(
                name : "WebSocket",
                title : "Chatting in WebSocket",
                selectedIcon : PackIconKind.Connection,
                unselectedIcon : PackIconKind.Connection,
                source : "/Views/PageWebSocket.xaml",
                isEnabled : true
            ),
            new(
                name : "SQLite",
                title : "SQLite Data Base",
                selectedIcon : PackIconKind.Mushroom,
                unselectedIcon : PackIconKind.MushroomOutline,
                source : "/Views/PageSQLIte.xaml",
                isEnabled : false
            ),
            new(
                name : "Oracle",
                title : "서정리 오라클",
                selectedIcon : PackIconKind.EmoticonWink,
                unselectedIcon : PackIconKind.EmoticonWinkOutline,
                source : "/Views/PageOracle.xaml",
                isEnabled : false
            )
        };
        [ObservableProperty]
        private NavigationItem? _selectedPage;
        [ObservableProperty]
        private int _selectedIndex;

        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;
        [ObservableProperty]
        private bool _progressBarIsIndeterminate = false;
        [ObservableProperty]
        private string _string1 = "test_Main";
        [ObservableProperty]
        private string _string2 = "test_home";

        [ObservableProperty]
        private string _addName = string.Empty;
        [ObservableProperty]
        private int _addOld;
        [ObservableProperty]
        private string _updateName = string.Empty;
        [ObservableProperty]
        private int _updateOld;

        public Button BtnSQLite { get; set; } = new();
        //{
        //    Content = "Connect",
        //    Background = new SolidColorBrush(App.Data.ColorPrimary),
        //    Foreground = new SolidColorBrush(Colors.White)
        //};
        public Button BtnOracleConnect { get; set; } = new();
        //{
        //    Content = "Connect",
        //    Background = new SolidColorBrush(App.Data.ColorPrimary),
        //    Foreground = new SolidColorBrush(Colors.White)
        //};
        public Button BtnWebSocket { get; set; } = new();
        //{
        //    Content = "Connect",
        //    Background = new SolidColorBrush(App.Data.ColorPrimary),
        //    Foreground = new SolidColorBrush(Colors.White)
        //};




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


        // SQLite
        public bool SQLiteIsConnected { get; set; } = false;
        public ContextSQLite? SQLiteContext { get; set; }
        public DbConnection? SQLiteConnection { get; set; }
        public DbCommand? SQLiteCommand { get; set; }
        public DbDataReader? SQLiteDataReader { get; set; }
        [ObservableProperty]
        private string _sQLiteConnectionString = string.Empty;
        public ObservableCollection<ModelSQLite> SQLiteItemsSource { get; set; } = new();
        public ObservableCollection<ModelSQLite> SQLiteSelectedItems { get; set; } = new();
        public ModelSQLite SQLiteData { get; set; } = new();


        // Oracle
        public bool IsOracleConnected { get; set; } = false;
        public ContextOracle? OracleContext { get; set; }
        public OracleConnection? OracleConnection { get; set; }
        public OracleCommand? OracleCommand { get; set; } = new();
        public OracleDependency? OracleDependency { get; set; }
        public OracleDataAdapter? OracleDataAdapter { get; set; }
        public OracleDataReader? OracleDataReader { get; set; }
        [ObservableProperty]
        private string _oracleConnectionString = JsonData.GetEdcoreWorksJsonData("SeojungriOracle");
        public ObservableCollection<OracleTable> OracleTableList { get; set; } = new();
        public ObservableCollection<ModelOracle> OracleItemsSource { get; set; } = new();
        public ObservableCollection<ModelOracle> OracleSelectedItems { get; set; } = new();
        public ModelOracle OracleData { get; set; } = new();






    }
}
