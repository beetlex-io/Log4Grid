using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log4Grid.Interfaces
{
    public interface IUserManagement
    {
        Models.User Login(string name, string password);
        Models.User Create(string name, string paasword, string email, bool enabled);
        void Enabled(string name, bool enabled);
        void ChangePassword(string name, string newpassword);
        Models.User Exists(string name);
        IList<Models.User> List();
        void Delete(string name);
    }
}
