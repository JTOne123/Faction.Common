using System;
using Faction.Common.Models;

public class NewErrorMessage 
{
  public string Source { get; set; }
  public string Message { get; set; }
  public string Details { get; set; }
}

public class ErrorMessageAnnouncement 
{
  public int Id { get; set; }
  public string Source { get; set; }
  public string Message { get; set; }
  public string Details { get; set; }
  public DateTime Timestamp { get; set; }
}