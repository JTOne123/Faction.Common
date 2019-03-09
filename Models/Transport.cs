using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class Transport
    {
        public Transport()
        {
            AgentsTransportsXref = new HashSet<AgentsTransportsXref>();
            Payloads = new HashSet<Payload>();
            Created = DateTime.UtcNow;
            Visible = true;
            Enabled = true;
    }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Guid { get; set; }
        public string Configuration { get; set; }
        public int? ApiKeyId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastCheckin { get; set; }
        public ApiKey ApiKey { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        [JsonIgnore]
        public ICollection<Payload> Payloads { get; set; }
        public ICollection<AgentsTransportsXref> AgentsTransportsXref { get; set; }
    }
}
