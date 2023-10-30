using Microsoft.AspNetCore.SignalR;
using OoManager.Models;

namespace BlazorServerSignalR.Hubs
{
    public class OoHub : Hub
    {
        public async Task AttendanceIn(OoMessageType ooMessageType, string[]? args)
        {
            await Clients.All.SendAsync("OoMessage", ooMessageType, args);
        }
    }
}
