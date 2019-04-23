using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using Faction.Common;
using Faction.Common.Models;

namespace Faction.Common.Backend.Database
{
    public partial class FactionDbContext : DbContext
    {
        public FactionDbContext()
        {
        }

        public FactionDbContext(DbContextOptions<FactionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentCheckin> AgentCheckin { get; set; }
        public virtual DbSet<AgentsModulesXref> AgentModulesXrefs { get; set; }
        public virtual DbSet<AgentsTransportsXref> AgentsTransportsXref { get; set; }
        public virtual DbSet<AgentTask> AgentTask { get; set; }
        public virtual DbSet<AgentTaskMessage> AgentTaskMessage { get; set; }
        public virtual DbSet<AgentTaskUpdate> AgentTaskUpdate { get; set; }
        public virtual DbSet<AgentTransportType> AgentTransportType { get; set; }
        public virtual DbSet<AgentType> AgentType { get; set; }
        public virtual DbSet<AgentTypeArchitecture> AgentTypeArchitecture { get; set; }
        public virtual DbSet<AgentTypeConfiguration> AgentTypeConfiguration { get; set; }
        public virtual DbSet<AgentTypeFormat> AgentTypeFormat { get; set; }
        public virtual DbSet<AgentTypeOperatingSystem> AgentTypeOperatingSystem { get; set; }
        public virtual DbSet<AgentTypeVersion> AgentTypeVersion { get; set; }
        public virtual DbSet<ApiKey> ApiKey { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<CommandParameter> CommandParameter { get; set; }
        public virtual DbSet<ConsoleMessage> ConsoleMessage { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Payload> Payload { get; set; }
        public virtual DbSet<StagingMessage> StagingMessage { get; set; }
        public virtual DbSet<StagingResponse> StagingResponse { get; set; }
        public virtual DbSet<Transport> Transport { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                FactionSettings config = Utility.GetConfiguration();
                string connectionString = $"Host={config.POSTGRES_HOST};Database={config.POSTGRES_DATABASE};Username={config.POSTGRES_USERNAME};Password={config.POSTGRES_PASSWORD}";
                optionsBuilder.UseNpgsql(connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.Property(e => e.AesPassword).HasColumnType("character varying");

                entity.Property(e => e.ExternalIp)
                    .HasColumnName("ExternalIP")
                    .HasColumnType("character varying");

                entity.Property(e => e.Hostname).HasColumnType("character varying");

                entity.Property(e => e.InternalIp)
                    .HasColumnName("InternalIP")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.OperatingSystem).HasColumnType("character varying");

                entity.Property(e => e.Pid).HasColumnName("PID");

                entity.Property(e => e.Username).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agent_AgentTypeId_fkey");

                entity.HasOne(d => d.Payload)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.PayloadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agent_PayloadId_fkey");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.TransportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agent_TransportId_fkey");

                entity.HasOne(d => d.StagingResponse)
                  .WithOne(b => b.Agent)
                  .HasForeignKey<StagingResponse>(b => b.AgentId);
                
                entity.HasIndex(e => e.Name)
                    .HasName("Agent_Name_key")
                    .IsUnique();
            });

            modelBuilder.Entity<AgentCheckin>(entity =>
            {
                entity.Property(e => e.Message).HasColumnType("character varying");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentCheckins)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("AgentCheckin_AgentId_fkey");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.AgentCheckins)
                    .HasForeignKey(d => d.TransportId)
                    .HasConstraintName("AgentCheckin_TransportId_fkey");
            });

            modelBuilder.Entity<AgentTask>(entity =>
            {
                entity.Property(e => e.Action).HasColumnType("character varying");

                entity.Property(e => e.Command).HasColumnType("character varying");
                entity.HasIndex(e => e.Name)
                    .HasName("AgentTask_Name_key")
                    .IsUnique();
            });

