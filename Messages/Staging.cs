using System;
using Faction.Common.Models;

namespace Faction.Common.Messages
{
  public class NewStagingMessage
  {
    public string PayloadName;
    public string IV = null;
    public string HMAC = null;
    public string Message = null;
  }
}
  