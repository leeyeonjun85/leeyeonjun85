using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using Microsoft.Extensions.Hosting;
using WebSocketSharp;
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
        private void RdBtnModeClick(RadioButton sender)
        {
            switch (sender.Content)
            {
                case "Server Mode":
                    AppData.WsMode = WebSocketMode.Server; break;
                case "Client Mode":
                    AppData.WsMode = WebSocketMode.Client; break;
            }
        }

        [RelayCommand]
        private async Task BtnWebSocketConnectClickAsync(Button sender)
        {
            bool _resultBool = false;

            // Disconnecting
            if (AppData.WsConnected)
            {
                if (AppData.WebSocket is not null && AppData.WebSocket.IsAlive)
                {
                    await SendMessageAsync($"'{AppData.WsChatNickName}' 님께서 퇴장하셨습니다.");
                }
                await Utiles.DisposeWebSocketAsync(AppData);

                AppData.BtnWebSocket.Content = "Connect";
                AppData.BtnWebSocket.Background = new SolidColorBrush(AppData.ColorPrimary);
                AppData.BtnWebSocket.Foreground = new SolidColorBrush(Colors.White);

                AppData.WsConnected = false;
                AppData.StatusBar1 = "Status : Ready"; ;
                AppData.StatusBar2 = "WebSocket 서버 종료";
            }
            // Connecting
            else
            {
                await Task.Run(() =>
                {
                    AppData.Wsipv4 = AppData.WsAddress[AppData.WsAddress.IndexOf("//")..AppData.WsAddress.LastIndexOf(":")][2..];
                    AppData.WsPort = Convert.ToInt32(AppData.WsAddress[AppData.WsAddress.LastIndexOf(":")..AppData.WsAddress.LastIndexOf("/")][1..]);
                    AppData.WsAddress = $"ws://{AppData.Wsipv4}:{AppData.WsPort}/Chat";

                    if (AppData.WsMode is WebSocketMode.Server)
                    {
                        try
                        {
                            AppData.WsServer = new(AppData.WsPort);
                            AppData.WsServer.AddWebSocketService<WebSocketChatServer>("/Chat");
                            AppData.WsServer.Start();

                            _resultBool = AppData.WsServer.IsListening;
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show($"{AppData.WsPort}포트가 이미 사용중입니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                        _resultBool = true;

                    if (_resultBool)
                    {
                        AppData.WebSocket = new(AppData.WsAddress);
                        AppData.WebSocket.OnOpen += async (sender, e) =>
                        {
                            await SendMessageAsync($"'{AppData.WsChatNickName}' 님께서 채팅방에 입장하셨습니다.");
                        };
                        AppData.WebSocket.OnMessage += OnMessageEvent;
                        AppData.WebSocket.Connect();

                        _resultBool = AppData.WebSocket.IsAlive;
                    }
                    else
                        _resultBool = false;
                });

                if (_resultBool)
                {
                    AppData.BtnWebSocket.Content = "Connected";
                    AppData.BtnWebSocket.Background = new SolidColorBrush(AppData.ColorSecondary);
                    AppData.BtnWebSocket.Foreground = new SolidColorBrush(Colors.Black);

                    AppData.WsConnected = true;
                    AppData.StatusBar1 = "Status : Server Running";
                    AppData.StatusBar2 = AppData.WsAddress;
                }
                else
                    MessageBox.Show("서버 연결에 실패하였습니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnMessageEvent(object? sender, MessageEventArgs e)
        {
            AppData.WsChatText += $"{e.Data}{Environment.NewLine}";
        }

        [RelayCommand]
        private async Task TextBoxSendMessageAsync(TextBox _textBox)
        {
            await SendMessageAsync($"{AppData.WsChatNickName} : {_textBox.Text}");
            _textBox.Text = string.Empty;
        }

        private async Task SendMessageAsync(string _sendMessage)
        {
            if (AppData.WebSocket is not null && AppData.WebSocket.IsAlive)
            {
                await Task.Run(() =>
                {
                    AppData.WebSocket?.Send(_sendMessage);
                });
            }
            else
            {
                MessageBox.Show("The current state of the connection is not Open.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            //AppData = message.Value;
        }
    }
}
