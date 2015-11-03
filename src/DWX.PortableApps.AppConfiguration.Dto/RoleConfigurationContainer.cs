using System.Collections.Generic;
using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Dto
{
    public class RoleConfigurationContainer
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("entries")]
        public List<ConfigurationEntry> Entries { get; set; }

    }
}