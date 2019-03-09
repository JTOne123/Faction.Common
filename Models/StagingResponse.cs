using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class StagingResponse
    {
        public StagingResponse() {
            Created = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int StagingMessageId { get; set; }
        public string IV { get; set; }
        public string HMAC { get; set; }
        public string Message { get; set; }
        public DateTime? Created { get; set; }
        public bool Sent { get; set; }
        [JsonIgnore]
        public Agent Agent { get; set; }
        [JsonIgnore]
        public StagingMessage StagingMessage { get; set; }

  }
}
