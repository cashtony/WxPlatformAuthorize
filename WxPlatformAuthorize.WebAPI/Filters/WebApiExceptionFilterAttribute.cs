using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute, IAutofacExceptionFilter
    {
        private log4net.ILog _log;
        public WebApiExceptionFilterAttribute(log4net.ILog log)
        {
            _log = log;
        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _log.Error("WebAPIController Exception", actionExecutedContext.Exception);
            var responseBody = new ErrorResponse(actionExecutedContext.Exception.Message);
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new ObjectContent<ErrorResponse>(responseBody, new JsonMediaTypeFormatter())
            };
            actionExecutedContext.Response = response;
        }
    }
}