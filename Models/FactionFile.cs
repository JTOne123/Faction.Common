using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class FactionFile
    {
        public FactionFile() {
            Created = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }  
        public bool? HashMatch { get; set; }
        public int? AgentId { get; set; } 
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastDownloaded { get; set; }
        public bool Visible { get; set; }

    [JsonIgnore]
        public Agent Agent { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
