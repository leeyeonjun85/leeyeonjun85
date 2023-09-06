using Microsoft.AspNetCore.SignalR;

namespace BlazorServerSignalR.Hubs
{
    public class AttendanceCheck : Hub
    {
        public async Task AttendanceIn(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
