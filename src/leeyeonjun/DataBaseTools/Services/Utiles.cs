#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.ViewModels;
using Microsoft.Extensions.Logging;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DataBaseTools.Services
{
    public partial class Utiles
    {
        public static AppData InitApp(AppData AppData)
        {
            return AppData;
        }

        [DllImport("User32", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern void SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();



        public static void RefreshPageNavigationItems(NavigationItem selectedPage)
        {
            ObservableCollection<NavigationItem> tempList = new();
            NavigationItem tempSelectedPage = new(
                name : selectedPage.Name,
                title : selectedPage.Title,
                selectedIcon : selectedPage.SelectedIcon,
                unselectedIcon : selectedPage.UnselectedIcon,
                source : selectedPage.Source,
                isEnabled : selectedPage.IsEnabled
            );

            foreach (NavigationItem item in App.Data.NavigationList)
            {
                tempList.Add(item);
            }

            App.Data.NavigationList.Clear();

            foreach (NavigationItem item in tempList)
            {
                App.Data.NavigationList.Add(item);
            }

            App.Data.SelectedPage = tempSelectedPage;
            //PageNavigationSelectionChanged(App.Data.SelectedPage);
        }

        public static void PageNavigationSelectionChanged(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case Pages.Home:
                    {
                        OpenPageHome(); break;
                    }
                case Pages.SignalR:
                    {
                        if (string.IsNullOrEmpty(App.Data.SignalRAddress))
                        {
                            App.Data.SignalRIPv4 = getLocalIPAddress(AddressFamily.InterNetwork);
                            App.Data.SignalRPort = 6714;
                            App.Data.SignalRHub = "chathub";
                            App.Data.SignalRAddress = $"https://{App.Data.SignalRIPv4}:{App.Data.SignalRPort}/{App.Data.SignalRHub}";
                        }
                        if (string.IsNullOrEmpty(App.Data.SignalRChatName))
                        {
                            App.Data.SignalRChatName = "닉네임" + DateTime.Now.Second.ToString()[^1];
                        }

                        OpenPageSignalR(); break;
                    }
                case Pages.WebSocket:
                    {
                        if (string.IsNullOrEmpty(App.Data.WsAddress))
                        {
                            App.Data.Wsipv4 = getLocalIPAddress(AddressFamily.InterNetwork);
                            App.Data.WsPort = 6714;
                            App.Data.WsAddress = $"ws://{App.Data.Wsipv4}:{App.Data.WsPort}/Chat";
                        }
                        if (string.IsNullOrEmpty(App.Data.WsChatNickName))
                        {
                            App.Data.WsChatNickName = "닉네임" + DateTime.Now.Second.ToString()[^1];
                        }

                        OpenPageWebSocket(); break;
                    }
                case Pages.SQLite:
                    {
                        OpenPageSQLite();
                        InitSQLite(); break;
                    }
                case Pages.Oracle:
                    {
                        OpenPageOracle();
                        InitOracle(); break;
                    }

                default: break;
            }

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(App.Data));
        }


        public static string getLocalIPAddress(AddressFamily addressFamily = AddressFamily.InterNetwork)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress iPAddress in host.AddressList)
            {
                if (iPAddress.AddressFamily == addressFamily)
                {
                    return iPAddress.ToString();
                }
            }

            return string.Empty;
        }


        public static void GetAllTables(AppData AppData)
        {
            if (AppData.OracleContext is null
             || AppData.OracleCommand is null) return;

            AppData.OracleCommand.CommandText = "SELECT *"
                                              + "FROM all_tables";

            try
            {
                AppData.OracleDataReader = AppData.OracleCommand.ExecuteReader();
            }
            catch (Exception ex) { }

            if (AppData.OracleDataReader is null) return;

            //List<string> columns = new();
            //for (int i = 0; i < AppData.OracleDataReader.FieldCount; i++)
            //    columns.Add(AppData.OracleDataReader.GetName(i));

            while (AppData.OracleDataReader.Read())
            {

                OracleTable addTableData = new()
                {
                    OWNER = AppData.OracleDataReader["OWNER"].ToString(),
                    TABLE_NAME = AppData.OracleDataReader["TABLE_NAME"].ToString(),
                    TABLESPACE_NAME = AppData.OracleDataReader["TABLESPACE_NAME"].ToString(),
                    CLUSTER_NAME = AppData.OracleDataReader["CLUSTER_NAME"].ToString(),
                    IOT_NAME = AppData.OracleDataReader["IOT_NAME"].ToString(),
                    STATUS = AppData.OracleDataReader["STATUS"].ToString(),
                    PCT_FREE = AppData.OracleDataReader["PCT_FREE"].ToString(),
                    PCT_USED = AppData.OracleDataReader["PCT_USED"].ToString(),
                    INI_TRANS = AppData.OracleDataReader["INI_TRANS"].ToString(),
                    MAX_TRANS = AppData.OracleDataReader["MAX_TRANS"].ToString(),
                    INITIAL_EXTENT = AppData.OracleDataReader["INITIAL_EXTENT"].ToString(),
                    NEXT_EXTENT = AppData.OracleDataReader["NEXT_EXTENT"].ToString(),
                    MIN_EXTENTS = AppData.OracleDataReader["MIN_EXTENTS"].ToString(),
                    MAX_EXTENTS = AppData.OracleDataReader["MAX_EXTENTS"].ToString(),
                    PCT_INCREASE = AppData.OracleDataReader["PCT_INCREASE"].ToString(),
                    FREELISTS = AppData.OracleDataReader["FREELISTS"].ToString(),
                    FREELIST_GROUPS = AppData.OracleDataReader["FREELIST_GROUPS"].ToString(),
                    LOGGING = AppData.OracleDataReader["LOGGING"].ToString(),
                    BACKED_UP = AppData.OracleDataReader["BACKED_UP"].ToString(),
                    NUM_ROWS = AppData.OracleDataReader["NUM_ROWS"].ToString(),
                    BLOCKS = AppData.OracleDataReader["BLOCKS"].ToString(),
                    EMPTY_BLOCKS = AppData.OracleDataReader["EMPTY_BLOCKS"].ToString(),
                    AVG_SPACE = AppData.OracleDataReader["AVG_SPACE"].ToString(),
                    CHAIN_CNT = AppData.OracleDataReader["CHAIN_CNT"].ToString(),
                    AVG_ROW_LEN = AppData.OracleDataReader["AVG_ROW_LEN"].ToString(),
                    AVG_SPACE_FREELIST_BLOCKS = AppData.OracleDataReader["AVG_SPACE_FREELIST_BLOCKS"].ToString(),
                    NUM_FREELIST_BLOCKS = AppData.OracleDataReader["NUM_FREELIST_BLOCKS"].ToString(),
                    DEGREE = AppData.OracleDataReader["DEGREE"].ToString(),
                    INSTANCES = AppData.OracleDataReader["INSTANCES"].ToString(),
                    CACHE = AppData.OracleDataReader["CACHE"].ToString(),
                    TABLE_LOCK = AppData.OracleDataReader["TABLE_LOCK"].ToString(),
                    SAMPLE_SIZE = AppData.OracleDataReader["SAMPLE_SIZE"].ToString(),
                    LAST_ANALYZED = AppData.OracleDataReader["LAST_ANALYZED"].ToString(),
                    PARTITIONED = AppData.OracleDataReader["PARTITIONED"].ToString(),
                    IOT_TYPE = AppData.OracleDataReader["IOT_TYPE"].ToString(),
                    TEMPORARY = AppData.OracleDataReader["TEMPORARY"].ToString(),
                    SECONDARY = AppData.OracleDataReader["SECONDARY"].ToString(),
                    NESTED = AppData.OracleDataReader["NESTED"].ToString(),
                    BUFFER_POOL = AppData.OracleDataReader["BUFFER_POOL"].ToString(),
                    FLASH_CACHE = AppData.OracleDataReader["FLASH_CACHE"].ToString(),
                    CELL_FLASH_CACHE = AppData.OracleDataReader["CELL_FLASH_CACHE"].ToString(),
                    ROW_MOVEMENT = AppData.OracleDataReader["ROW_MOVEMENT"].ToString(),
                    GLOBAL_STATS = AppData.OracleDataReader["GLOBAL_STATS"].ToString(),
                    USER_STATS = AppData.OracleDataReader["USER_STATS"].ToString(),
                    DURATION = AppData.OracleDataReader["DURATION"].ToString(),
                    SKIP_CORRUPT = AppData.OracleDataReader["SKIP_CORRUPT"].ToString(),
                    MONITORING = AppData.OracleDataReader["MONITORING"].ToString(),
                    CLUSTER_OWNER = AppData.OracleDataReader["CLUSTER_OWNER"].ToString(),
                    DEPENDENCIES = AppData.OracleDataReader["DEPENDENCIES"].ToString(),
                    COMPRESSION = AppData.OracleDataReader["COMPRESSION"].ToString(),
                    COMPRESS_FOR = AppData.OracleDataReader["COMPRESS_FOR"].ToString(),
                    DROPPED = AppData.OracleDataReader["DROPPED"].ToString(),
                    READ_ONLY = AppData.OracleDataReader["READ_ONLY"].ToString(),
                    SEGMENT_CREATED = AppData.OracleDataReader["SEGMENT_CREATED"].ToString(),
                    RESULT_CACHE = AppData.OracleDataReader["RESULT_CACHE"].ToString(),
                    CLUSTERING = AppData.OracleDataReader["CLUSTERING"].ToString(),
                    ACTIVITY_TRACKING = AppData.OracleDataReader["ACTIVITY_TRACKING"].ToString(),
                    DML_TIMESTAMP = AppData.OracleDataReader["DML_TIMESTAMP"].ToString(),
                    HAS_IDENTITY = AppData.OracleDataReader["HAS_IDENTITY"].ToString(),
                    CONTAINER_DATA = AppData.OracleDataReader["CONTAINER_DATA"].ToString(),
                    INMEMORY = AppData.OracleDataReader["INMEMORY"].ToString(),
                    INMEMORY_PRIORITY = AppData.OracleDataReader["INMEMORY_PRIORITY"].ToString(),
                    INMEMORY_DISTRIBUTE = AppData.OracleDataReader["INMEMORY_DISTRIBUTE"].ToString(),
                    INMEMORY_COMPRESSION = AppData.OracleDataReader["INMEMORY_COMPRESSION"].ToString(),
                    INMEMORY_DUPLICATE = AppData.OracleDataReader["INMEMORY_DUPLICATE"].ToString(),
                    DEFAULT_COLLATION = AppData.OracleDataReader["DEFAULT_COLLATION"].ToString(),
                    DUPLICATED = AppData.OracleDataReader["DUPLICATED"].ToString(),
                    SHARDED = AppData.OracleDataReader["SHARDED"].ToString(),
                    EXTERNALLY_SHARDED = AppData.OracleDataReader["EXTERNALLY_SHARDED"].ToString(),
                    EXTERNALLY_DUPLICATED = AppData.OracleDataReader["EXTERNALLY_DUPLICATED"].ToString(),
                    EXTERNAL = AppData.OracleDataReader["EXTERNAL"].ToString(),
                    HYBRID = AppData.OracleDataReader["HYBRID"].ToString(),
                    CELLMEMORY = AppData.OracleDataReader["CELLMEMORY"].ToString(),
                    CONTAINERS_DEFAULT = AppData.OracleDataReader["CONTAINERS_DEFAULT"].ToString(),
                    CONTAINER_MAP = AppData.OracleDataReader["CONTAINER_MAP"].ToString(),
                    EXTENDED_DATA_LINK = AppData.OracleDataReader["EXTENDED_DATA_LINK"].ToString(),
                    EXTENDED_DATA_LINK_MAP = AppData.OracleDataReader["EXTENDED_DATA_LINK_MAP"].ToString(),
                    INMEMORY_SERVICE = AppData.OracleDataReader["INMEMORY_SERVICE"].ToString(),
                    INMEMORY_SERVICE_NAME = AppData.OracleDataReader["INMEMORY_SERVICE_NAME"].ToString(),
                    CONTAINER_MAP_OBJECT = AppData.OracleDataReader["CONTAINER_MAP_OBJECT"].ToString(),
                    MEMOPTIMIZE_READ = AppData.OracleDataReader["MEMOPTIMIZE_READ"].ToString(),
                    MEMOPTIMIZE_WRITE = AppData.OracleDataReader["MEMOPTIMIZE_WRITE"].ToString(),
                    HAS_SENSITIVE_COLUMN = AppData.OracleDataReader["HAS_SENSITIVE_COLUMN"].ToString(),
                    ADMIT_NULL = AppData.OracleDataReader["ADMIT_NULL"].ToString(),
                    DATA_LINK_DML_ENABLED = AppData.OracleDataReader["DATA_LINK_DML_ENABLED"].ToString(),
                    LOGICAL_REPLICATION = AppData.OracleDataReader["LOGICAL_REPLICATION"].ToString(),
                };

                AppData.OracleTableList.Add(addTableData);
            }
        }


        public static void OpenPageHome()
        {
            App.Data.SelectedIndex = Pages.Home;
            App.Data.SelectedPage = App.Data.NavigationList[Pages.Home];
        }
        public static void OpenPageSignalR()
        {
            App.Data.SelectedIndex = Pages.SignalR;
            App.Data.SelectedPage = App.Data.NavigationList[Pages.SignalR];
        }
        public static void OpenPageWebSocket()
        {
            App.Data.SelectedIndex = Pages.WebSocket;
            App.Data.SelectedPage = App.Data.NavigationList[Pages.WebSocket];
        }

        public static void OpenPageSQLite()
        {
            App.Data.SelectedIndex = Pages.SQLite;
            App.Data.SelectedPage = App.Data.NavigationList[Pages.SQLite];
            InitSQLite();
        }
        public static void OpenPageOracle()
        {
            App.Data.SelectedIndex = Pages.Oracle;
            App.Data.SelectedPage = App.Data.NavigationList[Pages.Oracle];
        }


        public static void InitSQLite()
        {
            App.Data.SQLiteSelectedItems = new();
            App.Data.SQLiteData = new();
            App.Data.String1 = string.Empty;
            App.Data.AddName = string.Empty;
            App.Data.AddOld = 0;
            App.Data.UpdateName = string.Empty;
            App.Data.UpdateOld = 0;
        }

        public static void InitOracle()
        {
            App.Data.OracleSelectedItems = new();
            App.Data.OracleData = new();
            App.Data.String1 = string.Empty;
            App.Data.AddName = string.Empty;
            App.Data.AddOld = 0;
            App.Data.UpdateName = string.Empty;
            App.Data.UpdateOld = 0;
        }

        public static void ExceptionTask(Exception ex)
        {
            MessageBox.Show(
                    text: $"에러가 발생하였습니다."
                        + $"{Environment.NewLine}Error message: {ex.Message}"
                        + $"{Environment.NewLine}Details: {ex}",
                    caption: "Error", MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);

            App.logger!.LogError($"{ex.ToString}{Environment.NewLine}{ex}");
        }


        public static async Task DisposeSQLiteAsync()
        {
            await Task.Run(() =>
            {
                App.Data.SQLiteDataReader?.Close();
                App.Data.SQLiteDataReader?.Dispose();
                App.Data.SQLiteDataReader = null;
                App.Data.SQLiteCommand?.Dispose();
                App.Data.SQLiteCommand = null;
                App.Data.SQLiteConnection?.Close();
                App.Data.SQLiteConnection?.Dispose();
                App.Data.SQLiteCommand = null;
                App.Data.SQLiteContext?.Dispose();
                App.Data.SQLiteContext = null;
            });
        }

        public static async Task DisposeOracleAsync()
        {
            await Task.Run(() =>
            {
                App.Data.OracleDataReader?.Dispose();
                App.Data.OracleDataReader = null;
                App.Data.OracleDataAdapter?.Dispose();
                App.Data.OracleDataAdapter = null;
                App.Data.OracleDataReader?.Close();
                App.Data.OracleDependency = null;
                App.Data.OracleCommand?.Dispose();
                App.Data.OracleConnection?.Close();
                App.Data.OracleConnection?.Dispose();
                App.Data.OracleConnection = null;
                App.Data.OracleContext?.Dispose();
                App.Data.OracleContext = null;
            });
        }

        public static async Task DisposeSignalRAsync()
        {
            await Task.Run(() =>
            {
                App.Data.SignalRClient?.StopAsync();
                App.Data.SignalRClient?.DisposeAsync();
                App.Data.SignalRClient = null;
                App.Data.SignalRServer?.StopAsync();
                App.Data.SignalRServer?.Dispose();
                App.Data.SignalRServer = null;
            });
        }

        public static async Task DisposeWebSocketAsync()
        {
            await Task.Run(() =>
            {
                App.Data.WebSocket?.Close();
                App.Data.WebSocket = null;
                App.Data.WsServer?.Stop();
                App.Data.WsServer = null;
            });
        }

        public static async Task DisposeAllAsync()
        {
            await DisposeSQLiteAsync();
            await DisposeWebSocketAsync();
            await DisposeSignalRAsync();
            await DisposeOracleAsync();
        }
    }
}
