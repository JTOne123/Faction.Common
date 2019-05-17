using System;
using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class NewAgentType {
    public int Id;
    public string Name;
    public string Language;
    public NewAgentType(AgentType agentType) {
      Id = agentType.Id;
      Name = agentType.Name;
      Language = agentType.Language.Name;
    }
  }

  public class NewAgentTransport {
    public int Id;
    public string Name;
    public string Type;
    public NewAgentTransport(Transport transport) {
      Id = transport.Id;
      Name = transport.Name;
      Type = transport.TransportType;
    }
  }
  public class NewAgent
  {
    public int Id { get; set; }
    public string StagingId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Hostname { get; set; }
    public int? Pid { get; set; }
    public string OperatingSystem { get; set; }
    public bool? Admin { get; set; }
    public string InternalIp { get; set; }
    public string ExternalIp { get; set; }
    public DateTime? InitialCheckin { get; set; }
    public int? BeaconInterval { get; set; }
    public double? Jitter { get; set; }
    public bool Enabled { get; set; }
    public bool Visible { get; set; }
    public NewAgentType AgentType { get; set; }
    public NewAgentTransport Transport { get; set; }

    public NewAgent(Agent agent) {
      Id = agent.Id;
      StagingId = agent.StagingId;
      Name = agent.Name;
      Username = agent.Username;
      Hostname = agent.Hostname;
      if (agent.Pid.HasValue) {
        Pid = agent.Pid;
      }
      if (agent.Admin.HasValue) {
        Admin = agent.Admin;
      }
      OperatingSystem = agent.OperatingSystem;
      InternalIp = agent.InternalIp;
      ExternalIp = agent.InternalIp;
      InitialCheckin = agent.InitialCheckin;
      BeaconInterval = agent.BeaconInterval.Value;
      if (agent.Jitter.HasValue) {
        Jitter = agent.Jitter.Value;
      }
      Visible = agent.Visible;
      AgentType = new NewAgentType(agent.AgentType);
      Transport = new NewAgentTransport(agent.Transport);
    }
  }
  
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
    public int TransportId;
    public string SourceIp;
    public string IV = null;
    public string HMAC = null;
    public string Message = null;
  }

  public class AgentCheckinAnnouncement
  {
    public int Id;
    public int TransportId;
    public string TransportName;
    public string SourceIp;
    public DateTime Received;
  }
}