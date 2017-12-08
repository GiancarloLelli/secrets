using System.Collections.Specialized;
using System.Configuration;

namespace GL.Console.Secrets
{
    public class ExtendedConfigurationManager
    {
        private readonly NameValueCollection m_secrets;

        public ExtendedConfigurationManager()
        {
            m_secrets = new NameValueCollection();
        }

        public NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        public NameValueCollection Secrets
        {
            get
            {
                return m_secrets;
            }
        }

        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings;
            }
        }

        public object GetSection(string sectionName) => ConfigurationManager.GetSection(sectionName);

        public Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel) => ConfigurationManager.OpenExeConfiguration(userLevel);

        public Configuration OpenExeConfiguration(string exePath) => ConfigurationManager.OpenExeConfiguration(exePath);

        public Configuration OpenMachineConfiguration() => ConfigurationManager.OpenMachineConfiguration();

        public Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel) => ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel);

        public Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad) => ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel, preLoad);

        public Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap) => ConfigurationManager.OpenMappedMachineConfiguration(fileMap);

        public void RefreshSection(string sectionName) => ConfigurationManager.RefreshSection(sectionName);
    }
}
