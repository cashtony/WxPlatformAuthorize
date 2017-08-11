using System;
using System.Web.Http;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    [RoutePrefix("api/debug")]
    public class DebugController : ApiController
    {
        private AuthorizeService _authorizeService;
        public DebugController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }
        [HttpGet, Route("echo")]
        public string Echo()
        {
            return DateTime.Now.ToString();
        }
    }
}
