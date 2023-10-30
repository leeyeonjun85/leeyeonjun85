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

namespace DataBaseTools.Services
{
    public class Utiles
    {
        public static AppData InitApp(AppData AppData)
        {
            // Color Theme
            PaletteHelper paletteHelper = new();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(AppData.PrimaryColor);
            theme.SetSecondaryColor(AppData.SecondaryColor);
            paletteHelper.SetTheme(theme);


            AppData.NavigationList.Add(new NavigationItem
            {
                Title = "Home",
                SelectedIcon = PackIconKind.Home,
                UnselectedIcon = PackIconKind.HomeOutline,
                Source = "/Views/PageHome.xaml",
                IsVisibility = Visibility.Visible,
            });

            AppData.NavigationList.Add(new NavigationItem
            {
                Title = "SQLite",
                SelectedIcon = PackIconKind.Mushroom,
                UnselectedIcon = PackIconKind.MushroomOutline,
                Source = "/Views/PageSQLIte.xaml",
                IsVisibility = Visibility.Hidden,
            });

            AppData.NavigationList.Add(new NavigationItem
            {
                Title = "WebSocket",
                SelectedIcon = PackIconKind.Connection,
                UnselectedIcon = PackIconKind.Connection,
                Source = "/Views/PageWebSocket.xaml",
                IsVisibility = Visibility.Hidden,
            });

            OpenPageHome(AppData);

            // Window Title
            AppData.WindowTitle = $"이연준의 DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";

            // SQLite Connection String
            AppData.SQLiteConnectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().IndexOf("DataBaseTools")], "DataBaseTools", "SQLiteTest.db")}";

            // Oracle Connection String
            AppData.OracleConnectionString = JsonData.GetEdcoreWorksJsonData("SeojungriOracle");

            return AppData;
        }


        public static void OpenPageHome(AppData AppData)
        {
            AppData.SelectedPage = AppData.NavigationList[Pages.Home];
            if (AppData.SQLiteIsConnected)
                AppData.BtnSQLiteBackground = new SolidColorBrush(AppData.SecondaryColor);
            else
                AppData.BtnSQLiteBackground = new SolidColorBrush(AppData.PrimaryColor);
        }

        public static void OpenPageSQLite(AppData AppData)
        {
            AppData.SelectedPage = AppData.NavigationList[Pages.SQLite];
            InitSQLite(AppData);
        }

        public static void OpenPageWebSocket(AppData AppData)
        {
            AppData.SelectedPage = AppData.NavigationList[Pages.WebSocket];
            InitSQLite(AppData);
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
    }
}
