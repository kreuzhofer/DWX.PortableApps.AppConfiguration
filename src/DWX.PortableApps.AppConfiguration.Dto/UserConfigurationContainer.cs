using System.Collections.Generic;
using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Dto
{
    public class UserConfigurationContainer
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("entries")]
        public List<ConfigurationEntry> Entries { get; set; } 
    }
}