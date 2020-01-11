using System;
using System.Collections.Generic;
using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class NewPayload
  {
    public string Name;
    public string Description;
    public string BuildToken;
    public int AgentTypeId;
    public int AgentTypeFormatId;
    public int AgentTransportTypeId;
    public int TransportId;
    public int OperatingSystemId { get; set; }
    public int ArchitectureId { get; set; }
    public int VersionId { get; set; }
    public int FormatId { get; set; }
    public int AgentTypeConfigurationId { get; set; }
    public double Jitter;
    public int BeaconInterval;
    public DateTime? ExpirationDate;
    public bool Debug;
  }

  public class PayloadAPIMessage
  {
    public int Id;
    public string Name;
    public string Description;
    public string Key;
    public DateTime Created;
    public bool Built;
    public DateTime? LastDownloaded;
    public double Jitter;
    public int BeaconInterval;
    public DateTime? ExpirationDate;
    public bool Enabled;
    public bool Debug;
    public bool Visible;
    
    public Dictionary<string, string> AgentType;
    public Dictionary<string, string> Architecture;
    public Dictionary<string, string> Configuration;
    public Dictionary<string, string> OperatingSystem;
    public Dictionary<string, string> Format;
    public Dictionary<string, string> AgentTransport;
    public Dictionary<string, string> Transport;

    public PayloadAPIMessage(Payload payload)
    {
      Id = payload.Id;
      Name = payload.Name;
      Description = payload.Description;
      Key = payload.Key;
      Created = payload.Created;
      LastDownloaded = payload.LastDownloaded;
      Jitter = payload.Jitter;
      BeaconInterval = payload.BeaconInterval;
      ExpirationDate = payload.ExpirationDate;
      Enabled = payload.Enabled;
      Debug = payload.Debug;
      Visible = payload.Visible;

      if (payload.Built.HasValue)
      {
        Built = payload.Built.Value; 
      } else {
        Built = false;
      }
      
      AgentType = new Dictionary<string, string>
      {
        {"Id", payload.AgentType.Id.ToString()}, {"Name", payload.AgentType.Name}
      };

      Architecture = new Dictionary<string, string>
      {
        {"Id", payload.AgentTypeArchitecture.Id.ToString()}, {"Name", payload.AgentTypeArchitecture.Name}
      };

      Configuration = new Dictionary<string, string>
      {
        {"Id", payload.AgentTypeConfiguration.Id.ToString()}, {"Name", payload.AgentTypeConfiguration.Name}
      };

      OperatingSystem = new Dictionary<string, string>
      {
        {"Id", payload.AgentTypeOperatingSystem.Id.ToString()}, {"Name", payload.AgentTypeOperatingSystem.Name}
      };

      Format = new Dictionary<string, string>
      {
        {"Id", payload.AgentTypeFormat.Id.ToString()}, {"Name", payload.AgentTypeFormat.Name}
      };

      AgentTransport = new Dictionary<string, string>
      {
        {"Id", payload.AgentTransportType.Id.ToString()}, {"Name", payload.AgentTransportType.Name}
      };

      Transport = new Dictionary<string, string>
      {
        {"Id", payload.Transport.Id.ToString()}, {"Name", payload.Transport.Name}
      };

    }
  }

  public class PayloadCreated
  {
    public bool Success;
    public PayloadAPIMessage Payload;

    public PayloadCreated(bool success, Payload payload)
    {
      Success = success;
      Payload = new PayloadAPIMessage(payload);
    }
  }
  
  public class DevPayloadCreated
  {
    public bool Success;
    public string StagingKey;
    public double Jitter;
    public int BeaconInterval;
    public DateTime? ExpirationDate;
  }

  public class UpdatePayload 
  {
    public int Id;
    public double Jitter;
    public int BeaconInterval;
    public DateTime? ExpirationDate;
    public bool Enabled;
    public bool Visible;
  }

  public class PayloadUpdated
  {
    public bool Success;
    public Payload Payload;
  }
}