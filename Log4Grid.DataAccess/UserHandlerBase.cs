using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Peanut;
using Log4Grid.DBModels;

namespace Log4Grid.DataAccess
{
    public abstract class UserHandlerBase<T> : Interfaces.IUserManagement where T : Peanut.IDriver, new()
    {
        public virtual DB DB
        {
            get
            {
                return Peanut.DB.DB102;
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

        public Models.User Login(string name, string password)
        {
            Models.User user = (DBUser.name == name).ListFirst<DBUser, Models.User>(DB);
            if (user != null && user.Password == password && user.Enabled)
                return user;
            return null;
        }

        public Models.User Create(string name, string paasword, string email, bool enabled)
        {
            if ((DBUser.name == name).Count<DBUser>(DB) > 0)
                return null;
            DBUser user = new DBUser();
            user.Name = name;
            user.Password = paasword;
            user.Email = email;
            user.Enabled = enabled;
            user.Save(DB);
            return user.MemberCopyTo<Models.User>();
        }

        public void Enabled(string name, bool enabled)
        {
            (DBUser.name == name).Edit<DBUser>(DB, d => { d.Enabled = enabled; });
        }

        public void ChangePassword(string name, string newpassword)
        {
            (DBUser.name == name).Edit<DBUser>(DB, d => { d.Password = newpassword; });

        }

        public Models.User Exists(string name)
        {
            return (DBUser.name == name).ListFirst<DBUser, Models.User>(DB);
        }


        public IList<Models.User> List()
        {
            return new Expression().List<DBUser, Models.User>(DB);
        }

        public void Delete(string name)
        {
            (DBUser.name == name).Delete<DBUser>(DB);
        }
    }
}

