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
            AgentTypeFormats = new HashSet<AgentTypeFormat>();
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
        public ICollection<Agent> Agents { get; set; }
        [JsonIgnore]
        public ICollection<AgentTransportType> AgentTransportTypes { get; set; }
        public ICollection<AgentTypeFormat> AgentTypeFormats { get; set; }
        public ICollection<Command> Commands { get; set; }
    public ICollection<Payload> Payloads { get; set; }
  }
}
