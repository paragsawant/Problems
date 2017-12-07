using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ClassLibrary;

namespace WebAPI.App_Start
{
    public class Config
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterType<IDataLayerAccess>().AsSelf().InstancePerRequest();
            builder.Register(d => (new DataLayerAccess())).As<IDataLayerAccess>().SingleInstance();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}