using System.Collections.Generic;
using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Dto
{
    public class AggregatedConfiguration
    {
        [JsonProperty("entries")]
        public List<ConfigurationEntry> Entries { get; set; }
    }
}
