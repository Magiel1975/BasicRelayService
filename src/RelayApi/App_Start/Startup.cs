using System;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RelayApi.App_Start.Startup), "StartProcess")]

namespace RelayApi.App_Start
{
    public class Startup : IDisposable
    {
        private IDisposable webApi;
        private static Starter starter;

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration
            {
                DependencyResolver = new DependencyResolver(),
            };

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        public void Dispose()
        {
            if (webApi != null)
            {
                webApi.Dispose();
                webApi = null;
            }
            if (starter != null)
            {
                starter.Dispose();
                starter = null;
            }
            GC.SuppressFinalize(this);
        }

        public void LaunchWebApi()
        {
            string webApiUrl = ConfigurationManager.AppSettings.Get("WebApiUrl");
            webApi = WebApp.Start<Startup>(webApiUrl);
        }

        public static void StartProcess()
        {
            starter = new Starter();
            starter.Start();
        }
    }
}
