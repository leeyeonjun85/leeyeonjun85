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
        private AppData _appData = App.Data;
        #endregion

        public WindowMainViewModel()
        {
            AppData.OoService = (IAppUtiles)Ioc.Default.GetService(typeof(IAppUtiles))!; ;
            AppData.OoDbContext = (OoDbContext)Ioc.Default.GetService(typeof(OoDbContext))!; ;
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
            await Utiles.InitAppAsync(AppData);
            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
