using WebSocketSharp;
using WebSocketSharp.Server;

namespace DataBaseTools.Services
{
    public class WebSocketChatServer : WebSocketBehavior
    {

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast(e.Data);
        }
    }
}
