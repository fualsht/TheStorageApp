using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.API.Controllers
{
    public static class ControllerBaseExtensionMethods
    {
        /// <summary>
        /// Returns an InternalServerError (500) Response.
        /// </summary>
        /// <param name="exception">The exception from the error</param>
        /// <param name="message">A message to add to the end of the error message</param>
        /// <returns></returns>
        public static ActionResult InternalServerError(this ControllerBase controllerbase, Exception exception, string message)
        {
            var result = controllerbase.StatusCode(StatusCodes.Status500InternalServerError, exception);
            result.Value += "\nMessage: " + message;
            return result;
        }
    }
}
