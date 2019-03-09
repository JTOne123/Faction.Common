using System;
using System.Collections.Generic;

namespace Faction.Common.Models
{
    public partial class AgentsTransportsXref
    {
        public int AgentId { get; set; }
        public int TransportId { get; set; }

        public Agent Agent { get; set; }
        public Transport Transport { get; set; }
    }
}
