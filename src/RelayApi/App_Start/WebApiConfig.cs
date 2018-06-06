using RelayApi.Formatters;
using System.Web.Http;

namespace RelayApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            SwaggerConfig.Register(config);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Add(new PlainTextFormatter());
        }
    }
}
