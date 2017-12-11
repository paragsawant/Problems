using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProblemD_UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{policyNumber}",
                defaults: new { controller = "Policy", action = "PoliciesList", id = UrlParameter.Optional, policyNumber = UrlParameter.Optional }
            );
        }
    }
}