            modelBuilder.Entity<AgentTaskMessage>(entity =>
            {
                entity.Property(e => e.Hmac).HasColumnName("HMAC");

                entity.Property(e => e.Iv).HasColumnName("IV");

                entity.Property(e => e.Message).HasColumnType("character varying");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentTaskMessages)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("AgentTaskMessage_AgentId_fkey");
                
            });


            modelBuilder.Entity<AgentTaskUpdate>(entity =>
            {
                entity.Property(e => e.Message).HasColumnType("character varying");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentTaskUpdates)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("AgentTaskUpdate_AgentId_fkey");

                entity.HasOne(d => d.AgentTask)
                    .WithMany(p => p.AgentTaskUpdates)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("AgentTaskUpdate_TaskId_fkey");
            });

            modelBuilder.Entity<AgentTransportType>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.TransportTypeGuid).HasColumnType("character varying");

                entity.Property(e => e.BuildCommand).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentTransportTypes)
                    .HasForeignKey(d => d.AgentTypeId)
                    .HasConstraintName("AgentTransportType_AgentTypeId_fkey");
            });

            modelBuilder.Entity<AgentsModulesXref>(entity =>
            {
                entity.HasKey(e => new { e.AgentId, e.ModuleId });

                entity.ToTable("AgentModulesXREF");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentsModulesXref)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgentsModulesXREF_AgentId_fkey");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.AgentsModulesXref)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgentsModulesXREF_TransportId_fkey");
            });

            modelBuilder.Entity<AgentsTransportsXref>(entity =>
            {
                entity.HasKey(e => new { e.AgentId, e.TransportId });

                entity.ToTable("AgentsTransportsXREF");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.AgentsTransportsXref)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgentsTransportsXREF_AgentId_fkey");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.AgentsTransportsXref)
                    .HasForeignKey(d => d.TransportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AgentsTransportsXREF_TransportId_fkey");
            });

            modelBuilder.Entity<AgentType>(entity =>
            {
                entity.Property(e => e.Guid).HasColumnType("character varying");

                entity.Property(e => e.Name).HasColumnType("character varying");

              entity.HasOne(d => d.Language)
              .WithMany(p => p.AgentTypes)
              .HasForeignKey(d => d.LanguageId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("AgentType_LanguageId_fkey");
            });

            modelBuilder.Entity<AgentTypeArchitecture>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentTypeArchitectures)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("AgentTypeArchitecture_AgentTypeId_fkey");
            });

                modelBuilder.Entity<AgentTypeConfiguration>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentTypeConfigurations)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("AgentTypeConfiguration_AgentTypeId_fkey");
            });

                modelBuilder.Entity<AgentTypeFormat>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentTypeFormats)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("AgentTypeFormat_AgentTypeId_fkey");
            });

                modelBuilder.Entity<AgentTypeOperatingSystem>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentTypeOperatingSystems)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("AgentTypeOperatingSystem_AgentTypeId_fkey");
            });

                modelBuilder.Entity<AgentTypeVersion>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.AgentTypeVersions)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("AgentTypeVersion_AgentTypeId_fkey");
            });

            modelBuilder.Entity<ApiKey>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.Type).HasColumnType("character varying");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApiKeys)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("ApiKey_UserId_fkey");

                entity.HasIndex(e => e.Name)
                    .HasName("ApiKey_Name_key")
                    .IsUnique();
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");
                entity.Property(e => e.Help).HasColumnType("character varying");
                entity.Property(e => e.MitreReference).HasColumnType("character varying");
                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Commands)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("Command_ModuleId_fkey");
                    
                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Commands)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("Command_AgentTypeId_fkey");
            });

            modelBuilder.Entity<CommandParameter>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");
                entity.Property(e => e.Help).HasColumnType("character varying");
                entity.Property(e => e.Values).HasColumnType("character varying");
                entity.HasOne(d => d.Command)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.CommandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("CommandParameter_CommandId_fkey");
            });

            modelBuilder.Entity<ConsoleMessage>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("character varying");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.ConsoleMessages)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ConsoleMessage_AgentId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ConsoleMessages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ConsoleMessage_UserId_fkey");

              entity.HasOne(d => d.AgentTask)
                  .WithOne(p => p.ConsoleMessage)
                  .HasForeignKey<AgentTask>(d => d.ConsoleMessageId);
            });

            modelBuilder.Entity<ErrorMessage>(entity =>
            {
              entity.Property(e => e.Source).HasColumnType("character varying");
              entity.Property(e => e.Message).HasColumnType("character varying");
              entity.Property(e => e.Details).HasColumnType("character varying");
            });

            modelBuilder.Entity<FactionFile>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");
                entity.Property(e => e.Hash).HasColumnType("character varying");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FactionFiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_FactionFileId_fkey");
            });

            modelBuilder.Entity<IOC>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("character varying");
                entity.Property(e => e.Identifier).HasColumnType("character varying");
                entity.Property(e => e.Action).HasColumnType("character varying");
                entity.Property(e => e.Hash).HasColumnType("character varying");

                entity.HasOne(d => d.AgentTaskUpdate)
                    .WithMany(p => p.IOCs)
                    .HasForeignKey(d => d.AgentTaskUpdateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Agent_IOCId_fkey");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(e => e.Authors).HasColumnType("character varying");
                entity.Property(e => e.BuildCommand).HasColumnType("character varying");
                entity.Property(e => e.BuildLocation).HasColumnType("character varying");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)                    
                    .HasConstraintName("Module_LanguageId_fkey");
            });

            modelBuilder.Entity<Payload>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.HasIndex(e => e.Name)
                    .HasName("Payload_Name_key")
                    .IsUnique();

                entity.Property(e => e.Filename).HasColumnType("character varying");

                entity.HasIndex(e => e.Filename)
                    .HasName("Payload_Filename_key")
                    .IsUnique();

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Payload_AgentTypeId_fkey");

                entity.HasOne(d => d.AgentTransportType)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.AgentTransportTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Payload_AgentTransportTypeId_fkey");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Payload_LanguageId_fkey");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Payloads)
                    .HasForeignKey(d => d.TransportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Payload_TransportId_fkey");
            });

            modelBuilder.Entity<StagingMessage>(entity =>
            {
                entity.Property(e => e.HMAC).HasColumnName("HMAC");

                entity.Property(e => e.IV).HasColumnName("IV");

                entity.Property(e => e.Message).HasColumnType("character varying");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.StagingMessages)
                    .HasForeignKey(d => d.TransportId)
                    .HasConstraintName("StagingMessage_TransportId_fkey");

                entity.HasOne(d => d.Payload)
                    .WithMany(p => p.StagingMessages)
                    .HasForeignKey(d => d.PayloadId)
                    .HasConstraintName("StagingMessage_PayloadId_fkey");
            });

            modelBuilder.Entity<StagingResponse>(entity =>
            {
                entity.Property(e => e.HMAC).HasColumnName("HMAC");

                entity.Property(e => e.IV).HasColumnName("IV");

                entity.Property(e => e.Message).HasColumnType("character varying");

                
              entity.HasOne(d => d.StagingMessage)
                  .WithOne(b => b.StagingResponse)
                  .HasForeignKey<StagingMessage>(b => b.StagingResponseId);
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("character varying");
                entity.Property(e => e.TransportType).HasColumnType("character varying");
                entity.Property(e => e.Guid).HasColumnType("character varying");

                entity.HasOne(d => d.ApiKey)
                  .WithOne(b => b.Transport)
                  .HasForeignKey<ApiKey>(b => b.TransportId);
            });            

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("User_Username_key")
                    .IsUnique();

                entity.Property(e => e.Username).HasColumnType("character varying");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_RoleId_fkey");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UserRole_Name_key")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");
            });
        }
    }
}
