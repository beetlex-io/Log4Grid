using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Grid.Management.Web.Models
{
    public class IndexView
    {
        public IList<Log4Grid.Models.ApplicationData> Apps
        {
            get;
            set;
        }
    }
}