using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
namespace Log4Grid.Sqlite
{
    public class Management4Sqlite : Interfaces.IAppManagement
    {
        private string mDBFile;
        public string DBFile
        {
            get
            {
                return mDBFile;
            }
            set
            {
                mDBFile = value;
                DBContext.LoadEntityByAssembly(typeof(Management4Sqlite).Assembly);
                DBContext.SetConnectionDriver<SqliteDriver>(DB.DB1);
                DBContext.SetConnectionString(DB.DB1, value);
            }
        }
        public void Stat(Log4Grid.Models.StatModel e)
        {
            if ((DBApplication.name == e.App).Count<DBApplication>() == 0)
            {
                DBApplication app = new DBApplication();
                app.Name = e.App;
                app.Save();
                
            }
            DBHost host = (DBHost.appID == e.App & DBHost.name == e.Host).ListFirst<DBHost>();
            if (host == null)
            {
                host = new DBHost();
                host.Name = e.Host;
                host.AppID = e.App;

            }
            host.CpuUsage = e.CpuUsage;
            host.MemoryUsage = e.MemoryUsage;
            host.LastActiveTime = DateTime.Now;
            host.Save();
        }


        public IList<Models.ApplicationData> ListApp()
        {
            IList<Models.ApplicationData> result = new Expression().List<DBApplication,Models.ApplicationData>();
            foreach (Models.ApplicationData item in result)
            {
                item.Hosts = (DBHost.appID == item.Name).List<DBHost, Models.ApplicationHost>();
                foreach (Models.ApplicationHost host in item.Hosts)
                {
                    host.Enabled = (DateTime.Now - host.LastActiveTime).TotalSeconds < 5;
                }
            }
            return result;
        }


        public void CleanApp()
        {
            Expression exp = new Expression();
            exp.Delete<DBApplication>();
        }
    }
}
