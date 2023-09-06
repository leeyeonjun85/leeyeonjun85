using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic.ApplicationServices;
using Wpf_SignalR.ViewModels;
using Wpf_SignalR.Views;

namespace Wpf_SignalR.Services
{
    public class SignalRControl : ISignalRControl
    {

        public HubConnection GetHubConnection(Action<string, string> ReceiveMessageHandler, string serverAddress)
        {
            HubConnection hubConnection = new HubConnectionBuilder()
                //.WithUrl(serverAddress, Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets)
                .WithUrl(serverAddress, options => {
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

            hubConnection.On<string, string>("ReceiveMessage", ReceiveMessageHandler);

            return hubConnection;
        }

        public async Task StartAsync(HubConnection hubConnection)
        {
            if (hubConnection is not null)
            {
                await hubConnection.StartAsync();
            }
        }

        public async Task SendAsync(HubConnection hubConnection, string userInput, string messageInput)
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", userInput, messageInput);
            }
        }
    }
}
