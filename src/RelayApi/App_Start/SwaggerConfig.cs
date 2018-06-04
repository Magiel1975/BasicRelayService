using System.Web.Http;
using Swashbuckle.Application;

namespace RelayApi
{
    /// <summary>
    /// Registers swagger
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Registers swagger for this Web Api.
        /// </summary>
        /// <param name="config">The http configuration</param>
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Relay Api");
            })
            .EnableSwaggerUi(c => { });
        }
    }
}
