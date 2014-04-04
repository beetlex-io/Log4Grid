using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Service
{
    public class Utils
    {
        static Utils()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public static log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger("LogServer");
            }
        }
    }
}
