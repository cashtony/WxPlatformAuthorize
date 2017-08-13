using System;
using System.Net.Http;
using System.Web.Http;
using WxPlatformAuthorize.Service;
using WxPlatformAuthorize.WebAPI.Models;
using System.Linq;

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
                var postData = request.Content.ReadAsStringAsync().Result;
                var query = request.GetQueryNameValuePairs().ToDictionary(i => i.Key, i => i.Value);

                string msgSig = query["msg_signature"];
                string timeStamp = query["timestamp"];
                string nonce = query["nonce"];
                _authorizeService.HandleNotifyEvent(msgSig, timeStamp, nonce, postData);
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
