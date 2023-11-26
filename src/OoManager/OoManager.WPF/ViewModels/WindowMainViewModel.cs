using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using OoManager.Common.Models;
using OoManager.WPF.Models;
using OoManager.WPF.Services;
using OoManager.WPF.Views;

namespace OoManager.WPF.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = App.Data;
        #endregion

        public WindowMainViewModel()
        {
            AppData.OoDbContext = (OoDbContext)Ioc.Default.GetService(typeof(OoDbContext))!;
        }

        [RelayCommand]
        private async Task SelectionChangedAsync(ListBox listBox)
        {
            if (listBox is ListBox)
            {
                AppData.SelectedPage = listBox.SelectedItem as NavigationItem;
                AppData.SelectedIndex = listBox.SelectedIndex;

                switch (listBox.SelectedIndex)
                {
                    case PagesIndex.Home:
                        {
                            await Utiles.OpenPageHomeAsync(AppData); break;
                        }
                    case PagesIndex.Members:
                        {
                            await Utiles.OpenPageMembersAsync(AppData); break;
                        }
                    case PagesIndex.Lectures:
                        {
                            await Utiles.OpenPageLectureAsync(AppData); break;
                        }

                    default: break;
                }

                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));
            }
            else 
            {
                //throw new Exception();
            }
            
        }

        [RelayCommand]
        private async Task BtnConnectDataBaseAsync(Grid gridMain)
        {
            if (gridMain is null) return;

            if (AppData.IsOoDbConnected)
            {
                Utiles.DisposeSQLite();
                Utiles.TurnNaviButton(gridMain, PagesName.Members, false);
                Utiles.TurnNaviButton(gridMain, PagesName.Lectures, false);
                await Utiles.RefreshNaviItemsAsync();
            }
            else
            {
                await Utiles.GetSQLiteAsync();
                Utiles.TurnNaviButton(gridMain, PagesName.Members, true);
                Utiles.TurnNaviButton(gridMain, PagesName.Lectures, true);
                await Utiles.RefreshNaviItemsAsync();
            }
        }

        [RelayCommand]
        private async Task BtnConnectServerAsync(object obj)
        {
            if (obj is null) return;

            if (AppData.IsSignalRConnected)
            {
                Utiles.DisposeSignalR();
            }
            else
            {
                await Utiles.GetSignalRAsync();
            }
        }

        protected async override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Color Theme
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.Indigo];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            PaletteHelper paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(primaryColor);
            theme.SetSecondaryColor(accentColor);
            paletteHelper.SetTheme(theme);

            // Init NaviItems
            await Utiles.RefreshNaviItemsAsync();

            // Open PageHome
            await Utiles.OpenPageHomeAsync(AppData);

            // SignalR Address
            //await Utiles.GetSignalRAddressAsync();
            //await Utiles.RefreshOoDbAsync();

            if (sender is WindowMain windowMain)
            {
                windowMain.Title = $"오투공부방 Manger - {ConfigurationManager.AppSettings["Version"]}";
                //if (windowMain.Content is Grid _grid)
                //{
                //    foreach (var _ui in _grid.Children)
                //    {
                //        if (_ui is Grid _teporaryPageGrid)
                //        {
                //            if (_teporaryPageGrid.Name is "TemporaryPage")
                //            {
                //                _teporaryPageGrid.Visibility = Visibility.Hidden;
                //            }
                //        }
                //    }
                //}
            }

            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            Utiles.DisposeAll();
            App.Data.OoDbContext?.Dispose();
            App.Data.OoDbContext = null;
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
