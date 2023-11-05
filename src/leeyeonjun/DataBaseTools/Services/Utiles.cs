#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using DataBaseTools.Models;
using DataBaseTools.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Configuration;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Forms;
using System;
using MessageBox = System.Windows.Forms.MessageBox;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

namespace DataBaseTools.Services
{
    public class Utiles
    {
        public static AppData InitApp(AppData AppData)
        {
            return AppData;
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
            if (AppData.SQLiteIsConnected)
                AppData.BtnSQLiteBackground = new SolidColorBrush(AppData.ColorSecondary);
            else
                AppData.BtnSQLiteBackground = new SolidColorBrush(AppData.ColorPrimary);
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
