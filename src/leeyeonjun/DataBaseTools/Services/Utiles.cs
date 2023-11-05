#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
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
                Name = AppData.SelectedPage.Name,
                Title = AppData.SelectedPage.Title,
                SelectedIcon = AppData.SelectedPage.SelectedIcon,
                UnselectedIcon = AppData.SelectedPage.UnselectedIcon,
                Source = AppData.SelectedPage.Source,
                IsEnabled = AppData.SelectedPage.IsEnabled,
                IsVisibility = AppData.SelectedPage.IsVisibility,
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
            switch (AppData.SelectedPage.Name)
            {
                case "Home":
                    {
                        OpenPageHome(AppData); break;
                    }
                case "SQLite":
                    {
                        OpenPageSQLite(AppData); break;
                    }
                case "WebSocket":
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


        public static void OpenPageHome(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.Home;
            AppData.SelectedPage = AppData.NavigationList[Pages.Home];
        }

        public static void OpenPageSQLite(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.SQLite;
            AppData.SelectedPage = AppData.NavigationList[Pages.SQLite];
            InitSQLite(AppData);
        }

        public static void OpenPageWebSocket(AppData AppData)
        {
            AppData.SelectedPageIndex = Pages.WebSocket;
            AppData.SelectedPage = AppData.NavigationList[Pages.WebSocket];
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
