using System.Web.Http;
using Owin;
using RelayApi.Formatters;

namespace RelayApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration
            {
                DependencyResolver = new DependencyResolver()
            };

            SwaggerConfig.Register(config);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new PlainTextFormatter());

            app.UseWebApi(config);
        }
    }
}
