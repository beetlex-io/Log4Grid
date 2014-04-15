using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Appender.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.ILog log = log4net.LogManager.GetLogger("test");
            while (true)
            {
                log.ErrorFormat("{0} test error!", "henryfan");
                System.Threading.Thread.Sleep(5000);
                log.InfoFormat("{0} test Info!", "henryfan");
                System.Threading.Thread.Sleep(5000);
                log.DebugFormat("{0} test Info!", "henryfan");
                System.Threading.Thread.Sleep(5000);
                log.FatalFormat("{0} test Info!", "henryfan");
                System.Threading.Thread.Sleep(5000);

            }
            Console.Read();
        }
    }
}
