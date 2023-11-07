#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseTools.Models;
using DataBaseTools.Services;
using DataBaseTools.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;

namespace DataBaseTools.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public WindowMainViewModel()
        {

        }

        [RelayCommand]
        private void SelectionChanged(NavigationItem SelectedPage)
        {
            if (SelectedPage is null) return;

            AppData.SelectedPage = SelectedPage;
            Utiles.PageNavigationSelectionChanged(AppData);
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Color Theme
            PaletteHelper paletteHelper = new();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(AppData.ColorPrimary);
            theme.SetSecondaryColor(AppData.ColorSecondary);
            paletteHelper.SetTheme(theme);

            AppData.NavigationList.Add(new NavigationItem
            {
                Name = "Home",
                Title = "Data Base Tools by Lee Yeon-jun",
                SelectedIcon = PackIconKind.Home,
                UnselectedIcon = PackIconKind.HomeOutline,
                Source = "/Views/PageHome.xaml",
                IsEnabled = true,
                IsVisibility = Visibility.Visible,
            });

            AppData.NavigationList.Add(new NavigationItem
            {
                Name = "SQLite",
                Title = "SQLite Data Base",
                SelectedIcon = PackIconKind.Mushroom,
                UnselectedIcon = PackIconKind.MushroomOutline,
                Source = "/Views/PageSQLIte.xaml",
                IsEnabled = false,
                IsVisibility = Visibility.Hidden,
            });

            AppData.NavigationList.Add(new NavigationItem
            {
                Name = "WebSocket",
                Title = "Chatting in WebSocket",
                SelectedIcon = PackIconKind.Connection,
                UnselectedIcon = PackIconKind.Connection,
                Source = "/Views/PageWebSocket.xaml",
                IsEnabled = true,
                IsVisibility = Visibility.Hidden,
            });

            AppData.BtnSQLite.Content = "Connect";
            AppData.BtnSQLite.Background = new SolidColorBrush(AppData.ColorPrimary);
            AppData.BtnSQLite.Foreground = new SolidColorBrush(Colors.White);

            AppData.BtnWebSocket.Content = "Connect";
            AppData.BtnWebSocket.Background = new SolidColorBrush(AppData.ColorPrimary);
            AppData.BtnWebSocket.Foreground = new SolidColorBrush(Colors.White);

            AppData.BtnOracleConnect.Content = "Connect";
            AppData.BtnOracleConnect.Background = new SolidColorBrush(AppData.ColorPrimary);
            AppData.BtnOracleConnect.Foreground = new SolidColorBrush(Colors.White);

            // Window Title
            AppData.WindowTitle = $"이연준의 DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";

            // SQLite
            AppData.SQLiteConnectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().IndexOf("DataBaseTools")], "DataBaseTools", "SQLiteTest.db")}";

            if (sender is WindowMain _windowMain)
            {
                if (_windowMain.Content is Grid _grid)
                {
                    foreach (var _ui in _grid.Children)
                    {
                        if (_ui is Grid _teporaryPageGrid)
                        {
                            if (_teporaryPageGrid.Name is "TemporaryPage")
                            {
                                _teporaryPageGrid.Visibility = Visibility.Hidden;
                            }
                        }
                    }
                }

            }

            App.logger.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override async void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            await Utiles.DisposeAllAsync(AppData);
            App.logger.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
