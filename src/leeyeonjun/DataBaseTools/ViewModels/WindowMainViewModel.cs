#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using Microsoft.Extensions.Logging;

namespace DataBaseTools.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public WindowMainViewModel()
        {
            Utiles.InitApp(AppData);
        }

        [RelayCommand]
        private void SelectionChanged(object obj)
        {
            if (obj is NavigationItem SelectedPage)
            {
                switch (SelectedPage.Title)
                {
                    case "Home":
                        {
                            Utiles.OpenPageHome(AppData); break;
                        }
                    case "SQLite":
                        {
                            Utiles.OpenPageSQLite(AppData); break;
                        }

                    default: throw new Exception();
                }

                WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));
            }
            else throw new Exception();
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<AppData>(AppData));
            App.logger.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.logger.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
