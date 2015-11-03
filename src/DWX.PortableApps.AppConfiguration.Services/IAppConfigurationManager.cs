using System;
using DWX.PortableApps.AppConfiguration.Dto;

namespace DWX.PortableApps.AppConfiguration.Services
{
    public interface IAppConfigurationManager
    {
        AggregatedConfiguration ParseConfiguration(string jsonConfiguration, string[] roles = null, string userName = null);
        string SerializeConfiguration(ConfigurationContainer container);
        void LoadConfiguration(string jsonConfiguration, string[] roles = null, string userName = null);
        bool IsValid(string jsonConfiguration);
        string GetString(string key);
        int GetInt32(string key);
        Int64 GetInt64(string key);
        bool GetBool(string key);
        double GetDouble(string key);
        float GetFloat(string key);
        bool TryGetString(string key, out string value);
        bool TryGetInt32(string key, out Int32 value);
        bool TryGetInt64(string key, out Int64 value);
        bool TryGetBool(string key, out bool value);
        bool TryGetDouble(string key, out double value);
        bool TryGetFloat(string key, out float value);
        Int32 GetInt32OrDefault(string key, Int32 defaultValue);
        Int64 GetInt64OrDefault(string key, Int64 defaultValue);
        bool GetBoolOrDefault(string key, bool defaultValue);
        double GetDoubleOrDefault(string key, double defaultValue);
        float GetFloatOrDefault(string key, float defaultValue);
        string GetStringOrDefault(string key, string defaultValue);
    }
}