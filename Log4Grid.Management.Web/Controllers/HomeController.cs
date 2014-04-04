using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Log4Grid.Models;
using Log4Grid.Management.Web.Controllers.Filters;

namespace Log4Grid.Management.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
    

        private Config.InterfaceFactory mFactory = new Config.InterfaceFactory();
        [Filters.Login]
        public ActionResult Index()
        {
            Models.IndexView iv = new Models.IndexView();
            iv.Apps = mFactory.Management.ListApp();
            return View(iv);
        }
        [Filters.Login]
        public ActionResult GetAppStatus()
        {
            ContentResult cr = new ContentResult();
            IList<ApplicationData> result = mFactory.Management.ListApp();
            cr.Content = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return cr;
        }
        [Filters.Login]
        public ActionResult LogList(string app,string host, Log4Grid.Models.LogType? type, DateTime? from, DateTime? to,int? pageIndex)
        {
            Models.HomeLogList hl = new Models.HomeLogList();
            int pages=0;
            if (!string.IsNullOrEmpty(app))
            {
                hl.Logs = mFactory.Search.List(app,host, type, from, to, pageIndex == null ? 0 : pageIndex.Value, out pages);
            }
            else
            {
                hl.Logs = new List<Log4Grid.Models.LogModel>();
            }
            hl.PageIndex = pageIndex!=null?pageIndex.Value:0;
            hl.Pages = pages;
            return View(hl);
        }
        [AjaxSuccess]
        public ActionResult CleanApp()
        {
            mFactory.Management.CleanApp();
            return View();
        }
        public ActionResult SingOut()
        {
            Codes.Utils.Signout();
            return new RedirectResult("/Singin");
        }
        public ActionResult Login(bool? post,string name, string pwd)
        {
            string result = "";
            if (post != null && post.Value)
            {
                Log4Grid.Models.User user = mFactory.User.Login(name, pwd);
                if (user != null)
                {
                    Codes.Utils.SetLogin(user.Name);
                    return new RedirectResult("/");
                }
                result = "user name or password not available";
            }
            return View((object)result);
        }
        [AjaxSuccess]
        public ActionResult DeleteUser(string name)
        {
            if (!HttpContext.Request.IsLocal)
            {
                throw new Exception("must internal access!");
            }
            mFactory.User.Delete(name);
            return View();
        }
        public ActionResult Users()
        {
            Models.HomeUsers user = new Models.HomeUsers();
            if (HttpContext.Request.IsLocal)
            {
                user.Users = mFactory.User.List();
            }
            return View(user);
        }

        public ActionResult CreateUser(bool? post, string name, string pwd,string rpwd)
        {
            string result = "";
            if (!HttpContext.Request.IsLocal)
            {
                result = "must internal access!";
                return View((object)result);
            }
            if (post != null && post.Value)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
                {
                    result = "user name or password not available";
                }
                else if (pwd != rpwd)
                {
                    result = "password do not match";
                }
                else
                {
                    Log4Grid.Models.User user = mFactory.User.Create(name, pwd, "", true);
                    if (user == null)
                    {
                        result = "user already exists";
                    }
                    else
                    {
                        result = "create user success";
                    }
                }
            }
            return View((object)result);
        }
        
        [AjaxSuccess]
        [Filters.Login]
        public ActionResult Clean(string app, string host)
        {
            mFactory.Store.Clean(app, host);
            return View();
        }
       
        [AjaxSuccess]
        [Filters.Login]
        public ActionResult Backup(string app, string host)
        {
            mFactory.Store.Backup(app, host);
            return View();
        }
    }
}
