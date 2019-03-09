using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class IOC
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }  
        public string Identifier { get; set; }
        public string Hash { get; set; } 
        public string Action { get; set; } 
        public int AgentTaskUpdateId { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public AgentTaskUpdate AgentTaskUpdate { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
