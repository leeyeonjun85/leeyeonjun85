using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Wpf_Chat.ViewModels;
using Wpf_Chat.Views;

namespace Wpf_Chat.Services
{
    public class SignalRControl : ViewModelBase, ISignalRControl
    {
        private HubConnection? _hubConnection;

        public string Messages2
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        private string _messages = "채팅을 시작합니다.222";

        public HubConnection Connect(string serverAddress = @"https://172.30.1.45:7222//chatHub")
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(serverAddress)
                .Build();

            //_hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            //{
            //    //Messages.Add($"{user}: {message}");
            //    Messages += $"{Environment.NewLine}{user} : {message}";
            //});

            //await hubConnection.StartAsync();

            //isConnected = IsConnected;

            return _hubConnection;
        }

        public async void StartAsync()
        {
            if (_hubConnection is not null)
            {
                _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    //Messages.Add($"{user}: {message}");
                    Messages2 += $"{Environment.NewLine}{user} : {message}";
                });
                await _hubConnection.StartAsync();
            }
        }

        public async Task Send(string userInput, string messageInput)
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.SendAsync("SendMessage", userInput, messageInput);
            }
        }
    }
}
