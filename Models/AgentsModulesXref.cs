using System;
using System.Collections.Generic;

namespace Faction.Common.Models
{
    public partial class AgentsModulesXref
    {
        public int AgentId { get; set; }
        public int ModuleId { get; set; }

        public Agent Agent { get; set; }
        public Module Module { get; set; }
    }
}
