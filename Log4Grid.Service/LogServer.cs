using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Log4Grid.Config;

namespace Log4Grid.Service
{
    public class LogServer:IDisposable
    {
        private LogServerSection GetConfigSection(string sectionName)
        {
            LogServerSection result = null;

            System.Configuration.ExeConfigurationFileMap fm = new System.Configuration.ExeConfigurationFileMap();
            fm.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "Log4Grid.config";
            System.Configuration.Configuration mDomainConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fm, System.Configuration.ConfigurationUserLevel.None);
            result = (LogServerSection)mDomainConfig.GetSection(sectionName);
            return result;
        }
        public LogServer()
        {
            LogServerSection section = GetConfigSection(LogServerSection.LogServerSectionSectionName);
            mServer = Beetle.Express.ServerFactory.CreateUDP();
            mServer.Host = section.Host;
            mServer.Port = section.Port;
            mServer.SendBufferSize = 1024 * 64;
            mServer.ReceiveBufferSize = 1024 * 64;
            mServer.Handler = new MessageHandler(section.WorkThreads);
        }

        private Beetle.Express.IServer mServer;

        public void Open()
        {
            try
            {
                mServer.Open();
                Utils.Log.InfoFormat("log server open {0}@{1} success", mServer.Host, mServer.Port);
            }
            catch (Exception e_)
            {
                Utils.Log.ErrorFormat("log server open error {0}", e_.Message);
            }
        }

        public void Dispose()
        {
            if (mServer != null)
                mServer.Dispose();
        }
    }
}
