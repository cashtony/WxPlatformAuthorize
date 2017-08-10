using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI.Controllers
{
    [RoutePrefix("api/authorize")]
    public class AuthorizeController : ApiController
    {
        private AuthorizeService _authorizeService;
        public AuthorizeController(AuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }

        [HttpGet, Route("pre_auth_code")]
        public string GetPreAuthCode()
        {
            return _authorizeService.GetPreAuthCode();
        }

        [HttpGet, Route("access_token")]
        public string GetAccessToken()
        {
            return _authorizeService.GetAccessToken();
        }
    }
}
