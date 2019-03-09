namespace Faction.Common.Messages
{
  public class LoadModule 
  {
    public string MessageId;
    public string Name;
    public string Language;
  }

  public class ModuleResponse
  {
    public string MessageId;
    public bool Success;
    public string Contents;
  }
}