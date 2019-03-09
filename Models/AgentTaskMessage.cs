using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class AgentTaskMessage
    {
        public AgentTaskMessage() {
            Created = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int? AgentTaskId { get; set; }
        public string Iv { get; set; }
        public string Hmac { get; set; }
        public string Message { get; set; }
        public bool? Sent { get; set; }
        public DateTime? Created { get; set; }
        [JsonIgnore]
        public Agent Agent { get; set; }
        [JsonIgnore]
        public AgentTask AgentTask { get; set; }
    }
}
