using ChatLib.Events;
using System.Net;

namespace ChatLib.Sockets
{
  public abstract class ChatBase : ChatEventBase
  {
    private bool _isRunning;

    protected readonly IPAddress IPAddress;
    protected readonly int Port;

    public event Action<bool>? RunningStateChanged;

    public ChatBase(IPAddress iPAddress, int port)
    {
      IPAddress = iPAddress;
      Port = port;
    }

    public bool IsRunning
    {
      get => _isRunning;
      set
      {
        if (_isRunning != value)
        {
          _isRunning = value;
          RunningStateChanged?.Invoke(_isRunning);
        }
      }
    }
  }
}
