using System;
using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class NewStagingMessage
  {
    public string PayloadName;
    public int TransportId;
    public string SourceIp;
    public string IV = null;
    public string HMAC = null;
    public string Message = null;
  }
}
  