using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Faction.Common;

namespace Faction.Common.Models
{
    public partial class Agent
    {
        public Agent()
        {
            Name = Utility.GenerateSecureString(12);
            AgentTask = new HashSet<AgentTask>();
            AgentTaskMessages = new HashSet<AgentTaskMessage>();
            AgentCheckins = new HashSet<AgentCheckin>();
            AgentTaskUpdates = new HashSet<AgentTaskUpdate>();
            AgentsModulesXref = new HashSet<AgentsModulesXref>();
            AgentsTransportsXref = new HashSet<AgentsTransportsXref>();
            ConsoleMessages = new HashSet<ConsoleMessage>();
            Visible = true;
        }

        public int Id { get; set; }
        public string StagingId { get; set; }
        public string Name { get; set; }
        public string AesPassword { get; set; }
        public string Username { get; set; }
        public string Hostname { get; set; }
        public int? Pid { get; set; }
        public string OperatingSystem { get; set; }
        public bool? Admin { get; set; }
        public int AgentTypeId { get; set; }
        public int PayloadId { get; set; }
        public int StagingResponseId { get; set; }
        public string InternalIp { get; set; }
        public string ExternalIp { get; set; }
        public DateTime? InitialCheckin { get; set; }
        public DateTime? LastCheckin { get; set; }
        public int? BeaconInterval { get; set; }
        public double? Jitter { get; set; }
        public bool Visible { get; set; }

        public AgentType AgentType { get; set; }
        public Payload Payload { get; set; }
        [JsonIgnore]
        public StagingResponse StagingResponse { get; set; }
        [JsonIgnore]
        public ICollection<AgentTask> AgentTask { get; set; }
        [JsonIgnore]
        public ICollection<AgentTaskMessage> AgentTaskMessages { get; set; }
        [JsonIgnore]
        public ICollection<AgentCheckin> AgentCheckins { get; set; }
        [JsonIgnore]
        public ICollection<AgentTaskUpdate> AgentTaskUpdates { get; set; }
        [JsonIgnore]
        public ICollection<AgentsModulesXref> AgentsModulesXref { get; set; }
        [JsonIgnore]
        public ICollection<AgentsTransportsXref> AgentsTransportsXref { get; set; }
        [JsonIgnore]
        public ICollection<ConsoleMessage> ConsoleMessages { get; set; }
    }
}
