using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class AgentTypeArchitecture
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int AgentTypeId { get; set; }
    [JsonIgnore]
    public AgentType AgentType { get; set; }
  }
}
