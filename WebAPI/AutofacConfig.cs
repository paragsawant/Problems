using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public static class AutofacConfig
    {
        private static IContainer Container { get; set; }

        public static IContainer Initialize(Action<ContainerBuilder> registration)
        {
            var builder = new ContainerBuilder();
            registration(builder);
            Container = builder.Build();
            return Container;
        }
    }
}