using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class CommandParameter
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Help { get; set; }
    public bool Required { get; set; }
    public int? Position { get; set; }
    public string Values { get; set; }
    public int CommandId { get; set; }
    [JsonIgnore]
    public Command Command { get; set; }
  }
}