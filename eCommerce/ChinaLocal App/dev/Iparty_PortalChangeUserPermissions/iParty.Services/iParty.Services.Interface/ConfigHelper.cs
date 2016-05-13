// Ported and modifed from Quartet

using System.Configuration;

namespace QuartES.Services.Common
{
    public static class ConfigHelper
    {
        public static string GetEnvironment(string componentName = null, bool throwIfNotFound = true)
        {
            var environment = string.IsNullOrEmpty(componentName) ?
                ConfigurationManager.AppSettings["Environment"] :
                ConfigurationManager.AppSettings[string.Format("{0}.Environment", componentName)] ?? ConfigurationManager.AppSettings["Environment"];

            if (string.IsNullOrEmpty(environment) && throwIfNotFound)
                throw new ConfigurationErrorsException(string.Format("Missing 'Environment' appSetting"));

            return environment;
        }

        public static string GetRegion(string componentName = null, bool throwIfNotFound = true)
        {
            var environment = string.IsNullOrEmpty(componentName) ?
                ConfigurationManager.AppSettings["Region"] :
                ConfigurationManager.AppSettings[string.Format("{0}.Region", componentName)] ?? ConfigurationManager.AppSettings["Region"];

            if (string.IsNullOrEmpty(environment) && throwIfNotFound)
                throw new ConfigurationErrorsException(string.Format("Missing 'Region' appSetting"));

            return environment;
        }

        public static string GetLocalizedAppSetting(string key, string componentName = null, bool throwIfNotFound = true)
        {
            var environment = GetEnvironment(componentName);
            var region = GetRegion(componentName);

            var value = ConfigurationManager.AppSettings[string.Format("{0}.{1}.{2}", key, region, environment)];

            if (value == null)
                value = ConfigurationManager.AppSettings[string.Format("{0}.{1}", key, region)];

            if (value == null)
                value = ConfigurationManager.AppSettings[string.Format("{0}", key)];

            if (value == null && throwIfNotFound)
                throw new ConfigurationErrorsException(string.Format("Missing appsetting for '{0}'.\r\nKeys tried:\r\n{0}.{1}.{2}\r\n{0}.{1}\r\n{0}", key, region, environment));

            return value;
        }

        public static ConnectionStringSettings GetLocalizedConnectionString(string key, string componentName = null, bool throwIfNotFound = true)
        {
            var environment = GetEnvironment(componentName);
            var region = GetRegion(componentName);

            var value = ConfigurationManager.ConnectionStrings[string.Format("{0}.{1}.{2}", key, region, environment)];

            if (value == null)
                value = ConfigurationManager.ConnectionStrings[string.Format("{0}.{1}", key, region)];

            if (value == null)
                value = ConfigurationManager.ConnectionStrings[string.Format("{0}", key)];

            if (value == null && throwIfNotFound)
                throw new ConfigurationErrorsException(string.Format("Missing connection string for '{2}'.\r\nKeys tried:\r\n{0}.{1}.{2}\r\n{0}.{1}\r\n{0}", key, region, environment));

            return value;
        }
    }
}
