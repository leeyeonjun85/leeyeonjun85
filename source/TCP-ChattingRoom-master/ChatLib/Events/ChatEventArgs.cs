using ChatLib.Handlers;
using ChatLib.Models;

namespace ChatLib.Events
{
  public class ChatEventArgs : EventArgs
  {
    public ChatEventArgs(ClientHandler clientHandler, ChatHub hub)
    {
      ClientHandler = clientHandler;
      Hub = hub;
    }

    public ClientHandler ClientHandler { get; }
    public ChatHub Hub { get; }
  }
}
