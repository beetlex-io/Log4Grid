using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Log4Grid.Models;
namespace Log4Grid.Management.Web.Codes
{
    public class Utils
    {
        private static Config.InterfaceFactory mFactory = new Config.InterfaceFactory();
        public static User User
        {
            get
            {
                System.Collections.IDictionary dict = HttpContext.Current.Items;
                if (dict["_ISLOAD_USER"] == null)
                {
                    HttpCookie loginer = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (loginer != null && loginer.Value != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(loginer.Value);
                        string userid = ticket.Name;
                        User user = mFactory.User.Exists(userid);
                        dict["_LOGINER"] = user;
                    }
                    dict["_ISLOAD_USER"] = true;
                }
                return (User)dict["_LOGINER"];
            }

        }
        public static void SetLogin(string name)
        {
            FormsAuthentication.SetAuthCookie(name, true);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(name, false, 120);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void Signout()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie myCookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        private static Dictionary<Log4Grid.Models.LogType, string> mClass;

        private static object mLockClass = new object();

        public static Dictionary<Log4Grid.Models.LogType, string> Class
        {
            get
            {
                lock (mLockClass)
                {
                    if (mClass == null)
                    {
                        mClass = new Dictionary<LogType, string>();
                        mClass.Add(LogType.Error, "danger");
                        mClass.Add(LogType.Warn, "warning");
                        mClass.Add(LogType.Debug, "info");
                    }
                    return mClass;
                }
            }
        }

        public static string GetRowClass(Log4Grid.Models.LogType type)
        {
            string value = null;
            if (!Class.TryGetValue(type, out value))
            {
                value = "active";
            }
            return value;

        }
    }
}