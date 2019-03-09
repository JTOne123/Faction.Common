using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class Command
  {
    public Command()
    {
      Parameters = new HashSet<CommandParameter>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Help { get; set; }
    public string MitreReference { get; set; }
    public bool OpsecSafe { get; set; }
    public string Artifacts { get; set; }
    public int? ModuleId { get; set; }
    public int? AgentTypeId { get; set; }
    [JsonIgnore]
    public Module Module { get; set; }
    [JsonIgnore]
    public AgentType AgentType { get; set; }
    public IEnumerable<CommandParameter> Parameters { get; set; }
  }
}