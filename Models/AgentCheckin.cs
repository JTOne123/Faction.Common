using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class AgentCheckin
    {
        public AgentCheckin() {
            Received = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public string SourceIp { get; set; }
        public int TransportId { get; set; }
        public string IV { get; set; }
        public string HMAC { get; set; }
        public string Message { get; set; }
        public DateTime? Received { get; set; }

        [JsonIgnore]
        public Agent Agent { get; set; }

        [JsonIgnore]
        public Transport Transport { get; set; }
  }
}
