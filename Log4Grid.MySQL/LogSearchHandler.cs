using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
namespace Log4Grid.MySQL
{
    public class LogSearch4MySQL : Log4Grid.DataAccess.LogSearchHandlerBase<MySqlDriver>
    {
        protected override bool Exists(string table)
        {
            SQL sql = @"SHOW TABLES LIKE @p1";
            sql = sql["p1", table];
            return sql.GetValue<string>(DB)!=null;
        }
    }
}
