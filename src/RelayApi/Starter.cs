using RelayApi.App_Start;
using RelayApi.Interfaces;
using System;

namespace RelayApi
{
    public class Starter : IDisposable
    {
        private IManager manager;

        public Starter()
        {
            var dependencyResolver = new DependencyResolver();
            manager = (IManager)dependencyResolver.GetService(typeof(IManager));
        }

        public void Dispose()
        {
            manager = null;
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            manager.Start();
        }

        public void Stop()
        {
            try
            {
                manager?.Stop();
            }
            finally
            {
                Dispose();
            }
        }
    }
}
