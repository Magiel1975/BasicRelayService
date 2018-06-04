using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using RelayApi.Contracts;
using RelayApi.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace RelayApi.Controllers
{
    [RoutePrefix("api/mock")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }


        public ValuesController(IErrorMessageHandler errorMessageHandler)
        {
            this.errorMessageHandler = errorMessageHandler;
        }

        private readonly IErrorMessageHandler errorMessageHandler;

        /// <summary>
        /// Retrieves the last 200 caught errors thrown by exceptions in the main process.
        /// </summary>
        /// <returns>The list of errors</returns>
        [Route("GetErrorList"), HttpGet, ResponseType(typeof(GetReceivedMessagesResponse))]
        public IHttpActionResult GetErrors()
        {
            return ExecuteWrapped(() =>
            {
                return Ok(new GetReceivedMessagesResponse
                {
                    ReceivedErrors = errorMessageHandler.RetrieveErrors().ToList()
                });
            });
        }

        private IHttpActionResult ExecuteWrapped(Func<IHttpActionResult> func)
        {
            try
            {
                var result = func();
                return result;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
