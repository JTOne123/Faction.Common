using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Faction.Common.Models;

namespace Faction.Common.Backend.Database
{
  public class FactionRepository : IDisposable
  {
    #region Set DBContext
    private readonly FactionDbContext _dbContext;
    private readonly ILogger<FactionRepository> _logger;
    public FactionRepository(FactionDbContext dbContext, ILogger<FactionRepository> logger)
    {
      _dbContext = dbContext;
      _logger = logger;
    }
    #endregion

    // TODO rename to a better name? 
    public T Add<T>(T TMessage) where T : class
    {
        _dbContext.Set<T>().Add(TMessage);
        _dbContext.SaveChanges();
        var type = typeof(T).Name;
        _logger.LogInformation($"Added new {type}");
        return TMessage;
    }

    public T Update<T>(int id, T TMessage) where T : class 
    {
      var dbEntry = _dbContext.Set<T>().Find(id);
      _dbContext.Entry(dbEntry).CurrentValues.SetValues(TMessage);
      _dbContext.SaveChanges();
        
        var type = typeof(T).Name;
        _logger.LogInformation($"Updated {type}");
        return TMessage;
    }

    public Agent GetAgent(int AgentId)
    {
      var agent = _dbContext.Agent.Find(AgentId);
      Console.WriteLine();
      return agent;
    }

    public Agent GetAgent(string AgentName)
    {
      return _dbContext.Agent
                      .Where(b => b.Name == AgentName)
                      .First();
    }
    
    public User GetUser(int UserId)
    {
      return _dbContext.User.Find(UserId);
    }

    public AgentTask GetAgentTask(int AgentTaskId)
    {
      return _dbContext.AgentTask.Find(AgentTaskId);
    }
    
    public AgentTask GetAgentTask(string AgentTaskName)
    {
      return _dbContext.AgentTask
                      .Where(b => b.Name == AgentTaskName)
                      .First();
    }
    
    // public Agent GetAgent(Agent AgentId)
    // {
    //     var agent = _dbContext.Agent.Find(AgentId);
    //     return agent;
    // }

    public List<AgentsModulesXref> GetAgentModules(int AgentId)
    {
      return _dbContext.AgentModulesXrefs.Where(b => b.AgentId == AgentId).ToList();
    }

    public AgentType GetAgentType(int AgentTypeId)
    {
      return _dbContext.AgentType.Find(AgentTypeId);
    }

    public AgentType GetAgentType(string AgentTypeName)
    {
      return _dbContext.AgentType
                        .Where(b => b.Name == AgentTypeName)
                        .FirstOrDefault();
    }

    public List<Command> GetAgentTypeCommands(int AgentTypeId)
    {
      return _dbContext.Command.Where(b => b.AgentTypeId == AgentTypeId).ToList();
    }

    public AgentTypeArchitecture GetAgentTypeArchitecture(int AgentTypeArchitectureId)
    {
      return _dbContext.AgentTypeArchitecture.Find(AgentTypeArchitectureId);
    }

    public List<AgentTypeArchitecture> GetAgentTypeArchitectures(int AgentTypeId)
    {
      return _dbContext.AgentTypeArchitecture.Where(b => b.AgentTypeId == AgentTypeId).ToList();
    }
    
    public AgentTypeConfiguration GetAgentTypeConfiguration(int AgentTypeConfigurationId)
    {
      return _dbContext.AgentTypeConfiguration.Find(AgentTypeConfigurationId);
    }
    
    public List<AgentTypeConfiguration> GetAgentTypeConfigurations(int AgentTypeId)
    {
      return _dbContext.AgentTypeConfiguration.Where(b => b.AgentTypeId == AgentTypeId).ToList();
    }
    
    public AgentTypeFormat GetAgentTypeFormat(int AgentTypeFormatId)
    {
      return _dbContext.AgentTypeFormat.Find(AgentTypeFormatId);
    }
    
    public List<AgentTypeFormat> GetAgentTypeFormats(int AgentTypeId)
    {
      return _dbContext.AgentTypeFormat.Where(b => b.AgentTypeId == AgentTypeId).ToList();
    }
    
