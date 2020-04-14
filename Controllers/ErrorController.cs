using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace EmployeeManagementCoreApp.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger<ErrorController> logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested does not exist";
                    //ViewBag.Path = statusCodeFeature.OriginalPath;
                    //ViewBag.QS = statusCodeFeature.OriginalQueryString;
                    logger.LogWarning($"404 Error occured. Path = {statusCodeFeature.OriginalPath}" +
                        $" and QueryString = {statusCodeFeature.OriginalQueryString}");
                break;
                default:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested does not exist";
                break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if(exceptionDetails != null)
            {
                ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
                ViewBag.ExceptionStackTrace = exceptionDetails.Error.StackTrace;
                ViewBag.ExceptionPath = exceptionDetails.Path;
                logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");
            }
            return View("Error");
        }
    }
}
