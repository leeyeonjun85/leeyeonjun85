using System;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.ApplicationServices;
using OoManager.Common;
using OoManager.Models;
using OoManager.Services;

namespace OoManager.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppModel _appModel = new();
        #endregion


        public MainViewModel(
            OoDbContext ooDbContext,
            IOoService ooService,
            IViewService viewService)
        {
            AppModel.OoDbContext = ooDbContext;
            AppModel.OoService = ooService;
            AppModel.ViewService = viewService;
        }

        [RelayCommand]
        private void SelectionChanged(object obj)
        {
            if (obj is NavigationItem navigationItem)
            {
                if (AppModel.ChatHubConnection is not null)
                    AppModel.ChatHubConnection.DisposeAsync();

                if (AppModel.OoHubConnection is not null)
                    AppModel.OoHubConnection.DisposeAsync();

                switch (navigationItem.Title)
                {
                    case "Home":
                        {
                            AppModel.SelectedIndex = 0;
                            AppModel.PageHomeVisibility = Visibility.Visible;
                            AppModel.PageMembersVisibility = Visibility.Hidden;

                            string chathubSeverAddress = "https://172.30.1.45:7076/chathub";
                            AppModel.ChatHubConnection = AppModel.OoService.GetChatHubConnection(chathubSeverAddress, "ReceiveMessage", chathubReceiveMessageHandler);
                            AppModel.OoService.StartAsync(AppModel.ChatHubConnection);

                            break;
                        }
                    case "Members":
                        {
                            AppModel.SelectedIndex = 1;
                            AppModel.PageHomeVisibility = Visibility.Hidden;
                            AppModel.PageMembersVisibility = Visibility.Visible;

                            string ooHubSeverAddress = "https://172.30.1.45:7076/ooHub";
                            AppModel.OoHubConnection = AppModel.OoService.GetOoHubConnection(ooHubSeverAddress, "OoMessage", ooHubReceiveMessageHandler);
                            AppModel.OoService.StartAsync(AppModel.OoHubConnection);
                            break;
                        }
                    default: throw new Exception();
                }

                AppModel.TestInt += 1;
                ValueChangedMessage<AppModel> message = new(AppModel);
                WeakReferenceMessenger.Default.Send(message);
            }
            else throw new Exception();
        }

        private void chathubReceiveMessageHandler(string user, string message)
        {
            var encodedMsg = $"{user}: {message}";
            AppModel.ChatText += Environment.NewLine + encodedMsg;
        }

        private void ooHubReceiveMessageHandler(OoMessageType ooMessageType, string[]? args)
        {
            MessageBox.Show($"{ooMessageType}");
        }


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            AppModel.OoService?.InitApp(AppModel);
            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
