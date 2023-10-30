using DataBaseTools.ViewModels;
using WebSocketSharp;

namespace DataBaseTools.Services
{
    //public class WebSocketClient
    //{
    //    private WebSocket? mWebSocket;
    //    private AppData AppData = App.Data;

    //    public WebSocketClient()
    //    {
    //    }

    //    public void StartConnect()
    //    {
    //        mWebSocket = new WebSocket("ws://localhost:4649/Chat");
    //        mWebSocket.OnOpen += (sender, e) =>
    //        {
    //            AppData.WsChatText += "클라이언트가 연결되었습니다.";
    //        };
    //        mWebSocket.OnMessage += (sender, e) =>
    //        {
    //            AppData.WsChatText += e.Data;
    //        };
    //        mWebSocket.Connect();
    //    }

    //    public void StopConnect()
    //    {
    //        mWebSocket?.Close();
    //        mWebSocket = null;
    //    }

    //    public void SendMessage(string msg)
    //    {
    //        mWebSocket?.Send("nickname: " + msg);
    //    }
    //}
}
