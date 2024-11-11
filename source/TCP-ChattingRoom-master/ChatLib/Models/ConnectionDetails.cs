namespace ChatLib.Models
{
  public class ConnectionDetails
  {
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int RoomId { get; set; }

    public override string ToString()
    {
      return $"RoomId: {RoomId}, UserName: {UserName}";
    }
  }
}
