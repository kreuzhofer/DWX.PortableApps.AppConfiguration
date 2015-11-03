using System.Collections.Generic;
using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Dto
{
    public class ConfigurationContainer
    {
        [JsonProperty("global")]
        public List<ConfigurationEntry> Global { get; set; }

        [JsonProperty("roles")]
        public List<RoleConfigurationContainer> Roles { get; set; }

        [JsonProperty("users")]
        public List<UserConfigurationContainer> Users { get; set; } 
    }
}
