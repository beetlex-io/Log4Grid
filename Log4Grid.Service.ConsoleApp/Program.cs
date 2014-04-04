using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Service.ConsoleApp
{
    class Program
    {
        private static LogServer mServer;
        static void Main(string[] args)
        {
            mServer = new LogServer();
            mServer.Open();
            Console.Read();
        }
    }
}
