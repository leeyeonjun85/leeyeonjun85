using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using OoManager.Common;
using OoManager.Models;

namespace OoManager.Services
{
    public interface IOoService
    {
        HubConnection GetChatHubConnection(string ServerAddress, string MethodName, Action<string, string> ReceiveMessageHandler);
        HubConnection GetOoHubConnection(string ServerAddress, string MethodName, Action<OoMessageType, string[]?> ReceiveMessageHandler);
        (AppModel, PageHomeModel) InitApp(AppModel AppModel, PageHomeModel PageHome);
        AppModel InitAppModel(AppModel appModel);
        Task SendAsync(HubConnection hubConnection, string userInput, string messageInput);
        Task StartAsync(HubConnection hubConnection);
    }
}