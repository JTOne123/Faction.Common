using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class NewConsoleMessage
  {
    public int AgentId;
    public int UserId;
    public string Content;
    public string Display;
  }
  public class ConsoleMessageAnnouncement
  {
    public bool Success;
    public string Username;
    
    public ConsoleMessage ConsoleMessage;
  }
}