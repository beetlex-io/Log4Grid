using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Grid.Management.Web.Models
{
    public class HomeLogList
    {
        public IList<Log4Grid.Models.LogModel> Logs
        {
            get;
            set;
        }
        public int PageIndex
        {
            get;
            set;
        }
        public int Pages
        {
            get;
            set;
        }

    }
}