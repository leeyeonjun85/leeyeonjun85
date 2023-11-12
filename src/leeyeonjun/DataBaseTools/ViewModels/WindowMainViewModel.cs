#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataBaseTools.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public string WindowTitle { get; } = $"이연준의 DB Tool - {ConfigurationManager.AppSettings["Version"]}({ConfigurationManager.AppSettings["LastUpdateDate"]})";


        public WindowMainViewModel()
        { }

        [RelayCommand]
        private void SelectionChanged(ListBox listBox)
        {
            if (listBox is null) return;

            AppData.SelectedPage = listBox.SelectedItem as NavigationItem;
            AppData.SelectedIndex = listBox.SelectedIndex;
            Utiles.PageNavigationSelectionChanged(listBox.SelectedIndex);
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Color Theme
            PaletteHelper paletteHelper = new();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(AppData.ColorPrimary);
            theme.SetSecondaryColor(AppData.ColorSecondary);
            paletteHelper.SetTheme(theme);

            // Set Page Navigation Items
            AppData.SelectedPage = AppData.NavigationList[Pages.Home];
            AppData.SelectedIndex = Pages.Home;
            Utiles.PageNavigationSelectionChanged(Pages.Home);

            // SQLite
            AppData.SQLiteConnectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().IndexOf("DataBaseTools")], "DataBaseTools", "SQLiteTest.db")}";

            AppData.BtnSQLite.Content = AppData.BtnOracleConnect.Content = AppData.BtnWebSocket.Content
                = AppData.BtnSignalRConnect.Content = "Connect";
            AppData.BtnSQLite.Background = AppData.BtnOracleConnect.Background = AppData.BtnWebSocket.Background
                = AppData.BtnSignalRConnect.Background = new SolidColorBrush(App.Data.ColorPrimary);
            AppData.BtnSQLite.Foreground = AppData.BtnOracleConnect.Foreground = AppData.BtnWebSocket.Foreground
                = AppData.BtnSignalRConnect.Foreground = new SolidColorBrush(Colors.White);

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
            App.Data.SignalRServerProcess?.Kill();
            App.logger.LogInformation("프로그램이 종료되었습니다.");
            await Utiles.DisposeAllAsync();
        }
    }
}
