using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Supervisor.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var errorName = context.Error.GetType().Name;
            int statusCode = 400;

            switch (errorName)
            {
                case "NullReferenceException":
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case "ArgumentException":
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.ServiceUnavailable;
                    break;
            }


            return Problem(
             detail: context.Error.Message,
             statusCode: statusCode
             );

        }

        /// <summary>
        /// Error action sends an RFC 7807-compliant payload to the client.
        /// </summary>
        /// <returns></returns>
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

     
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();;

            var errorName = context.Error.GetType().Name;
            int statusCode = 400;

            switch (errorName)
            {
                case "NullReferenceException":
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case "ArgumentException":
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.ServiceUnavailable;
                    break;
            }

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message,
                statusCode: statusCode
                );

        }
    }
}
