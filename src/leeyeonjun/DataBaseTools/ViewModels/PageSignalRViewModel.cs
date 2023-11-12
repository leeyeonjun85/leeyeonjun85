using System;
using System.Diagnostics;
using System.Net.Http;
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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebSocketSharp;
using WebSocketSharp.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataBaseTools.ViewModels
{
    public partial class PageSignalRViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public PageSignalRViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private void RdBtnModeClick(RadioButton sender)
        {
            switch (sender.Content)
            {
                case "Server Mode":
                    AppData.SignalRMode = SignalRMode.Server; break;
                case "Client Mode":
                    AppData.SignalRMode = SignalRMode.Client; break;
            }
        }

        [RelayCommand]
        private async Task BtnSignalRConnectClickAsync(Button sender)
        {
            bool resultBool = false;

            // Disconnecting
            if (AppData.SignalRConnected)
            {
                if (AppData.SignalRClient is not null && AppData.SignalRClient.State is HubConnectionState.Connected)
                {
                    await SendMessageAsync(AppData.SignalRChatName, $"'{AppData.SignalRChatName}' 님께서 퇴장하셨습니다.");
                }
                await Utiles.DisposeSignalRAsync();

                AppData.BtnSignalRConnect.Content = "Connect";
                AppData.BtnSignalRConnect.Background = new SolidColorBrush(Colors.MidnightBlue);
                AppData.BtnSignalRConnect.Foreground = new SolidColorBrush(Colors.White);

                AppData.SignalRConnected = false;
                AppData.StatusBar1 = "Status : Ready"; ;
                AppData.StatusBar2 = "WebSocket 서버 종료";
            }
            // Connecting
            else
            {
                await Task.Run(() =>
                {
                    AppData.SignalRIPv4 = AppData.SignalRAddress[AppData.SignalRAddress.IndexOf("//")..AppData.SignalRAddress.LastIndexOf(":")][2..];
                    AppData.SignalRPort = Convert.ToInt32(AppData.SignalRAddress[AppData.SignalRAddress.LastIndexOf(":")..AppData.SignalRAddress.LastIndexOf("/")][1..]);
                    AppData.SignalRHub = AppData.SignalRAddress[AppData.SignalRAddress.LastIndexOf($":{AppData.SignalRPort}/")..][$":{AppData.SignalRPort}/".Length..];
                    AppData.SignalRAddress = $"https://{App.Data.SignalRIPv4}:{App.Data.SignalRPort}/{App.Data.SignalRHub}";

                    

                    if (AppData.WsMode is WebSocketMode.Server)
                    {
                        try
                        {
                            //AppData.SignalRServer?.Dispose();
                            //AppData.SignalRServer = Host.CreateDefaultBuilder()
                            //    .ConfigureWebHostDefaults(webBuilder => webBuilder
                            //        .UseUrls($"http://{App.Data.SignalRIPv4}:{App.Data.SignalRPort}")
                            //        .ConfigureServices(services => services.AddSignalR())
                            //        .Configure(app =>
                            //        {
                            //            app.UseRouting();
                            //            app.UseEndpoints(endpoints => endpoints.MapHub<SignalRChatHub>("/signalRChatHub"));
                            //        }))
                            //   .Build();

                            //AppData.SignalRServer.StartAsync();

                            Process.Start("BlazorServerSignalRApp.exe", new string[3] { AppData.SignalRIPv4, AppData.SignalRPort.ToString(), AppData.SignalRHub });

                            AppData.SignalRServerProcess = Process.GetProcessesByName("BlazorServerSignalRApp")[0];

                            if (AppData.SignalRServerProcess is not null && AppData.SignalRServerProcess.ProcessName is "BlazorServerSignalRApp")
                            {
                                resultBool = true;
                            }
                            else
                            {
                                resultBool = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                        resultBool = true;

                    if (resultBool)
                    {
                        AppData.SignalRClient = new HubConnectionBuilder()
                            .WithUrl(AppData.SignalRAddress, options =>
                                {
                                    options.UseDefaultCredentials = true;
                                    options.HttpMessageHandlerFactory = (msg) =>
                                    {
                                        if (msg is HttpClientHandler clientHandler)
                                        {
                                            // bypass SSL certificate
                                            clientHandler.ServerCertificateCustomValidationCallback +=
                                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                                        }

                                        return msg;
                                    };
                                })
                            .Build();

                        AppData.SignalRClient.On<string, string>("ReceiveMessage", (user, message) =>
                        {
                            AppData.SignalRChatText += $"{user} : {message}{Environment.NewLine}";
                        });

                        AppData.SignalRClient.StartAsync();

                        resultBool = true;
                    }
                    else
                        resultBool = false;
                });

                if (resultBool)
                {
                    AppData.BtnSignalRConnect.Content = "Connected";
                    AppData.BtnSignalRConnect.Background = new SolidColorBrush(AppData.ColorSecondary);
                    AppData.BtnSignalRConnect.Foreground = new SolidColorBrush(Colors.Black);

                    AppData.SignalRConnected = true;
                    AppData.StatusBar1 = "Status : Server Running";
                    AppData.StatusBar2 = AppData.SignalRAddress;
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
        private async Task TextBoxSendMessageAsync(TextBox textBox)
        {
            await SendMessageAsync(AppData.SignalRChatName, textBox.Text);
            textBox.Text = string.Empty;
        }

        private async Task SendMessageAsync(string name, string message)
        {
            if (AppData.SignalRClient is not null && AppData.SignalRClient.State is HubConnectionState.Connected)
            {
                await AppData.SignalRClient.SendAsync("SendMessage", name, message);
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
