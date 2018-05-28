﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevCube
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "UpdateProgrammer",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Programmer", action = "IndexProgrammer", id = UrlParameter.Optional }
            );

        }
    }
}
