#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
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
    public class Utiles
    {
        public static AppData InitApp(AppData AppData)
        {
            return AppData;
        }

        public static void RefreshPageNavigationItems(AppData AppData)
        {
            ObservableCollection<NavigationItem> tempList = new();
            NavigationItem tempSelectedPage = new()
            {
                Index = AppData.SelectedPage.Index,
                Name = AppData.SelectedPage.Name,
                Title = AppData.SelectedPage.Title,
                SelectedIcon = AppData.SelectedPage.SelectedIcon,
                UnselectedIcon = AppData.SelectedPage.UnselectedIcon,
                Source = AppData.SelectedPage.Source,
                IsEnabled = AppData.SelectedPage.IsEnabled,
            };

            foreach (NavigationItem item in AppData.NavigationList)
            {
                tempList.Add(item);
            }

            AppData.NavigationList.Clear();

            foreach (NavigationItem item in tempList)
            {
                AppData.NavigationList.Add(item);
            }

            AppData.SelectedPage = tempSelectedPage;
            PageNavigationSelectionChanged(AppData);
        }

        public static void PageNavigationSelectionChanged(AppData AppData)
        {
            switch (AppData.SelectedPage.Index)
            {
                case Pages.Home:
                    {
                        OpenPageHome(AppData); break;
                    }
                case Pages.WebSocket:
                    {
                        if (string.IsNullOrEmpty(AppData.WsAddress))
                        {
                            AppData.Wsipv4 = getLocalIPAddress(AddressFamily.InterNetwork);
                            AppData.WsPort = 6714;
                            AppData.WsAddress = $"ws://{AppData.Wsipv4}:{AppData.WsPort}/Chat";
                        }
                        if (string.IsNullOrEmpty(AppData.WsChatNickName))
                        {
                            AppData.WsChatNickName = "닉네임" + DateTime.Now.Second.ToString()[^1];
                        }

                        OpenPageWebSocket(AppData); break;
                    }
                case Pages.SQLite:
                    {
                        OpenPageSQLite(AppData); break;
                    }
                case Pages.Oracle:
                    {
                        OpenPageOracle(AppData); break;
                    }

                default: throw new Exception();
            }

            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));
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


        public static void OpenPageHome(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.Home;
            AppData.SelectedPage = AppData.NavigationList[Pages.Home];
        }

        public static void OpenPageWebSocket(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.WebSocket;
            AppData.SelectedPage = AppData.NavigationList[Pages.WebSocket];
        }

        public static void OpenPageSQLite(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.SQLite;
            AppData.SelectedPage = AppData.NavigationList[Pages.SQLite];
            InitSQLite(AppData);
        }
        public static void OpenPageOracle(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.Oracle;
            AppData.SelectedPage = AppData.NavigationList[Pages.Oracle];
        }


        public static void InitSQLite(AppData AppData)
        {
            AppData.SQLiteSelectedItems = new();
            AppData.SQLiteData = new();
            AppData.String1 = string.Empty;
            AppData.SQLiteAddName = string.Empty;
            AppData.SQLiteAddOld = 0;
            AppData.SQLiteUpdateName = string.Empty;
            AppData.SQLiteUpdateOld = 0;
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


        public static async Task DisposeSQLiteAsync(AppData AppData)
        {
            await Task.Run(() =>
            {
                AppData.SQLiteDataReader?.Close();
                AppData.SQLiteDataReader?.Dispose();
                AppData.SQLiteDataReader = null;
                AppData.SQLiteCommand?.Dispose();
                AppData.SQLiteCommand = null;
                AppData.SQLiteConnection?.Close();
                AppData.SQLiteConnection?.Dispose();
                AppData.SQLiteCommand = null;
                AppData.SQLiteContext?.Dispose();
                AppData.SQLiteContext = null;
            });
        }

        public static async Task DisposeOracleAsync(AppData AppData)
        {
            await Task.Run(() =>
            {
                AppData.OracleContext?.Dispose();
                AppData.OracleContext = null;
                AppData.OracleConnection?.Close();
                AppData.OracleConnection?.Dispose();
                AppData.OracleConnection = null;
                AppData.OracleCommand?.Dispose();
                AppData.OracleConnection = null;
                AppData.OracleDependency = null;
                AppData.OracleDataAdapter?.Dispose();
                AppData.OracleDataAdapter = null;
                AppData.OracleDataReader?.Close();
                AppData.OracleDataReader?.Dispose();
                AppData.OracleDataReader = null;
            });
        }

        public static async Task DisposeWebSocketAsync(AppData AppData)
        {
            await Task.Run(() =>
            {
                AppData.WebSocket?.Close();
                AppData.WebSocket = null;
                AppData.WsServer?.Stop();
                AppData.WsServer = null;
            });
        }

        public static async Task DisposeAllAsync(AppData AppData)
        {
            await DisposeSQLiteAsync(AppData);
            await DisposeWebSocketAsync(AppData);
        }
    }
}
