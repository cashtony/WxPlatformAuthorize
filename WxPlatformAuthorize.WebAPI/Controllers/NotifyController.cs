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
        public NotifyController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpPost, Route("event")]
        public BaseResponse HandleNotifyEvent(HttpRequestMessage request)
        {
            try
            {
                var xml = request.Content.ReadAsStringAsync().Result;
                _authorizeService.HandleNotifyEvent(xml);
            }
            catch (Exception e)
            {

            }

            return new BaseResponse();
        }
    }
}
