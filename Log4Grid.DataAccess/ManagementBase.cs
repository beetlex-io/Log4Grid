using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
using Log4Grid.DBModels;

namespace Log4Grid.DataAccess
{
    public abstract class ManagementBase<T> : Interfaces.IAppManagement where T:Peanut.IDriver,new()
    {
        public virtual DB DB
        {
            get
            {
                return Peanut.DB.DB101;
            }
        }

        private string mConnectionString;
        public  string ConnectionString
        {
            get
            {
                return mConnectionString;

            }
            set
            {
                mConnectionString = value;
                DBContext.LoadEntityByAssembly(typeof(Log4Grid.DBModels.DBHost).Assembly);
                DBContext.SetConnectionDriver<T>(DB);
                DBContext.SetConnectionString(DB, value);
            }
        }
        public virtual void Stat(Log4Grid.Models.StatModel e)
        {
            if ((DBApplication.name == e.App).Count<DBApplication>(DB) == 0)
            {
                DBApplication app = new DBApplication();
                app.Name = e.App;
                app.Save(DB);

            }
            DBHost host = (DBHost.appID == e.App & DBHost.name == e.Host).ListFirst<DBHost>(DB);
            if (host == null)
            {
                host = new DBHost();
                host.Name = e.Host;
                host.AppID = e.App;

            }
            host.CpuUsage = e.CpuUsage;
            host.MemoryUsage = e.MemoryUsage;
            host.LastActiveTime = DateTime.Now;
            host.Save(DB);
        }
        public virtual IList<Models.ApplicationData> ListApp()
        {
            IList<Models.ApplicationData> result = new Expression().List<DBApplication, Models.ApplicationData>(DB);
            foreach (Models.ApplicationData item in result)
            {
                item.Hosts = (DBHost.appID == item.Name).List<DBHost, Models.ApplicationHost>(DB);
                foreach (Models.ApplicationHost host in item.Hosts)
                {
                    host.Enabled = (DateTime.Now - host.LastActiveTime).TotalSeconds < 5;
                }
            }
            return result;
        }

        public virtual void CleanApp()
        {
            Expression exp = new Expression();
            exp.Delete<DBApplication>(DB);
        }
    }
}
