using System;
using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class UpdateAgent 
  {
    public int Id;
    public string Name;
    public bool Visible;
  }

  public class AgentUpdated
  {
    public bool Success;
    public Agent Agent;
  }

  public class NewAgentCheckin
  {
    public string AgentName;
    public string IV = null;
    public string HMAC = null;
    public string Message = null;
  }

  public class AgentCheckinAnnouncement
  {
    public int Id;
    public DateTime Received;
  }
}