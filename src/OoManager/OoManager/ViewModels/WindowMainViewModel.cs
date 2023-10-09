using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using OoManager.Models;
using OoManager.Services;

namespace OoManager.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        #endregion

        public WindowMainViewModel()
        {
            AppData.OoService = (IAppUtiles)Ioc.Default.GetService(typeof(IAppUtiles))!; ;
        }

        [RelayCommand]
        private async Task SelectionChangedAsync(object obj)
        {
            if (obj is NavigationItem navigationItem)
            {
                switch (navigationItem.Title)
                {
                    case "Home":
                        {
                            Task<AppData> _appData = AppData.OoService!.OpenPageHomeAsync(AppData);
                            await _appData; AppData = _appData.Result;
                            break;
                        }
                    case "Members":
                        {
                            Task<AppData> _appData = AppData.OoService!.OpenPageMembersAsync(AppData);
                            await _appData; AppData = _appData.Result;
                            break;
                        }
                    case "Lectures":
                        {
                            Task<AppData> _appData = AppData.OoService!.OpenPageLectureAsync(AppData);
                            await _appData; AppData = _appData.Result;
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
            Task<AppData> _appData = AppData.OoService!.InitAppAsync(AppData);
            await _appData; AppData = _appData.Result;
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
