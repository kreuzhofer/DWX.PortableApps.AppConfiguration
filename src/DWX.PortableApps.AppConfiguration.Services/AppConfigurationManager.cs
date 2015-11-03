using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DWX.PortableApps.AppConfiguration.Dto;
using Newtonsoft.Json;

namespace DWX.PortableApps.AppConfiguration.Services
{
    public class AppConfigurationManager : IAppConfigurationManager
    {
        private AggregatedConfiguration _configurationCache = null;

        public AggregatedConfiguration ParseConfiguration(string jsonConfiguration, string[] roles = null, string userName = null)
        {
            var json = new JsonSerializer();
            json.MissingMemberHandling = MissingMemberHandling.Ignore;
            var config = json.Deserialize<ConfigurationContainer>(new JsonTextReader(new StringReader(jsonConfiguration)));
            var result = new AggregatedConfiguration();
            result.Entries = new List<ConfigurationEntry>();
            foreach (var entry in config.Global)
            {
                result.Entries.Add(entry);
            }
            if (roles != null && config.Roles != null)
            {
                foreach (var role in roles)
                {
                    if (config.Roles.Any(r => r.Role.ToLower() == role.ToLower()))
                    {
                        foreach (var roleConfig in config.Roles.FirstOrDefault(r=>r.Role.ToLower() == role.ToLower()).Entries)
                        {
                            var existingEntry =
                                result.Entries.FirstOrDefault(e => e.Key.ToLower() == roleConfig.Key.ToLower());
                            if (existingEntry!=null)
                            {
                                result.Entries.Remove(existingEntry);
                            }
                            result.Entries.Add(roleConfig);
                        }
                    }
                }
            }
            if (userName != null && config.Users != null)
            {
                if (config.Users.Any(r => r.User.ToLower() == userName.ToLower()))
                {
                    foreach (
                        var userConfig in
                            config.Users.FirstOrDefault(r => r.User.ToLower() == userName.ToLower()).Entries)
                    {
                        var existingEntry =
                            result.Entries.FirstOrDefault(e => e.Key.ToLower() == userConfig.Key.ToLower());
                        if (existingEntry != null)
                        {
                            result.Entries.Remove(existingEntry);
                        }
                        result.Entries.Add(userConfig);
                    }
                }
            }
            return result;
        }

        public string SerializeConfiguration(ConfigurationContainer container)
        {
            return JsonConvert.SerializeObject(container, Formatting.Indented);
        }

        public bool IsValid(string jsonConfiguration)
        {
            try
            {
                var obj = ParseConfiguration(jsonConfiguration);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void LoadConfiguration(string jsonConfiguration, string[] roles = null, string userName = null)
        {
            _configurationCache = ParseConfiguration(jsonConfiguration, roles, userName);
        }

        public string GetString(string key)
        {
            return _configurationCache.Entries.FirstOrDefault(e=>e.Key == key).Value;
        }

        public bool TryGetString(string key, out string value)
        {
            if (_configurationCache == null)
            {
                value = null;
                return false;
            }
            var entry = _configurationCache.Entries.FirstOrDefault(e => e.Key == key);
            if (entry != null)
            {
                value  = entry.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public int GetInt32(string key)
        {
            return Int32.Parse(GetString(key));
        }

        public Int64 GetInt64(string key)
        {
            return Int64.Parse(GetString(key));
        }

        public bool GetBool(string key)
        {
            return bool.Parse(GetString(key));
        }

        public double GetDouble(string key)
        {
            return double.Parse(GetString(key));
        }

        public float GetFloat(string key)
        {
            return float.Parse(GetString(key));
        }

        public bool TryGetInt32(string key, out Int32 value)
        {
            string valueString;
            if (TryGetString(key, out valueString))
            {
                value = Int32.Parse(valueString);
                return true;
            }
            value = Int32.MinValue;
            return false;
        }

        public bool TryGetInt64(string key, out Int64 value)
        {
            string valueString;
            if (TryGetString(key, out valueString))
            {
                value = Int64.Parse(valueString);
                return true;
            }
            value = Int64.MinValue;
            return false;
        }

        public bool TryGetBool(string key, out bool value)
        {
            string valueString;
            if (TryGetString(key, out valueString))
            {
                value = bool.Parse(GetString(key));
                return true;
            }
            value = false;
            return false;
        }

        public bool TryGetDouble(string key, out double value)
        {
            string valueString;
            if (TryGetString(key, out valueString))
            {
                value = double.Parse(GetString(key));
                return true;
            }
            value = double.NaN;
            return false;
        }

        public bool TryGetFloat(string key, out float value)
        {
            string valueString;
            if (TryGetString(key, out valueString))
            {
                value = float.Parse(GetString(key));
                return true;
            }
            value = float.NaN;
            return false;
        }

        public Int32 GetInt32OrDefault(string key, Int32 defaultValue)
        {
            Int32 result;
            return TryGetInt32(key, out result) ? result : defaultValue;
        }

        public Int64 GetInt64OrDefault(string key, Int64 defaultValue)
        {
            Int64 result;
            return TryGetInt64(key, out result) ? result : defaultValue;
        }

        public bool GetBoolOrDefault(string key, bool defaultValue)
        {
            bool result;
            return TryGetBool(key, out result) ? result : defaultValue;
        }

        public double GetDoubleOrDefault(string key, double defaultValue)
        {
            double result;
            return TryGetDouble(key, out result) ? result : defaultValue;
        }

        public float GetFloatOrDefault(string key, float defaultValue)
        {
            float result;
            return TryGetFloat(key, out result) ? result : defaultValue;
        }

        public string GetStringOrDefault(string key, string defaultValue)
        {
            string result;
            return TryGetString(key, out result) ? result : defaultValue;
        }
    }
}

