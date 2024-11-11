using ChatLib.Handlers;
using ChatLib.Models;
using ChatLib.Sockets;
using System.Net;

namespace WinFormClient
{
  public partial class ChatClientForm : Form
  {
    private ChatClient _client;
    private ClientHandler? _clientHandler;

    private int RoomId => (int)nudRoomId.Value;
    private string UserName => txtName.Text;
    private string Message => txtMessage.Text;


    private void RunningStateChanged(bool isRunning)
    {
      btnConnect.Enabled = !isRunning;
      btnStop.Enabled = isRunning;
    }

    private void Connected(object? sender, ChatLib.Events.ChatEventArgs e)
    {
      _clientHandler = e.ClientHandler;
    }

    private void Disconnected(object? sender, ChatLib.Events.ChatEventArgs e)
    {
      _clientHandler = null;
      lbxMsg.Items.Add("서버의 연결이 끊겼습니다.");
    }

    private void Received(object? sender, ChatLib.Events.ChatEventArgs e)
    {
      ChatHub hub = e.Hub;
      string message = hub.State switch
      {
        ChatState.Connect => $"{hub.UserName}님이 접속하였습니다.",
        ChatState.Disconnect => $"{hub.UserName}님이 종료하였습니다.",
        _ => $"{hub.UserName}: {hub.Message}"
      };

      lbxMsg.Items.Add(message);
    }

    private async void BtnConnect_Click(object? sender, EventArgs e)
    {
      await _client.ConnectAsync(new ConnectionDetails
      {
        RoomId = RoomId,
        UserName = UserName,
      });
    }

    private void BtnStop_Click(object? sender, EventArgs e)
    {
      _client.Close();
    }

    private void TxtMessage_KeyDown(object? sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        _clientHandler?.Send(new ChatHub
        {
          RoomId = RoomId,
          UserName = UserName,
          Message = Message,
        });
        txtMessage.Clear();
      }
    }

    public ChatClientForm()
    {
      InitializeComponent();

      _client = new ChatClient(IPAddress.Parse("127.0.0.1"), 8080);
      _client.Connected += Connected;
      _client.Disconnected += Disconnected;
      _client.Received += Received;
      _client.RunningStateChanged += RunningStateChanged;

      btnConnect.Click += BtnConnect_Click;
      btnStop.Click += BtnStop_Click;

      txtMessage.KeyDown += TxtMessage_KeyDown;
    }     
  }
}