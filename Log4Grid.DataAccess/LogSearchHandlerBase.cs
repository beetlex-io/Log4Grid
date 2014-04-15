using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
using Log4Grid.DBModels;
using System.Security.Cryptography;

namespace Log4Grid.DataAccess
{
    public abstract class LogSearchHandlerBase<T> : Interfaces.ILogSearchHandler where T : IDriver, new()
    {
        public virtual DB DB
        {
            get
            {
                return Peanut.DB.DB103;
            }
        }
        private string mConnectionString;
        public string ConnectionString
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

        public string MD5Encoding(string rawPass)
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

        protected abstract bool Exists(string table);

        public IList<Models.LogModel> List(string app, string host, Models.LogType? type, DateTime? from, DateTime? to, int pageindex, out int pages)
        {
            string name = "tbl_" + MD5Encoding(app);

            if (!Exists(name))
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
                int count = exp.Count<DBLog>(DB);
                pages = count / 50;
                if ((pages % 50) > 0)
                    pages++;
                return exp.List<DBLog, Models.LogModel>(DB, new Region(pageindex, 50), DBLog.createTime.Desc);

            }

        }
    }
}
