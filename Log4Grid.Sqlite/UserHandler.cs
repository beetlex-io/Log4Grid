using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;

namespace Log4Grid.Sqlite
{
    public class UserHandler:Interfaces.IUserManagement
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
                DBContext.SetConnectionDriver<SqliteDriver>(DB.DB4);
                DBContext.SetConnectionString(DB.DB4, value);
            }
        }

        public Models.User Login(string name, string password)
        {
            Models.User user = (DBUser.name == name).ListFirst<DBUser, Models.User>(DB.DB4);
            if (user != null && user.Password == password && user.Enabled)
                return user;
            return null;
        }

        public Models.User Create(string name, string paasword, string email, bool enabled)
        {
            if ((DBUser.name == name).Count<DBUser>(DB.DB4) > 0)
                return null;
            DBUser user = new DBUser();
            user.Name = name;
            user.Password = paasword;
            user.Email = email;
            user.Enabled = enabled;
            user.Save(DB.DB4);
            return user.MemberCopyTo<Models.User>();
        }

        public void Enabled(string name, bool enabled)
        {
            (DBUser.name == name).Edit<DBUser>(DB.DB4,d => { d.Enabled = enabled; });
        }

        public void ChangePassword(string name, string newpassword)
        {
            (DBUser.name == name).Edit<DBUser>(DB.DB4,d => { d.Password = newpassword; });
            
        }

        public Models.User Exists(string name)
        {
            return (DBUser.name == name).ListFirst<DBUser, Models.User>(DB.DB4);
        }


        public IList<Models.User> List()
        {
            return new Expression().List<DBUser, Models.User>(DB.DB4);
        }

        public void Delete(string name)
        {
            (DBUser.name == name).Delete<DBUser>(DB.DB4);
        }
    }
}
