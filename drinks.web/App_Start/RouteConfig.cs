﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace drinks.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Admin/Drinks", "Admin/{secret}/Drinks", new { controller = "Admin", action = "Drinks", secret = UrlParameter.Optional });
            routes.MapRoute("Admin/Coins", "Admin/{secret}/Coins", new { controller = "Admin", action = "Coins", secret = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
