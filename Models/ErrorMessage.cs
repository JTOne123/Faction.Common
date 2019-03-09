using System;
using System.Collections.Generic;

namespace Faction.Common.Models
{
  public partial class ErrorMessage
  {
    public ErrorMessage()
    {
      Timestamp = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public string Source { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
  }
}
