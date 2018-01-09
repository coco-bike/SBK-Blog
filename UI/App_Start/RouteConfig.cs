using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "WebLogin", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "UI.Areas.Web.Controllers" }
            ).DataTokens.Add("area", "Web");

            routes.MapRoute(
                name: "Admin",
                url: " Admin/{controller}/{action}/{id}",
                defaults: new { controller = "AdminLogin", action = "AdminLogin", id = UrlParameter.Optional },
                namespaces: new string[] { "UI.Areas.Admin.Controllers" }
            ).DataTokens.Add("area", "Admin");
        }
    }
}