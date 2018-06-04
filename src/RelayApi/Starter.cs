using Microsoft.Owin.Hosting;
using RelayApi.App_Start;
using RelayApi.Interfaces;
using System;
using System.Configuration;

namespace RelayApi
{
    public class Starter : IDisposable
    {
        private IManager manager;
        private IDisposable webApi;

        public Starter()
        {
        }

        public Starter(IManager manager, IDisposable webApi)
        {
            this.manager = manager;
            this.webApi = webApi;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && webApi != null)
            {
                webApi.Dispose();
                webApi = null;
            }
        }

        public void Start()
        {
            string webApiUrl = ConfigurationManager.AppSettings.Get("WebApiUrl");
            webApi = WebApp.Start<Startup>(webApiUrl);
            var dependencyResolver = new DependencyResolver();
            manager = (IManager)dependencyResolver.GetService(typeof(IManager));
            manager.Start();
        }

        public void Stop()
        {
            try
            {
                manager.Stop();
            }
            finally
            {
                Dispose();
            }
        }
    }
}
