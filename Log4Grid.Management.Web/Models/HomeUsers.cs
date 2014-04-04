using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Grid.Management.Web.Models
{
    public class HomeUsers
    {
        public HomeUsers()
        {
            Users = new List<Log4Grid.Models.User>();
        }
        public IList<Log4Grid.Models.User> Users
        {
            get;
            set;
        }
    }
}