using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
namespace Log4Grid.Sqlite
{
    public class LogSearchHandler:Interfaces.ILogSearchHandler
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
                DBContext.SetConnectionDriver<SqliteDriver>(DB.DB3);
                DBContext.SetConnectionString(DB.DB3, value);
            }
        }
        public IList<Models.LogModel> List(string app,string host, Models.LogType? type, DateTime? from, DateTime? to, int pageindex, out int pages)
        {
            string name = "tbl_" + LogStore4Sqlite.MD5Encoding(app);

            SQL sql = "SELECT count(*) FROM sqlite_master WHERE name =@p1 and type='table'";
            sql = sql["p1", name];
            if (sql.GetValue<long>(DB.DB3) == 0)
            {
                pages = 0;
                return new List<Log4Grid.Models.LogModel>();
            }
            using (DBContext.ChangeTable<DBLog>(name))
            {
                Expression exp = new Expression();
                if (type != null)
                    exp &= DBLog.type == type.Value;
                if (from != null)
                    exp &= DBLog.createTime >= from.Value;
                if (to != null)
                    exp &= DBLog.createTime < to.Value;
                if (!string.IsNullOrEmpty(host))
                    exp &= DBLog.host == host;
                int count = exp.Count<DBLog>(DB.DB3);
                pages = count / 50;
                if ((pages % 50) > 0)
                    pages++;
                return exp.List<DBLog, Models.LogModel>(DB.DB3, new Region(pageindex, 50), DBLog.createTime.Desc);
                
            }

        }
    }
}
