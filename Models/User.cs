using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Models
{
    public partial class User
    {
        public User()
        {
            ConsoleMessages = new HashSet<ConsoleMessage>();
            ApiKeys = new HashSet<ApiKey>();
      FactionFiles = new HashSet<FactionFile>();
      Created = DateTime.UtcNow;
            Visible = true;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public int RoleId { get; set; }
        public bool? Authenticated { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }

        public UserRole Role { get; set; }
        [JsonIgnore]
        public ICollection<ConsoleMessage> ConsoleMessages { get; set; }
        public ICollection<ApiKey> ApiKeys { get; set; }
        public ICollection<FactionFile> FactionFiles { get; set; }
  }
}
