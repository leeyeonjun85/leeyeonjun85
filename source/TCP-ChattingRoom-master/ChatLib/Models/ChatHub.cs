using System.Text.Json;

namespace ChatLib.Models
{
  public class ChatHub : ConnectionDetails
  {
    public static ChatHub? Parse(string json) => JsonSerializer.Deserialize<ChatHub>(json)!;    

    public ChatState State { get; set; }
    public string Message { get; set; } = string.Empty;

    public string ToJsonString() => JsonSerializer.Serialize(this);
  }  
}
