using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
using System.Security.Cryptography;

namespace Log4Grid.Sqlite
{
    public class LogStore4Sqlite : Log4Grid.Interfaces.ILogStoreHandler
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
                DBContext.SetConnectionDriver<SqliteDriver>(DB.DB2);
                DBContext.SetConnectionString(DB.DB2, value);
            }
        }

        private static object mLockCreateTable = new object();

        private void CreateTabel(string name)
        {
            lock (mLockCreateTable)
            {
                SQL sql = "SELECT count(*) FROM sqlite_master WHERE name =@p1 and type='table'";
                sql = sql["p1", name];
                if (sql.GetValue<long>(DB.DB2) == 0)
                {
                    sql = string.Format(@"CREATE TABLE [{0}] (
  [ID] VARCHAR(50), 
  [App] VARCHAR(50), 
  [Host] VARCHAR(50), 
  [Type] INT, 
  [Content] TEXT, 
  [CreateTime] DATETIME);

CREATE INDEX [{0}_index_createtime] ON [{0}] ([CreateTime] DESC);", name);
                    sql.Execute(DB.DB2);
                }
            }
        }

        public static string MD5Encoding(string rawPass)
        {

            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        public void Add(Models.LogModel e)
        {
            string name = "tbl_" + MD5Encoding(e.App);
            CreateTabel(name);
            using (DBContext.ChangeTable<DBLog>(name))
            {
                DBLog log = new DBLog();
                log.App = e.App;
                log.Content = e.Content;
                log.CreateTime = e.CreateTime;
                log.Host = e.Host;
                log.Type = e.Type;
                log.Save(DB.DB2);
            }
        }


        public void Clean(string app, string host)
        {
            string name = "tbl_" + LogStore4Sqlite.MD5Encoding(app);
            SQL sql = "SELECT count(*) FROM sqlite_master WHERE name =@p1 and type='table'";
            sql = sql["p1", name];
            if (sql.GetValue<long>(DB.DB2) == 0)
            {
                return;
            }
            sql = "delete from " + name + " where App=@p1";
            sql = sql["p1", app];
            if (!string.IsNullOrEmpty(host))
            {
                sql += " and Host=@p2";
                sql = sql["p2", host];
            }
            sql.Execute(DB.DB2);

        }

        public void Backup(string app, string host)
        {

        }
    }
}
