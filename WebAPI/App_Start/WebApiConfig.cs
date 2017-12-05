using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Data.Sql;

namespace WebAPI
{
    public static class WebApiConfig
    {
        static WebApiConfig()
        {
            SqlSetup setup = new SqlSetup();
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute("Policy", "{Controller}/policy/");
        }
    }
}
