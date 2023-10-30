using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseTools.Models;
using Microsoft.Data.Sqlite;
using WebSocketSharp.Server;
using WebSocketSharp;
using DataBaseTools.Services;

namespace DataBaseTools.ViewModels
{
    public partial class AppData : ViewModelBase
    {
        // WindowMain
        [ObservableProperty]
        private string _windowTitle = string.Empty;
        [ObservableProperty]
        private Color _primaryColor = Color.FromArgb(255, 0, 31, 158);
        [ObservableProperty]
        private Color _secondaryColor = Color.FromArgb(255, 34, 158, 0);


        [ObservableProperty]
        private ObservableCollection<NavigationItem> _navigationList = new();
        [ObservableProperty]
        private NavigationItem _selectedPage = new();



        [ObservableProperty]
        private Visibility _pageTempVisibility = Visibility.Hidden;

        [ObservableProperty]
        private string _string1 = "test_Main";
        [ObservableProperty]
        private string _string2 = "test_home";


        // SQLite
        [ObservableProperty]
        private bool _sQLiteIsEnabled = false;
        [ObservableProperty]
        private string _sQLiteConnectionString = string.Empty;
        [ObservableProperty]
        private SolidColorBrush _btnSQLiteBackground = new();
        [ObservableProperty]
        private bool _sQLiteIsConnected = false;
        [ObservableProperty]
        private ConnectionState _sQLiteIsState = ConnectionState.Closed;
        [ObservableProperty]
        private SQLiteContext? _sQLiteContext;
        [ObservableProperty]
        private DbConnection? _sQLiteConnection;
        [ObservableProperty]
        private DbCommand? _sQLiteCommand;
        [ObservableProperty]
        private DbDataReader? _sQLiteDataReader;
        [ObservableProperty]
        private ObservableCollection<SQLiteModel> _sQLiteItemsSource = new();
        //[ObservableProperty]
        //private DataTable _sQLiteItemsSource = new();
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
        [ObservableProperty]
        private WebSocketServer? _wsServer;
        [ObservableProperty]
        private WebSocket? _webSocket;
        [ObservableProperty]
        private string _wsChatNickName = "임시닉네임1";
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
