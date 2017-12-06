using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WebAPI
{
    public class WebAPIService : IService
    {
        public void Start(ServiceConfiguration serviceConfiguration)
        {
            var httpsEndpoint = serviceConfiguration.InputEndpoints["RPHttpsEndpoint"];

            // We need to register both https://localhost:port and https://+:port otherwise we will get 503 exception
            var uriBuilder = new UriBuilder(httpsEndpoint);
            uriBuilder.Host = "+";
            try
            {
                this.app = WebApp.Start<Startup>(new StartOptions { Urls = { uriBuilder.ToString(), httpsEndpoint.AbsoluteUri } });
            }
            catch (Exception ex)
            {
                LogEventSource.Instance.Critical(
                    DateTime.Now,
                    "ResourceProvider",
                    string.Empty,
                    "StartupFailure",
                    "Service",
                    string.Empty,
                    string.Empty,
                    $"RP Service failed to start {ex.ToExceptionString()}");
                throw;
            }
        }

        /// <summary>The stop.</summary>
        public void Stop()
        {
            this.app?.Dispose();
        }
    }
}