using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Web.Common;
using RelayApi.Interfaces;

namespace RelayApi.App_Start
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;
        private static IErrorMessageHandler errorMessageHandler;

        public DependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyResolver();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        private void AddBindings()
        {
            // singleton and transient bindings go here
            kernel.Bind<IManager>().To<Manager>();
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            CreateStaticBindingsOnKernel(kernel);
        }

        private static void CreateStaticBindingsOnKernel(IKernel kernel)
        {
            errorMessageHandler = errorMessageHandler ?? kernel.Get<ErrorMessageHandler>();
            kernel.Bind<IErrorMessageHandler>().ToConstant(errorMessageHandler);
        }
    }
}
