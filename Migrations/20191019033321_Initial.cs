using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Faction.Common.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Source = table.Column<string>(type: "character varying", nullable: true),
                    Message = table.Column<string>(type: "character varying", nullable: true),
                    Details = table.Column<string>(type: "character varying", nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    TransportType = table.Column<string>(type: "character varying", nullable: true),
                    Guid = table.Column<string>(type: "character varying", nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    ApiKeyId = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    LastCheckin = table.Column<DateTime>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Visible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Guid = table.Column<string>(type: "character varying", nullable: true),
                    Authors = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    BuildCommand = table.Column<string>(nullable: true),
                    BuildLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentType", x => x.Id);
                    table.ForeignKey(
                        name: "AgentType_LanguageId_fkey",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Authors = table.Column<string>(type: "character varying", nullable: true),
                    BuildCommand = table.Column<string>(type: "character varying", nullable: true),
                    BuildLocation = table.Column<string>(type: "character varying", nullable: true),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "Module_LanguageId_fkey",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying", nullable: true),
                    Password = table.Column<byte[]>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    Authenticated = table.Column<bool>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Visible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "User_RoleId_fkey",
                        column: x => x.RoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTransportType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false),
                    TransportTypeGuid = table.Column<string>(type: "character varying", nullable: true),
                    BuildCommand = table.Column<string>(type: "character varying", nullable: true),
                    BuildLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTransportType", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTransportType_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentTypeArchitecture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypeArchitecture", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTypeArchitecture_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTypeConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypeConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTypeConfiguration_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTypeFormat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypeFormat", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTypeFormat_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTypeOperatingSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypeOperatingSystem", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTypeOperatingSystem_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTypeVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypeVersion", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTypeVersion_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Command",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Help = table.Column<string>(type: "character varying", nullable: true),
                    MitreReference = table.Column<string>(type: "character varying", nullable: true),
                    OpsecSafe = table.Column<bool>(nullable: false),
                    Artifacts = table.Column<string>(nullable: true),
                    ModuleId = table.Column<int>(nullable: true),
                    AgentTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.Id);
                    table.ForeignKey(
                        name: "Command_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Command_ModuleId_fkey",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiKey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Type = table.Column<string>(type: "character varying", nullable: true),
                    Key = table.Column<byte[]>(nullable: true),
                    TransportId = table.Column<int>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    LastUsed = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKey_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ApiKey_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payload",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false),
                    AgentTransportTypeId = table.Column<int>(nullable: false),
                    TransportId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Filename = table.Column<string>(type: "character varying", nullable: true),
                    Built = table.Column<bool>(nullable: true),
                    BuildToken = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    BeaconInterval = table.Column<int>(nullable: false),
                    Jitter = table.Column<double>(nullable: false),
                    AgentTypeOperatingSystemId = table.Column<int>(nullable: false),
                    AgentTypeArchitectureId = table.Column<int>(nullable: false),
                    AgentTypeVersionId = table.Column<int>(nullable: false),
                    AgentTypeFormatId = table.Column<int>(nullable: false),
                    AgentTypeConfigurationId = table.Column<int>(nullable: false),
                    Debug = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastDownloaded = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Visible = table.Column<bool>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payload", x => x.Id);
                    table.ForeignKey(
                        name: "Payload_AgentTransportTypeId_fkey",
                        column: x => x.AgentTransportTypeId,
                        principalTable: "AgentTransportType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payload_AgentTypeArchitecture_AgentTypeArchitectureId",
                        column: x => x.AgentTypeArchitectureId,
                        principalTable: "AgentTypeArchitecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payload_AgentTypeConfiguration_AgentTypeConfigurationId",
                        column: x => x.AgentTypeConfigurationId,
                        principalTable: "AgentTypeConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payload_AgentTypeFormat_AgentTypeFormatId",
                        column: x => x.AgentTypeFormatId,
                        principalTable: "AgentTypeFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Payload_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payload_AgentTypeOperatingSystem_AgentTypeOperatingSystemId",
                        column: x => x.AgentTypeOperatingSystemId,
                        principalTable: "AgentTypeOperatingSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payload_AgentTypeVersion_AgentTypeVersionId",
                        column: x => x.AgentTypeVersionId,
                        principalTable: "AgentTypeVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Payload_LanguageId_fkey",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Payload_TransportId_fkey",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommandParameter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Help = table.Column<string>(type: "character varying", nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Position = table.Column<int>(nullable: true),
                    Values = table.Column<string>(type: "character varying", nullable: true),
                    CommandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandParameter", x => x.Id);
                    table.ForeignKey(
                        name: "CommandParameter_CommandId_fkey",
                        column: x => x.CommandId,
                        principalTable: "Command",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StagingId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    AesPassword = table.Column<string>(type: "character varying", nullable: true),
                    Username = table.Column<string>(type: "character varying", nullable: true),
                    Hostname = table.Column<string>(type: "character varying", nullable: true),
                    PID = table.Column<int>(nullable: true),
                    OperatingSystem = table.Column<string>(type: "character varying", nullable: true),
                    Admin = table.Column<bool>(nullable: true),
                    AgentTypeId = table.Column<int>(nullable: false),
                    PayloadId = table.Column<int>(nullable: false),
                    StagingResponseId = table.Column<int>(nullable: false),
                    InternalIP = table.Column<string>(type: "character varying", nullable: true),
                    ExternalIP = table.Column<string>(type: "character varying", nullable: true),
                    InitialCheckin = table.Column<DateTime>(nullable: true),
                    LastCheckin = table.Column<DateTime>(nullable: true),
                    TransportId = table.Column<int>(nullable: false),
                    BeaconInterval = table.Column<int>(nullable: true),
                    Jitter = table.Column<double>(nullable: true),
                    Visible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                    table.ForeignKey(
                        name: "Agent_AgentTypeId_fkey",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Agent_PayloadId_fkey",
                        column: x => x.PayloadId,
                        principalTable: "Payload",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Agent_TransportId_fkey",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentCheckin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentId = table.Column<int>(nullable: true),
                    SourceIp = table.Column<string>(nullable: true),
                    TransportId = table.Column<int>(nullable: false),
                    IV = table.Column<string>(nullable: true),
                    HMAC = table.Column<string>(nullable: true),
                    Message = table.Column<string>(type: "character varying", nullable: true),
                    Received = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentCheckin", x => x.Id);
                    table.ForeignKey(
                        name: "AgentCheckin_AgentId_fkey",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "AgentCheckin_TransportId_fkey",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentModulesXREF",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentModulesXREF", x => new { x.AgentId, x.ModuleId });
                    table.ForeignKey(
                        name: "AgentsModulesXREF_AgentId_fkey",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "AgentsModulesXREF_TransportId_fkey",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentsTransportsXREF",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false),
                    TransportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentsTransportsXREF", x => new { x.AgentId, x.TransportId });
                    table.ForeignKey(
                        name: "AgentsTransportsXREF_AgentId_fkey",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "AgentsTransportsXREF_TransportId_fkey",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsoleMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    AgentTaskId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Content = table.Column<string>(type: "character varying", nullable: true),
                    Display = table.Column<string>(nullable: true),
                    Received = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleMessage", x => x.Id);
                    table.ForeignKey(
                        name: "ConsoleMessage_AgentId_fkey",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ConsoleMessage_UserId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FactionFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying", nullable: true),
                    Hash = table.Column<string>(type: "character varying", nullable: true),
                    HashMatch = table.Column<bool>(nullable: true),
                    AgentId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastDownloaded = table.Column<DateTime>(nullable: true),
                    Visible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactionFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactionFile_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "User_FactionFileId_fkey",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StagingResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentId = table.Column<int>(nullable: true),
                    StagingMessageId = table.Column<int>(nullable: false),
                    IV = table.Column<string>(nullable: true),
                    HMAC = table.Column<string>(nullable: true),
                    Message = table.Column<string>(type: "character varying", nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Sent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagingResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StagingResponse_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    AgentId = table.Column<int>(nullable: false),
                    ConsoleMessageId = table.Column<int>(nullable: true),
                    Action = table.Column<string>(type: "character varying", nullable: true),
                    Command = table.Column<string>(type: "character varying", nullable: true),
                    Created = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentTask_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgentTask_ConsoleMessage_ConsoleMessageId",
                        column: x => x.ConsoleMessageId,
                        principalTable: "ConsoleMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StagingMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentName = table.Column<string>(nullable: true),
                    PayloadId = table.Column<int>(nullable: false),
                    SourceIp = table.Column<string>(nullable: true),
                    TransportId = table.Column<int>(nullable: false),
                    PayloadName = table.Column<string>(nullable: true),
                    StagingId = table.Column<string>(nullable: true),
                    StagingResponseId = table.Column<int>(nullable: true),
                    IV = table.Column<string>(nullable: true),
                    HMAC = table.Column<string>(nullable: true),
                    Message = table.Column<string>(type: "character varying", nullable: true),
                    Received = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagingMessage", x => x.Id);
                    table.ForeignKey(
                        name: "StagingMessage_PayloadId_fkey",
                        column: x => x.PayloadId,
                        principalTable: "Payload",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StagingMessage_StagingResponse_StagingResponseId",
                        column: x => x.StagingResponseId,
                        principalTable: "StagingResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "StagingMessage_TransportId_fkey",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgentTaskMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentId = table.Column<int>(nullable: true),
                    AgentTaskId = table.Column<int>(nullable: true),
                    IV = table.Column<string>(nullable: true),
                    HMAC = table.Column<string>(nullable: true),
                    Message = table.Column<string>(type: "character varying", nullable: true),
                    Sent = table.Column<bool>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTaskMessage", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTaskMessage_AgentId_fkey",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentTaskMessage_AgentTask_AgentTaskId",
                        column: x => x.AgentTaskId,
                        principalTable: "AgentTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentTaskUpdate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgentId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    TaskName = table.Column<string>(nullable: true),
                    Message = table.Column<string>(type: "character varying", nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ContentId = table.Column<string>(nullable: true),
                    Complete = table.Column<bool>(nullable: true),
                    Success = table.Column<bool>(nullable: true),
                    Received = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTaskUpdate", x => x.Id);
                    table.ForeignKey(
                        name: "AgentTaskUpdate_AgentId_fkey",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "AgentTaskUpdate_TaskId_fkey",
                        column: x => x.TaskId,
                        principalTable: "AgentTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IOC",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(type: "character varying", nullable: true),
                    Hash = table.Column<string>(type: "character varying", nullable: true),
                    Action = table.Column<string>(type: "character varying", nullable: true),
                    AgentTaskUpdateId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOC", x => x.Id);
                    table.ForeignKey(
                        name: "Agent_IOCId_fkey",
                        column: x => x.AgentTaskUpdateId,
                        principalTable: "AgentTaskUpdate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOC_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgentTypeId",
                table: "Agent",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "Agent_Name_key",
                table: "Agent",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agent_PayloadId",
                table: "Agent",
                column: "PayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_TransportId",
                table: "Agent",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentCheckin_AgentId",
                table: "AgentCheckin",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentCheckin_TransportId",
                table: "AgentCheckin",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentModulesXREF_ModuleId",
                table: "AgentModulesXREF",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentsTransportsXREF_TransportId",
                table: "AgentsTransportsXREF",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTask_AgentId",
                table: "AgentTask",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTask_ConsoleMessageId",
                table: "AgentTask",
                column: "ConsoleMessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AgentTask_Name_key",
                table: "AgentTask",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentTaskMessage_AgentId",
                table: "AgentTaskMessage",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTaskMessage_AgentTaskId",
                table: "AgentTaskMessage",
                column: "AgentTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentTaskUpdate_AgentId",
                table: "AgentTaskUpdate",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTaskUpdate_TaskId",
                table: "AgentTaskUpdate",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTransportType_AgentTypeId",
                table: "AgentTransportType",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentType_LanguageId",
                table: "AgentType",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTypeArchitecture_AgentTypeId",
                table: "AgentTypeArchitecture",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTypeConfiguration_AgentTypeId",
                table: "AgentTypeConfiguration",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTypeFormat_AgentTypeId",
                table: "AgentTypeFormat",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTypeOperatingSystem_AgentTypeId",
                table: "AgentTypeOperatingSystem",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentTypeVersion_AgentTypeId",
                table: "AgentTypeVersion",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "ApiKey_Name_key",
                table: "ApiKey",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiKey_TransportId",
                table: "ApiKey",
                column: "TransportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiKey_UserId",
                table: "ApiKey",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Command_AgentTypeId",
                table: "Command",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Command_ModuleId",
                table: "Command",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandParameter_CommandId",
                table: "CommandParameter",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsoleMessage_AgentId",
                table: "ConsoleMessage",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsoleMessage_UserId",
                table: "ConsoleMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FactionFile_AgentId",
                table: "FactionFile",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_FactionFile_UserId",
                table: "FactionFile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IOC_AgentTaskUpdateId",
                table: "IOC",
                column: "AgentTaskUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_IOC_UserId",
                table: "IOC",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_LanguageId",
                table: "Module",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTransportTypeId",
                table: "Payload",
                column: "AgentTransportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTypeArchitectureId",
                table: "Payload",
                column: "AgentTypeArchitectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTypeConfigurationId",
                table: "Payload",
                column: "AgentTypeConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTypeFormatId",
                table: "Payload",
                column: "AgentTypeFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTypeId",
                table: "Payload",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTypeOperatingSystemId",
                table: "Payload",
                column: "AgentTypeOperatingSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Payload_AgentTypeVersionId",
                table: "Payload",
                column: "AgentTypeVersionId");

            migrationBuilder.CreateIndex(
                name: "Payload_Filename_key",
                table: "Payload",
                column: "Filename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payload_LanguageId",
                table: "Payload",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "Payload_Name_key",
                table: "Payload",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payload_TransportId",
                table: "Payload",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_StagingMessage_PayloadId",
                table: "StagingMessage",
                column: "PayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_StagingMessage_StagingResponseId",
                table: "StagingMessage",
                column: "StagingResponseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StagingMessage_TransportId",
                table: "StagingMessage",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_StagingResponse_AgentId",
                table: "StagingResponse",
                column: "AgentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "User_Username_key",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserRole_Name_key",
                table: "UserRole",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentCheckin");

            migrationBuilder.DropTable(
                name: "AgentModulesXREF");

            migrationBuilder.DropTable(
                name: "AgentsTransportsXREF");

            migrationBuilder.DropTable(
                name: "AgentTaskMessage");

            migrationBuilder.DropTable(
                name: "ApiKey");

            migrationBuilder.DropTable(
                name: "CommandParameter");

            migrationBuilder.DropTable(
                name: "ErrorMessage");

            migrationBuilder.DropTable(
                name: "FactionFile");

            migrationBuilder.DropTable(
                name: "IOC");

            migrationBuilder.DropTable(
                name: "StagingMessage");

            migrationBuilder.DropTable(
                name: "Command");

            migrationBuilder.DropTable(
                name: "AgentTaskUpdate");

            migrationBuilder.DropTable(
                name: "StagingResponse");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "AgentTask");

            migrationBuilder.DropTable(
                name: "ConsoleMessage");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Payload");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "AgentTransportType");

            migrationBuilder.DropTable(
                name: "AgentTypeArchitecture");

            migrationBuilder.DropTable(
                name: "AgentTypeConfiguration");

            migrationBuilder.DropTable(
                name: "AgentTypeFormat");

            migrationBuilder.DropTable(
                name: "AgentTypeOperatingSystem");

            migrationBuilder.DropTable(
                name: "AgentTypeVersion");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "AgentType");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
