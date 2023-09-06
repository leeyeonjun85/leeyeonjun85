using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.ApplicationServices;
using Wpf_SignalR.Models;
using Wpf_SignalR.Services;

namespace Wpf_SignalR.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<ToMainData>>
    {
        private readonly IViewService _viewService;
        private readonly ISignalRControl _signalRControl;
        private HubConnection _hubConnection;
        private readonly Regex _regexIsNumeric = new Regex("[0-9]+"); //regex that matches Numeric

        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        [NotifyCanExecuteChangedFor(nameof(ConnectServerCommand))]
        private string _tbServerIP;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        [NotifyCanExecuteChangedFor(nameof(ConnectServerCommand))]
        private string _tbMyName;

        [ObservableProperty]
        private string _tbMessage = "Sample Message";
        [ObservableProperty]
        private string _tbxMessages = "====== Sample Message ======";

        [ObservableProperty]
        private string _tbSendText = "메시지 보내기";
        [ObservableProperty]
        private bool _tbSendIsEnabled = true;
        [ObservableProperty]
        private bool _btnSendIsEnabled = true;






        private bool IsNotEmpty => (
            TbServerIP != string.Empty
            && TbMyName != string.Empty
            );



        public MainViewModel(
            ISignalRControl signalRControl,
            IViewService viewService)
        {
            _signalRControl = signalRControl;
            _viewService = viewService;

            TbServerIP = ConfigurationManager.AppSettings["ServerIP"]?.ToString() ?? "http://localhost:8080/";
            TbMyName = ConfigurationManager.AppSettings["MyName"]?.ToString() ?? "익명사용자";
            _hubConnection = _signalRControl.GetHubConnection(ReceiveMessageHandler, TbServerIP);

            // SubView에서 message받기
            //WeakReferenceMessenger.Default.RegisterAll(this); // ObservableObject 상속한 경우
            IsActive = true; // ObservableRecipient 상속한 경우
        }

        [RelayCommand(CanExecute = nameof(IsNotEmpty))]
        private async Task ConnectServerAsync(object? obj)
        {
            await _signalRControl.StartAsync(_hubConnection);
            StatusBar1 = _hubConnection.State.ToString();
            StatusBar2 = "원하는 서비스를 실행해 주세요.";
        }

        [RelayCommand]
        private async Task BtnSendAsync(object? obj)
        {
            await _signalRControl.SendAsync(_hubConnection, TbMyName, TbSendText);
        }

        void ReceiveMessageHandler(string user, string message)
        {
            TbxMessages += $"{Environment.NewLine}{user} : {message}";
        }






        public void Receive(ValueChangedMessage<ToMainData> message)
        {
            TbMessage = message.Value.Message;
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
