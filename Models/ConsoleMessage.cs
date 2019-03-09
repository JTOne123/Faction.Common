using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class ConsoleMessage
    {
        public ConsoleMessage() {
            Received = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public int AgentId { get; set; }
        public int? UserId { get; set; }
        public int? AgentTaskId { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Display { get; set; }
        public DateTime? Received { get; set; }
        public Agent Agent { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public AgentTask AgentTask { get; set; }
    }
}
