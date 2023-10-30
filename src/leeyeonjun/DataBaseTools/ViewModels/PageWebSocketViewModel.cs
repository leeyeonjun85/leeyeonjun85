using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Services;
using Microsoft.Extensions.Hosting;
using WebSocketSharp.Server;

namespace DataBaseTools.ViewModels
{
    public partial class PageWebSocketViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        private IHost? webHost;


        public PageWebSocketViewModel()
        {
            IsActive = true;
        }


        [RelayCommand]
        private async Task BtnWebSocketSeverStartClickAsync(object? obj)
        {
            await Task.Run(() =>
            {
                if (AppData.WsServer is not null)
                    return;

                int port = 4649;
                AppData.WsServer = new WebSocketServer(port);
                AppData.WsServer.AddWebSocketService<WebSocketChatServer>("/Chat");
                AppData.WsServer.Start();

                AppData.StatusBar1 = "Status : WebSocket Sever Start"; ;
                AppData.StatusBar2 = "WebSocket 서버 시작";
            });
        }


        [RelayCommand]
        private async Task BtnWebSocketSeverStopClickAsync(object? obj)
        {
            await Task.Run(() =>
            {
                AppData.WsServer?.Stop();
                AppData.WsServer = null;

                AppData.StatusBar1 = "Status : WebSocket Sever Stop"; ;
                AppData.StatusBar2 = "WebSocket 서버 종료";
            });
        }

        [RelayCommand]
        private async Task BtnWebSocketClientStartClickAsync(object? obj)
        {
            await Task.Run(() =>
            {
                AppData.WebSocket = new("ws://localhost:4649/Chat");
                AppData.WebSocket.OnOpen += (sender, e) =>
                {
                    AppData.WebSocket?.Send($"'{AppData.WsChatNickName}' 님께서 채팅방에 입장하셨습니다.");
                };
                AppData.WebSocket.OnMessage += (sender, e) =>
                {
                    AppData.WsChatText += $"{e.Data}{Environment.NewLine}";
                };
                AppData.WebSocket.Connect();
            });
        }

        [RelayCommand]
        private async Task BtnWebSocketClientStopClickAsync(object? obj)
        {
            await Task.Run(() =>
            {
                AppData.WebSocket?.Close();
                AppData.WebSocket = null;
            });
        }

        [RelayCommand]
        private async Task BtnWebSocketChatSendTextClickAsync(object? obj)
        {
            await Task.Run(() =>
            {
                AppData.WebSocket?.Send($"{AppData.WsChatNickName} : {AppData.WsChatSendText}");
                AppData.WsChatSendText = string.Empty;
            });
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            //AppData = message.Value;
        }
    }
}
