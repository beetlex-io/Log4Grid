using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
using System.Security.Cryptography;

namespace Log4Grid.MSSQL
{
    public class LogStore4MSSQL : Log4Grid.DataAccess.LogStoreHandlerBase<Peanut.MSSQL>
    {
        protected override bool Exists(string table)
        {

            SQL sql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + table + "]') AND type in (N'U')";
            return sql.GetValue<long>(DB) > 0;
        }

        protected override void OnCreateTable(string table)
        {

            SQL sql = string.Format(@"CREATE TABLE [dbo].[{0}](
	[ID] [varchar](50) NOT NULL,
	[Host] [varchar](200) NULL,
	[App] [varchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[LogContent] [varchar](1000) NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY];
CREATE NONCLUSTERED INDEX [IX_{0}_CreateTime] ON [dbo].[TBL_Log] 
(
	[CreateTime] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
;", table);

            sql.Execute(DB);
          

        }
    }
}
