using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Log4Grid.Management.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
           name: "Users",
           url: "Users",
           defaults: new { controller = "Home", action = "Users", id = UrlParameter.Optional }
       );

            routes.MapRoute(
             name: "CreateUser",
             url: "CreateUser",
             defaults: new { controller = "Home", action = "CreateUser", id = UrlParameter.Optional }
         );
            routes.MapRoute(
           name: "SingOut",
           url: "SingOut",
           defaults: new { controller = "Home", action = "SingOut", id = UrlParameter.Optional }
       );
            routes.MapRoute(
              name: "Singin",
              url: "Singin",
              defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}