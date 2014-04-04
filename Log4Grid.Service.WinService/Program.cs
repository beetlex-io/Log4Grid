using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Configuration.Install;

namespace Log4Grid.Service.WinService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new Log4GridService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
    [RunInstaller(true)]
    public class WindowsServiceInstaller : Installer
    {

        public WindowsServiceInstaller()
        {
        
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();


            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;


            serviceInstaller.DisplayName = "Log4Grid Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;


            serviceInstaller.ServiceName = "Log4Grid Service";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
