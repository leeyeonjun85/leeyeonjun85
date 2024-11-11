using ChatLib.Events;
using ChatLib.Handlers;
using ChatLib.Models;
using ChatLib.Sockets;
using System.Net;

namespace WinFormServer
{
  public partial class ChatServerForm : Form
  {
    private ChatServer _server;
    private ClientRoomManager _roomManager;

    private ChatHub CreateNewStateChatHub(ChatHub hub, ChatState state)
    {
      return new ChatHub
      {
        RoomId = hub.RoomId,
        UserName = hub.UserName,
        State = state,
      };
    }

    private void AddClientMessageList(ChatHub hub)
    {
      string message = hub.State switch
      {
        ChatState.Connect => $"≮ 立加 ≮ {hub} ≮",
        ChatState.Disconnect => $"≮ 立加 辆丰 ≮ {hub} ≮",
        _ => $"{hub}: {hub.Message}"
      };
      lbxMsg.Items.Add(message);
    }

    private void Connected(object? sender, ChatEventArgs e)
    {
      var hub = CreateNewStateChatHub(e.Hub, ChatState.Connect);

      _roomManager.Add(e.ClientHandler);
      _roomManager.SendToMyRoom(hub);

      lbxClients.Items.Add(e.Hub);
      AddClientMessageList(hub);
    }

    private void Disconnected(object? sender, ChatEventArgs e)
    {
      var hub = CreateNewStateChatHub(e.Hub, ChatState.Disconnect);

      _roomManager.Remove(e.ClientHandler);
      _roomManager.SendToMyRoom(hub);

      lbxClients.Items.Remove(e.Hub);
      AddClientMessageList(hub);
    }

    private void Received(object? sender, ChatEventArgs e)
    {
      _roomManager.SendToMyRoom(e.Hub);

      AddClientMessageList(e.Hub);
    }

    private void RunningStateChanged(bool isRunning)
    {
      btnStart.Enabled = !isRunning;
      btnStop.Enabled = isRunning;
    }

    private void BtnStart_Click(object? sender, EventArgs e)
    {
      _ = _server.StartAsync();
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
      _server.Stop();
    }

    public ChatServerForm()
    {
      InitializeComponent();

      _roomManager = new ClientRoomManager();
      _server = new ChatServer(IPAddress.Parse("127.0.0.1"), 8080);
      _server.Connected += Connected;
      _server.Disconnected += Disconnected;
      _server.Received += Received;
      _server.RunningStateChanged += RunningStateChanged;

      btnStart.Click += BtnStart_Click;
      btnStop.Click += BtnStop_Click;
    }
  }
}