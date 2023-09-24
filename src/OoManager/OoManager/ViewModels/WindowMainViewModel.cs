using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using OoManager.Common;
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


        public WindowMainViewModel(
            OoDbContext ooDbContext,
            IOoService ooService)
        {
            AppData.OoDbContext = ooDbContext;
            AppData.OoService = ooService;
        }


        [RelayCommand]
        private void SelectionChanged(object obj)
        {
            if (obj is NavigationItem navigationItem)
            {
                switch (navigationItem.Title)
                {
                    case "Home":
                        {
                            AppData.PageHomeVisibility = Visibility.Visible;
                            AppData.PageMembersVisibility = Visibility.Hidden;

                            break;
                        }
                    case "Members":
                        {
                            AppData.PageHomeVisibility = Visibility.Hidden;
                            AppData.PageMembersVisibility = Visibility.Visible;

                            break;
                        }
                    default: throw new Exception();
                }
                AppData.TestInt += 1;

                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));

                //object[] message = new object[] { AppData, PageHome };
                //WeakReferenceMessenger.Default.Send(new ValueChangedMessage<object[]>(message));
            }
            else throw new Exception();
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            AppData = AppData.OoService.InitApp(AppData);
            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
