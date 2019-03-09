using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class AgentTask
    {
        public AgentTask() {
            Name = Guid.NewGuid().ToString();
            AgentTaskUpdates = new HashSet<AgentTaskUpdate>();
            Created = DateTime.UtcNow;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgentId { get; set; }
        public int? ConsoleMessageId { get; set; }
        public string Action { get; set; }
        public string Command { get; set; }
        public DateTime? Created { get; set; }

        public Agent Agent { get; set; }
        public AgentTaskMessage AgentTaskMessage { get; set; }

        public ICollection<AgentTaskUpdate> AgentTaskUpdates { get; set; }

        public ConsoleMessage ConsoleMessage { get; set; }
  }
}
