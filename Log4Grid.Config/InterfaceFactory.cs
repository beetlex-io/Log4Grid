using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Log4Grid.Config
{
    public class InterfaceFactory
    {
        private Interfaces.IAppManagement mManagement;

        private Interfaces.ILogStoreHandler mStore;

        private Interfaces.ILogSearchHandler mSearch;

        private Interfaces.IUserManagement mUser;

        private Log4GridSection GetConfigSection(string sectionName)
        {
            Log4GridSection result = null;

            System.Configuration.ExeConfigurationFileMap fm = new System.Configuration.ExeConfigurationFileMap();
            fm.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Log4Grid.config";
            System.Configuration.Configuration mDomainConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fm, System.Configuration.ConfigurationUserLevel.None);
            result = (Log4GridSection)mDomainConfig.GetSection(sectionName);
            return result;
        }

        public InterfaceFactory()
        {
            LoadConfig(GetConfigSection(Log4GridSection.Log4GridSectionSectionName));

        }

        public InterfaceFactory(string section)
        {
            LoadConfig(GetConfigSection(section));
        }

        private void LoadConfig(Log4GridSection section)
        {
            mManagement = (Interfaces.IAppManagement)Activator.CreateInstance(Type.GetType(section.Management.Type));
            LoadProperties(mManagement, section.Management.Properties);

            mStore = (Interfaces.ILogStoreHandler)Activator.CreateInstance(Type.GetType(section.LogStore.Type));
            LoadProperties(mStore, section.LogStore.Properties);

            mSearch = (Interfaces.ILogSearchHandler)Activator.CreateInstance(Type.GetType(section.LogSearch.Type));
            LoadProperties(mSearch, section.LogSearch.Properties);

            mUser = (Interfaces.IUserManagement)Activator.CreateInstance(Type.GetType(section.User.Type));
            LoadProperties(mUser, section.User.Properties);
        }

        private void LoadProperties(object data, PropertyCollection properties)
        {
            foreach (Property item in properties)
            {
                PropertyInfo pi = data.GetType().GetProperty(item.Name, BindingFlags.Public | BindingFlags.Instance);
                if (pi != null && pi.CanWrite)
                {
                    try
                    {
                        pi.SetValue(data, Convert.ChangeType(item.Value, pi.PropertyType), null);
                    }
                    catch (Exception e_)
                    {

                    }
                }
            }

        }

        public Interfaces.IUserManagement User
        {
            get
            {
                return mUser;
            }
        }

        public Interfaces.ILogSearchHandler Search
        {
            get
            {
                return mSearch;
            }
        }

        public Interfaces.ILogStoreHandler Store
        {
            get
            {
                return mStore;
            }
        }
        public Interfaces.IAppManagement Management
        {
            get
            {
                return mManagement;
            }
        }
    }
}
