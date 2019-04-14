using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class Payload
    {
        public Payload() {
          Agents = new HashSet<Agent>();
          Created = DateTime.UtcNow;
          Visible = true;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgentTypeId { get; set; }
        public int AgentTransportTypeId { get; set; }
        public int TransportId { get; set; }
        public int LanguageId { get; set; }
        public string Filename { get; set; }
        public bool? Built { get; set; }
        public string BuildToken { get; set; }
        public string Key { get; set; }
        public int BeaconInterval { get; set; }
        public double Jitter { get; set; }
        public int AgentTypeOperatingSystemId { get; set; }
        public int AgentTypeArchitectureId { get; set; }
        public int AgentTypeVersionId { get; set; }
        public int AgentTypeFormatId { get; set; }
        public int AgentTypeConfigurationId { get; set; }
        public bool Debug { get; set; }
    public DateTime Created { get; set; }
        public DateTime? LastDownloaded { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public Transport Transport { get; set; }
        [JsonIgnore]
        public AgentType AgentType { get; set; }
        [JsonIgnore]
        public Language Language { get; set; }
        [JsonIgnore]
        public ICollection<StagingMessage> StagingMessages { get; set; }
        public AgentTypeArchitecture AgentTypeArchitecture { get; set; }
        public AgentTypeConfiguration AgentTypeConfiguration { get; set; }
        public AgentTypeFormat AgentTypeFormat { get; set; }
        public AgentTypeOperatingSystem AgentTypeOperatingSystem { get; set; }
        public AgentTypeVersion AgentTypeVersion { get; set; }
        public AgentTransportType AgentTransportType { get; set; }
        [JsonIgnore]
        public ICollection<Agent> Agents { get; set; }
  }
}
