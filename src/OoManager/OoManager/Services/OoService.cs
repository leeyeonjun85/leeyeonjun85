using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using OoManager.Common;
using OoManager.Models;

namespace OoManager.Services
{
    public class OoService : IOoService
    {
        public (AppModel, PageHomeModel) InitApp(AppModel AppModel, PageHomeModel PageHome)
        {
            //AppModel.NavigationList = new()
            //{
            //    new NavigationItem
            //    {
            //        Title = "Home",
            //        SelectedIcon = PackIconKind.Home,
            //        UnselectedIcon = PackIconKind.HomeOutline,
            //        Source = "/Views/PageHome.xaml",
            //    },
            //    new NavigationItem
            //    {
            //        Title = "Members",
            //        SelectedIcon = PackIconKind.Users,
            //        UnselectedIcon = PackIconKind.UsersOutline,
            //        Source = "/Views/PageMembers.xaml",
            //    },
            //};




            AppModel.SelectedIndex = 0;


            //AppModel.OoDbContext.Database.EnsureCreated();
            //AppModel.CanConnectDb = AppModel.OoDbContext.Database.CanConnect();
            //AppModel.OoDbContext.members.Load();
            //AppModel.Members = AppModel.OoDbContext.members.Local.ToObservableCollection();

            return (AppModel, PageHome);
        }


        public AppModel InitAppModel(AppModel AppModel)
        {
            AppModel.SelectedIndex = 0;


            return AppModel;
        }


        public HubConnection GetChatHubConnection(string ServerAddress, string MethodName, Action<string, string> ReceiveMessageHandler)
        {
            HubConnection hubConnection = new HubConnectionBuilder()
                //.WithUrl(serverAddress, Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets)
                .WithUrl(ServerAddress, options =>
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

            hubConnection.On<string, string>(MethodName, ReceiveMessageHandler);

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

        public HubConnection GetOoHubConnection(string ServerAddress, string MethodName, Action<OoMessageType, string[]?> ReceiveMessageHandler)
        {
            HubConnection hubConnection = new HubConnectionBuilder()
                //.WithUrl(serverAddress, Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets)
                .WithUrl(ServerAddress, options =>
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

            hubConnection.On<OoMessageType, string[]?>(MethodName, ReceiveMessageHandler);

            return hubConnection;
        }

        
    }
}
