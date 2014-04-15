using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
using System.Security.Cryptography;

namespace Log4Grid.MySQL
{
    public class LogStore4MySQL : Log4Grid.DataAccess.LogStoreHandlerBase<MySqlDriver>
    {

        private Dictionary<string, string> mTables = new Dictionary<string, string>();

        protected override bool Exists(string table)
        {
            if (mTables.ContainsKey(table))
                return true;
            SQL sql = @"SHOW TABLES LIKE @p1";
            sql = sql["p1", table];
            return !string.IsNullOrEmpty(sql.GetValue<string>(DB));
        }

        protected override void OnCreateTable(string table)
        {

            SQL sql = string.Format(@"CREATE TABLE [{0}] (
  [ID] VARCHAR(50), 
  [App] VARCHAR(50), 
  [Host] VARCHAR(50), 
  [Type] INT, 
  [LogContent] TEXT, 
  [CreateTime] DATETIME);

CREATE INDEX [{0}_index_createtime] ON [{0}] ([CreateTime] DESC);", table);
            sql.Execute(DB);
            mTables.Add(table, table);
        }
    }
}
