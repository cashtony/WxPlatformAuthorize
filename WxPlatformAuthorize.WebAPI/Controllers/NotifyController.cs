using System.Net.Http;
using System.Web.Http;
using WxPlatformAuthorize.Service;

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
        public void HandleNotifyEvent(HttpRequestMessage request)
        {
            var xml = request.Content.ReadAsStringAsync().Result;
            _authorizeService.HandleNotifyEvent(xml);
        }
    }
}
