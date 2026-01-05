using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Wpf_Chat.Models;
using Wpf_Chat.Services;

namespace Wpf_Chat.ViewModels
{
    public class SubViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly IViewService _viewService;
        private readonly IConfiguration _configuration;
        private readonly ISignalRControl _signalRControl;
        private string _nickName = string.Empty;
        private HubConnection? _hubConnection;

        public SubData SubData { get; set; } = default!;

        public string Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        private string _messages = "채팅을 시작합니다.111";

        public string TbStatusBar1Text
        {
            get { return _tbStatusBar1Text; }
            set { SetProperty(ref _tbStatusBar1Text, value); }
        }
        private string _tbStatusBar1Text = "Status: Ready";

        public string TbStatusBar2Text
        {
            get { return _tbStatusBar2Text; }
            set { SetProperty(ref _tbStatusBar2Text, value); }
        }
        private string _tbStatusBar2Text = "Please Connect Server first!";



        public SubViewModel(
            IViewService viewService,
            ISignalRControl signalRControl,
            IConfiguration configuration)
        {
            _viewService = viewService;
            _signalRControl = signalRControl;
            _configuration = configuration;
        }


        private void SendMessage(object obj)
        {

            //await Task.Delay(10);
            //Thread.Sleep(10);

            if (_hubConnection is not null)
            {
                _hubConnection.SendAsync("SendMessage", _nickName, ((TextBox)obj).Text);
            }
        }

        public async void StartAsync()
        {
            if (_hubConnection is not null)
            {
                _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    //Messages.Add($"{user}: {message}");
                    Messages += $"{Environment.NewLine}{user} : {message}";
                });
                await _hubConnection.StartAsync();
            }
        }


        public void ReceiveParameter(object parameter)
        {
            if (parameter is SubData subData)
            {
                SubData = subData;
            }
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _nickName = SubData.NickName;
            _hubConnection = SubData.HConnection;

            StartAsync();
            //_signalRControl.StartAsync();
            //_signalRControl.Send(_nickName, "채팅방에 입장하였습니다.");
            //MessageBox.Show("SubWindow Loaded");

            TbStatusBar1Text = "Status : Connected";
            TbStatusBar2Text = "SignalR Running!";
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            //MessageBox.Show("SubWindow Closing");
        }



        public ICommand CloseCommand => new RelayCommand<object>(_ => Window?.Close());
        public ICommand SendMessageCommand => new RelayCommand<object>(SendMessage!);
    }
}
