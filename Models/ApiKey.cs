using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
  public partial class ApiKey
  {
    public ApiKey()
    {
      Created = DateTime.UtcNow;
      Visible = true;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public byte[] Key { get; set; }
    public int? TransportId { get; set; }
    public bool Enabled { get; set; }
    public bool Visible { get; set; }

    public User User { get; set; }
    public Transport Transport { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? LastUsed { get; set; }
  }
}
