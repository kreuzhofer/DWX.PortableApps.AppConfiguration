using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Dto
{
    public class ConfigurationEntry
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
