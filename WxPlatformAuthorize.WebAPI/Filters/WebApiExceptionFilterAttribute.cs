using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
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