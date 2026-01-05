using Microsoft.AspNetCore.SignalR.Client;

namespace Wpf_Chat.Models
{
  public class SubData
  {
    public string NickName { get; set; } = string.Empty;
    public HubConnection? HConnection { get; set; }
    }
}
