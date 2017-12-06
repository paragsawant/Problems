using Autofac;
using ClassLibrary;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI
{
    public class Startup
    {

        public static void Configuration(IAppBuilder app)
        {
            var container = AutofacConfig.Initialize(Registration);
            app.UseAutofacMiddleware(container);
        }

        public static void Registration(ContainerBuilder builder)
        {
            builder.RegisterType<DataLayerAccess>().As<IDataLayerAccess>().SingleInstance();
        }


    }
}