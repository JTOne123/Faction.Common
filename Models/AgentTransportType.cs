using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class AgentTransportType
    {
      public AgentTransportType() {
        Payloads = new HashSet<Payload>();
      }
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgentTypeId { get; set; }
        public string TransportTypeGuid { get; set; }
        public string BuildCommand { get; set; }
        public string BuildLocation { get; set; }
        [JsonIgnore]
        public AgentType AgentType { get; set; }
        [JsonIgnore]
        public ICollection<Payload> Payloads { get; set; }
  }
}
