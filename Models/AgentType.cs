using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class AgentType
  {
    public AgentType()
    {
      Agents = new HashSet<Agent>();
      Payloads = new HashSet<Payload>();
      AgentTransportTypes = new HashSet<AgentTransportType>();
      AgentTypeArchitectures = new HashSet<AgentTypeArchitecture>();
      AgentTypeConfigurations = new HashSet<AgentTypeConfiguration>();
      AgentTypeFormats = new HashSet<AgentTypeFormat>();
      AgentTypeOperatingSystems = new HashSet<AgentTypeOperatingSystem>();
      AgentTypeVersions = new HashSet<AgentTypeVersion>();
      Commands = new HashSet<Command>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Guid { get; set; }
    public string Authors { get; set; }
    public int LanguageId { get; set; }
    [JsonIgnore]
    public Language Language { get; set; }
    [JsonIgnore]
    public string BuildCommand { get; set; }
    public string BuildLocation { get; set; }
    public ICollection<Agent> Agents { get; set; }
    [JsonIgnore]
    public ICollection<AgentTransportType> AgentTransportTypes { get; set; }
    public ICollection<AgentTypeArchitecture> AgentTypeArchitectures { get; set; }
    public ICollection<AgentTypeConfiguration> AgentTypeConfigurations { get; set; }
    public ICollection<AgentTypeFormat> AgentTypeFormats { get; set; }
    public ICollection<AgentTypeOperatingSystem> AgentTypeOperatingSystems { get; set; }
    public ICollection<AgentTypeVersion> AgentTypeVersions { get; set; }
    public ICollection<Command> Commands { get; set; }
    public ICollection<Payload> Payloads { get; set; }
  }
}
