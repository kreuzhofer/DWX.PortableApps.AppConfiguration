using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DWX.PortableApps.AppConfiguration.Dto;
using DWX.PortableApps.AppConfiguration.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Tests
{
    [TestClass]
    public class ConfigurationManagerTests
    {
        [TestMethod]
        public void TestSerializeConfiguration()
        {
            var config = new ConfigurationContainer();
            config.Global = new List<ConfigurationEntry>
            {
                new ConfigurationEntry {Key = "maximum_orderposition_amount", Value = "50000"},
                new ConfigurationEntry { Key = "another_configuration_entry", Value = "anothervalue"},
                new ConfigurationEntry{ Key = "settings_flyout_developer_available", Value = "false"}
            };
            config.Roles = new List<RoleConfigurationContainer>
            {
                new RoleConfigurationContainer{ Role = "developer", Entries = new List<ConfigurationEntry>
                {
                    new ConfigurationEntry{ Key = "settings_flyout_developer_available", Value = "true"}
                }}
            };
            var configManager = new AppConfigurationManager();
            var result = configManager.SerializeConfiguration(config);
            Trace.WriteLine("Trace: "+result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestParseConfiguration()
        {
            var configManager = new AppConfigurationManager();
            var stream = this.GetType()
                .Assembly.GetManifestResourceStream("Microsoft.PortableApps.AppConfiguration.Tests.config.json");
            var reader = new StreamReader(stream);
            var testConfiguration = reader.ReadToEnd();
            Trace.WriteLine(testConfiguration);
            var result = configManager.ParseConfiguration(testConfiguration, roles:new []{"developer"});
            Assert.IsTrue(result.Entries != null);
            Assert.IsTrue(result.Entries.Count==3);
            Trace.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
