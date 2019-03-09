using System;
using System.Collections.Generic;

namespace Faction.Common.Models
{
    public partial class AgentTaskUpdate
    {
        public AgentTaskUpdate() {
            Received = DateTime.UtcNow;
            IOCs = new HashSet<IOC>();
    }
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int? TaskId { get; set; }
        public string TaskName { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string ContentId { get; set; }
        public bool? Complete { get; set; }
        public bool? Success { get; set; }
        public DateTime? Received { get; set; }

        public Agent Agent { get; set; }
        public AgentTask AgentTask { get; set; }
        public ICollection<IOC> IOCs { get; set; }
  }
}
