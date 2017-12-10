using Autofac;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.App_Start;
using WebAPI.Data.Sql;

namespace WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SqlSetup.Setup();
            Config.Configure();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}