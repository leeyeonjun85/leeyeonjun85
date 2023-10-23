#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System.Configuration;
using System.IO;
using System.Windows.Media;
using DataBaseTools.ViewModels;
using MaterialDesignThemes.Wpf;

namespace DataBaseTools.Services
{
    public class Utiles
    {
        public static AppData InitApp(AppData AppData)
        {
            // Color Theme
            Color primaryColor = Color.FromArgb(255, 0, 31, 158);
            Color secondaryColor = Color.FromArgb(255, 34, 158, 0);

            PaletteHelper paletteHelper = new ();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(primaryColor);
            theme.SetSecondaryColor(secondaryColor);
            paletteHelper.SetTheme(theme);

            // Window Title
            AppData.WindowTitle = $"이연준의 DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";

            // SQLite Connection String
            AppData.SQLiteConnectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().IndexOf("DataBaseTools")], "DataBaseTools", "SQLiteTest.db")}";

            // Oracle Connection String
            AppData.OracleConnectionString = JsonData.GetEdcoreWorksJsonData("SeojungriOracle");

            return AppData;
        }
    }
}
