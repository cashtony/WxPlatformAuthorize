using System;
using System.Net.Http;
using System.Web.Http;
using WxPlatformAuthorize.Service;
using WxPlatformAuthorize.WebAPI.Models;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    [RoutePrefix("api/notify")]
    public class NotifyController : ApiController
    {
        private AuthorizeService _authorizeService;
        private log4net.ILog _log;
        public NotifyController(AuthorizeService authorizeService, log4net.ILog log)
        {
            _authorizeService = authorizeService;
            _log = log;
        }

        [HttpPost, Route("event")]
        public HttpResponseMessage HandleNotifyEvent(HttpRequestMessage request)
        {
            try
            {
                var xml = request.Content.ReadAsStringAsync().Result;
                _authorizeService.HandleNotifyEvent(xml);
            }
            catch (Exception e)
            {
                _log.Error("推送事件处理异常", e);
            }

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("success")
            };
        }
    }
}
