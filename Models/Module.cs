using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class Module
  {
    public Module() {
      Commands = new HashSet<Command>();
      AgentsModulesXref = new HashSet<AgentsModulesXref>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Authors { get; set; }
    public string BuildCommand { get; set; }
    public string BuildLocation { get; set; }
    public int LanguageId { get; set; }
    [JsonIgnore]
    public Language Language { get; set; }

    public ICollection<Command> Commands { get; set; }
    public ICollection<AgentsModulesXref> AgentsModulesXref { get; set; }

  }
}