    public AgentTypeOperatingSystem GetAgentTypeOperatingSystem(int AgentTypeOperatingSystemId)
    {
      return _dbContext.AgentTypeOperatingSystem.Find(AgentTypeOperatingSystemId);
    }
    
    public List<AgentTypeOperatingSystem> GetAgentTypeOperatingSystems(int AgentTypeId)
    {
      return _dbContext.AgentTypeOperatingSystem.Where(b => b.AgentTypeId == AgentTypeId).ToList();
    }
    
    public AgentTypeVersion GetAgentTypeVersion(int AgentTypeVersionId)
    {
      return _dbContext.AgentTypeVersion.Find(AgentTypeVersionId);
    }
    
    public List<AgentTypeVersion> GetAgentTypeVersions(int AgentTypeId)
    {
      return _dbContext.AgentTypeVersion.Where(b => b.AgentTypeId == AgentTypeId).ToList();
    }
    

    public AgentTransportType GetAgentTransportType(int AgentTransportTypeId)
    {
      return _dbContext.AgentTransportType.Find(AgentTransportTypeId);
    }

    public ApiKey GetApiKey(int ApiKeyId)
    {
      return _dbContext.ApiKey.Find(ApiKeyId);
    }

    public Command GetCommand(int CommandId)
    {
      return _dbContext.Command.Find(CommandId);
    }

    public Command GetCommand(string CommandName)
    {
      return _dbContext.Command.Where(b => b.Name.ToLower() == CommandName.ToLower()).First();
    }

    public List<Command> GetCommands(int ModuleId)
    {
      return _dbContext.Command.Where(b => b.ModuleId == ModuleId).ToList();
    }

    public CommandParameter GetCommandParameter(string CommandName, string ParameterName)
    {
      Command command = _dbContext.Command.Where(b => b.Name.ToLower() == CommandName.ToLower()).First();
      return _dbContext.CommandParameter.Where(b => b.CommandId == command.Id)
                                        .Where(b => b.Name.ToLower() == ParameterName.ToLower()).First();
    }

    public CommandParameter GetCommandParameter(string CommandName, int Position)
    {
      Command command = _dbContext.Command.Where(b => b.Name.ToLower() == CommandName.ToLower()).First();
      return _dbContext.CommandParameter.Where(b => b.CommandId == command.Id)
                                        .Where(b => b.Position == Position).First();
    }

    public List<CommandParameter> GetCommandParameters(int CommandId) {
      return _dbContext.CommandParameter.Where(b => b.CommandId == CommandId).ToList();
    }
    public Language GetLanguage(string LanguageName)
    {
      return _dbContext.Language.Where(b => b.Name == LanguageName).First();
    }

    public Language GetLanguage(int LanguageId)
    {
      return _dbContext.Language.Find(LanguageId);
    }
    // public List<Module> GetModules(string ModuleName)
    // {
    //   return _dbContext.Module.Where(b => b.Name == ModuleName).ToList();
    // }

    public Module GetModule(string ModuleName, string LanguageName)
    {
      Language language = GetLanguage(LanguageName);
      return _dbContext.Module.Where(b => b.Name == ModuleName)
                              .Where(b => b.LanguageId == language.Id).First();
    }
    public Module GetModule(int ModuleId)
    {
      return _dbContext.Module.Find(ModuleId);
    }
    public List<Module> GetModules(string LanguageName)
    {
      Language language = GetLanguage(LanguageName);
      return GetModules(language.Id);
    }

    public List<Module> GetModules(int LanguageId)
    {
      return _dbContext.Module.Where(b => b.LanguageId == LanguageId).ToList();
    }

    public Payload GetPayload(int PayloadId) 
    {
      return _dbContext.Payload.Find(PayloadId);
    }

    public Payload GetPayload(string PayloadName) 
    {
      var Payload = _dbContext.Payload
                  .Where(b => b.Name == PayloadName)
                  .First();
      return Payload;
    }

    public Transport GetTransport(int TransportId)
    {
      return _dbContext.Transport.Find(TransportId);
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          _dbContext.Dispose();
        }

        disposedValue = true;
      }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion
  }
}