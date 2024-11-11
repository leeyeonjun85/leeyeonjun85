namespace ChatLib.Events
{
  public abstract class ChatEventBase
  {
    public abstract event EventHandler<ChatEventArgs>? Connected;
    public abstract event EventHandler<ChatEventArgs>? Disconnected;
    public abstract event EventHandler<ChatEventArgs>? Received;    
  }
}
