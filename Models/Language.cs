using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class Language
  {
    public Language() {
      AgentTypes = new HashSet<AgentType>();
      Modules = new HashSet<Module>();
      Payloads = new HashSet<Payload>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AgentType> AgentTypes { get; set; }

    public ICollection<Module> Modules { get; set; }

    public ICollection<Payload> Payloads { get; set; }
  }
}