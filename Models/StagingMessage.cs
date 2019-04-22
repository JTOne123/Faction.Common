using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class StagingMessage
    {
        public StagingMessage() {
            Received = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string AgentName { get; set; }
        public int PayloadId { get; set; }
        public string SourceIp { get; set; }
        public int TransportId { get; set; }
        public string PayloadName { get; set; }
        public string StagingId { get; set; }
        public int? StagingResponseId { get; set; }
        public string IV { get; set; }
        public string HMAC { get; set; }
        public string Message { get; set; }
        public DateTime? Received { get; set; }
        public Payload Payload { get; set; }
        public Transport Transport { get; set; }
    public StagingResponse StagingResponse { get; set; }


  }
}
