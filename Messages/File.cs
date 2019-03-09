using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class NewFile 
  {
    public int UserId;
    public int AgentId;
    public string ContentId;
    public string Content;
  }
}