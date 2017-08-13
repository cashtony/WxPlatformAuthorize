using System;
using System.Web.Http;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    [RoutePrefix("api/debug")]
    public class DebugController : ApiController
    {
        private AuthorizeService _authorizeService;
        private log4net.ILog _log;
        public DebugController(AuthorizeService authorizeService, log4net.ILog log)
        {
            _authorizeService = authorizeService;
            _log = log;
        }
        [HttpGet, Route("echo")]
        public string Echo()
        {
            _log.Info("echo");
            return DateTime.Now.ToString();
        }
    }
}
