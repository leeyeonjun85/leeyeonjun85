using ChatLib.Events;
using ChatLib.Handlers;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ChatLib.Sockets
{
  public class ChatServer : ChatBase
  {
    private readonly TcpListener _listener;

    public override event EventHandler<ChatEventArgs>? Connected;
    public override event EventHandler<ChatEventArgs>? Disconnected;
    public override event EventHandler<ChatEventArgs>? Received;

    public ChatServer(IPAddress iPAddress, int port) : base(iPAddress, port)
    {
      _listener = new TcpListener(iPAddress, port);
    }

    public async Task StartAsync()
    {
      if (IsRunning)
        return;

      try
      {
        _listener.Start();
        IsRunning = true;
        Debug.Print("서버 시작");

        while (true)
        {
          TcpClient client = await _listener.AcceptTcpClientAsync();
          Debug.Print($"클라이언트 연결 수락: {client.Client.Handle}");

          ClientHandler clientHandler = new ClientHandler(client);
          clientHandler.Connected += Connected;
          clientHandler.Disconnected += Disconnected;
          clientHandler.Received += Received;

          _ = clientHandler.HandleClientAsync();
        }
      }
      catch (Exception ex)
      {
        Debug.Print($"서버 시작 중 오류 발생: {ex.Message}");
        IsRunning = false;
      }
    }

    public void Stop()
    {
      IsRunning = false;
      _listener.Stop();      
      Debug.Print("서버 정지");
    }
  }
}
