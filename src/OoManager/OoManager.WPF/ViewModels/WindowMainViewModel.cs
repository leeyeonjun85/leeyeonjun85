using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
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
        private async Task SelectionChangedAsync(object obj)
        {
            if (obj is NavigationItem navigationItem)
            {
                switch (navigationItem.Name)
                {
                    case "Home":
                        {
                            await Utiles.OpenPageHomeAsync(AppData);
                            break;
                        }
                    case "Members":
                        {
                            await Utiles.OpenPageMembersAsync(AppData);
                            break;
                        }
                    case "Lectures":
                        {
                            await Utiles.OpenPageLectureAsync(AppData);
                            break;
                        }

                    default: throw new Exception();
                }

                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));
            }
            else throw new Exception();
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

            // Init OoDb
            await Utiles.GetOoDbAsync(AppData);

            // Open PageHome
            await Utiles.OpenPageHomeAsync(AppData);

            // SignalR Address
            if (string.IsNullOrEmpty(AppData.SignalRAddress))
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress iPAddress in host.AddressList)
                {
                    if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        AppData.SignalRIPv4 = iPAddress.ToString();
                    }
                }
                AppData.SignalRPort = 6714;
                AppData.SignalRHub = "chathub";
                AppData.SignalRAddress = $"https://{AppData.SignalRIPv4}:{AppData.SignalRPort}/{AppData.SignalRHub}";
            }

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

        [RelayCommand]
        private async Task BtnConnectServerAsync(object obj)
        {
            AppData.SignalRIPv4 = AppData.SignalRAddress[AppData.SignalRAddress.IndexOf("//")..AppData.SignalRAddress.LastIndexOf(":")][2..];
            AppData.SignalRPort = Convert.ToInt32(AppData.SignalRAddress[AppData.SignalRAddress.LastIndexOf(":")..AppData.SignalRAddress.LastIndexOf("/")][1..]);
            AppData.SignalRHub = AppData.SignalRAddress[AppData.SignalRAddress.LastIndexOf($":{AppData.SignalRPort}/")..][$":{AppData.SignalRPort}/".Length..];
            AppData.SignalRAddress = $"https://{App.Data.SignalRIPv4}:{App.Data.SignalRPort}/{App.Data.SignalRHub}";

            await Task.Run(() =>
            {
                ProcessStartInfo psi = new()
                {
                    FileName = "OoManager.Server.exe",
                    Arguments = $"\"{AppData.SignalRIPv4}\" \"{AppData.SignalRPort}\" \"{AppData.SignalRHub}\"",
                    CreateNoWindow = true
                };
                AppData.SignalRServerProcess = Process.Start(psi);

                AppData.IsSignalRConnected = true;
                AppData.NoSignalRConnected = false;

                //AppData.SignalRServerProcess = Process.Start("BlazorServerSignalRApp.exe", new string[3] { AppData.SignalRIPv4, AppData.SignalRPort.ToString(), AppData.SignalRHub });
            });
        }

        protected override async void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            await Utiles.DisposeAllAsync();

            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
