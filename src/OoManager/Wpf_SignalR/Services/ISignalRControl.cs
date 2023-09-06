using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Wpf_SignalR.Services
{
    public interface ISignalRControl
    {
        HubConnection GetHubConnection(Action<string, string> ReceiveMessageHandler, string serverAddress);
        Task SendAsync(HubConnection hubConnection, string userInput, string messageInput);
        Task StartAsync(HubConnection hubConnection);
    }
}