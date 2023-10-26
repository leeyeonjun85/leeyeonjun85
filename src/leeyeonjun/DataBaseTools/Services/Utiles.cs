#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using DataBaseTools.Models;
using DataBaseTools.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Configuration;
using System.IO;
using System.Windows.Media;

namespace DataBaseTools.Services
{
    public class Utiles
    {
        public static AppData InitApp(AppData AppData)
        {
            // Color Theme
            Color primaryColor = Color.FromArgb(255, 0, 31, 158);
            Color secondaryColor = Color.FromArgb(255, 34, 158, 0);

            PaletteHelper paletteHelper = new();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(primaryColor);
            theme.SetSecondaryColor(secondaryColor);
            paletteHelper.SetTheme(theme);


            AppData.NavigationList.Add(new NavigationItem
            {
                Title = "Home",
                SelectedIcon = PackIconKind.Home,
                UnselectedIcon = PackIconKind.HomeOutline,
                Source = "/Views/PageHome.xaml",
            });

            AppData.NavigationList.Add(new NavigationItem
            {
                Title = "SQLite",
                SelectedIcon = PackIconKind.Mushroom,
                UnselectedIcon = PackIconKind.MushroomOutline,
                Source = "/Views/PageSQLIte.xaml",
            });

            OpenPageHomeAsync(AppData);

            // Window Title
            AppData.WindowTitle = $"이연준의 DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";

            // SQLite Connection String
            AppData.SQLiteConnectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().IndexOf("DataBaseTools")], "DataBaseTools", "SQLiteTest.db")}";

            // Oracle Connection String
            AppData.OracleConnectionString = JsonData.GetEdcoreWorksJsonData("SeojungriOracle");

            return AppData;
        }


        public static void OpenPageHomeAsync(AppData AppData)
        {
            AppData.SelectedPage = AppData.NavigationList[0];
        }

        public static void OpenPageSQLiteAsync(AppData AppData)
        {
            AppData.SelectedPage = AppData.NavigationList[1];
        }

    }
}
