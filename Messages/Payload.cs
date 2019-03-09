using System;
using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class UpdatePayload 
  {
    public int Id;
    public double Jitter;
    public int BeaconInterval;
    public DateTime ExpirationDate;
    public bool Enabled;
    public bool Visible;
  }

  public class PayloadUpdated
  {
    public bool Success;
    public Payload Payload;
  }
}