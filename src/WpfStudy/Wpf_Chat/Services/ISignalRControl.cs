using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Wpf_Chat.Services
{
    public interface ISignalRControl
    {
        string Messages2 { get; set; }

        HubConnection Connect(string serverAddress = "https://localhost:7076/chathub");
        Task Send(string user, string message);
        void StartAsync();
    }
}