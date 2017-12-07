using Autofac;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.App_Start;

namespace WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Config.Configure();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}